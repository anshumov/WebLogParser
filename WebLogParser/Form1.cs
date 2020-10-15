using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.String;

namespace WebLogParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chkUTC.Checked = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtTo.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 14);
            var deltaDate = dtTo.Value.AddMonths(-1 * Properties.Settings.Default.Period);
            dtFrom.Value = new DateTime(deltaDate.Year, deltaDate.Month, 15);

            tbFolder.Text = Properties.Settings.Default.Path;
            tbPeriod.Text = Properties.Settings.Default.Period.ToString();
            tbDomenPath.Text = IsNullOrEmpty(Properties.Settings.Default.DomenPath)
                ? System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName
                : Properties.Settings.Default.DomenPath;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Path = tbFolder.Text;
            Properties.Settings.Default.DomenPath = tbDomenPath.Text;
            Properties.Settings.Default.Save();
        }

        private void btChoseFolder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = tbFolder.Text;
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbFolder.Text = fbd.SelectedPath;
            }
        }

        private List<LogRecord> GetLogRecords(string[] files)
        {
            var records = new List<LogRecord>();
            var logPattern = new Dictionary<string, int>();
            var defaultPages = textBox1.Text.Split(';');

            foreach (var nameFile in files)
            {
                var fileLastWriteTime = File.GetLastWriteTime(nameFile);
                if (fileLastWriteTime < dtFrom.Value.Date || fileLastWriteTime > dtTo.Value.Date.AddDays(2)) continue;

                var readStrings = File.ReadAllLines(nameFile);

                foreach (var s in readStrings)
                {
                    if (s.StartsWith("#Fields:"))
                    {
                        logPattern.Clear();
                        var t = s.Substring(9).Split(' ');
                        for (var i = 0; i < t.Length; i++)
                        {
                            logPattern.Add(t[i], i);
                        }
                    }

                    if (!s.StartsWith("#") && logPattern.Count != 0)
                    {

                        var parseStr = s.Split(' ');
                        var date = logPattern.ContainsKey("date") ? parseStr[logPattern["date"]] : null;
                        var time = logPattern.ContainsKey("time") ? parseStr[logPattern["time"]] : null;
                        var ipAddr = logPattern.ContainsKey("c-ip") ? parseStr[logPattern["c-ip"]] : null;
                        var userName = logPattern.ContainsKey("cs-username") ? parseStr[logPattern["cs-username"]] : null;
                        var refer = logPattern.ContainsKey("cs(Referer)") ? parseStr[logPattern["cs(Referer)"]] : null;
                        var host = logPattern.ContainsKey("cs-host") ? parseStr[logPattern["cs-host"]] : null;
                        var status = logPattern.ContainsKey("sc-status") ? parseStr[logPattern["sc-status"]] : null;
                        var csUri = logPattern.ContainsKey("cs-uri-stem") ? parseStr[logPattern["cs-uri-stem"]] : null;

                        if (status != null && !status.Contains("200"))
                        {
                            continue;
                        }

                        var brkLoop = true;
                        if (csUri != null)
                        {
                            if (defaultPages.Any(page => csUri.Contains(page)))
                            {
                                brkLoop = false;
                            }
                        }

                        if (brkLoop && csUri != @"/")
                        {
                            continue;
                        }
                        DateTime.TryParse(date, out var d);
                        TimeSpan.TryParse(time, out var t);
                        var dt = d.Add(t);
                        if (chkUTC.Checked) dt = dt.ToLocalTime();
                        if (dt.Date < dtFrom.Value.Date || dt > dtTo.Value.Date.AddDays(1)) 
                            continue;

                        var record = new LogRecord();
                        record.UserName = userName;
                        record.IP = ipAddr;
                        record.DateTime = dt;
                        record.Reference = refer;
                        record.Host = host;
                        record.Status = status;
                        record.FileName = nameFile;
                        records.Add(record);
                    }
                }
            }

            return records;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsNullOrEmpty(tbFolder.Text))
            {
                MessageBox.Show(@"Выбери папку!");
                return;
            }

            var ofd = new SaveFileDialog
            {
                Filter = @"Flat Files (*.csv)|*.csv",
                FileName = Properties.Settings.Default.File
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;
            Properties.Settings.Default.File = ofd.FileName;
            Properties.Settings.Default.Save();

            var nameFiles = Directory.GetFiles(tbFolder.Text, "*.log");
            var records = GetLogRecords(nameFiles);

            var strings = records
                .Where(r => r.UserName != "-" && !IsNullOrEmpty(r.UserName) && r.Status == "200")
                .GroupBy(r => new {UserName = r.UserName.ToLower(), r.Host}, r => r.DateTime,
                    (grFields, date) => new
                    {
                        Name = grFields.UserName,
                        FullName = GetADInfo(tbDomenPath.Text, "sAMAccountName",
                            grFields.UserName.Substring(grFields.UserName.IndexOf(@"\", StringComparison.Ordinal) +
                                                        1), "name"),
                        grFields.Host,
                        maxDate = date.Max()
                    })
                .OrderBy(r => r.FullName)
                .Select(r =>
                    r.Host + ";" + r.Name + ";" + r.FullName + ";" +
                    r.maxDate.AddHours(5).ToString(CultureInfo.CurrentCulture))
                .ToList();
            strings.Insert(0, "Host;DomainAccount;DisplayName;LastAccess");

            if (File.Exists(ofd.FileName)) File.Delete(ofd.FileName);
            File.WriteAllLines(ofd.FileName, strings, Encoding.UTF8);
            MessageBox.Show(@"Файл сохранен.", @"Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var nameFiles = Directory.GetFiles(tbFolder.Text, "*.log");
            var records = GetLogRecords(nameFiles);

            var avg = records
                .Where(r => r.UserName != "-" && !IsNullOrEmpty(r.UserName) && r.Status == "200" )
                .GroupBy(r => new {UserName = r.UserName.ToLower(), r.DateTime.Date})
                .Select(gp=> new {UserName = gp.Key.UserName, Date = gp.Key.Date })
                .GroupBy(g => new {Date = g.Date})
                .Select(s=> s.Count())
                .Average()
                ;
                
            MessageBox.Show($@"Среднее за период: {Math.Round(avg, 1)}", @"Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetADInfo(string searchDirectory, string searchField, string searchString, string returnField)
        {
#if DEBUG
            return null;
#endif
            var adCon = new OleDbConnection("Provider=ADSDSOObject;");
            adCon.Open();
            try
            {
                var cmd = "<LDAP://" + searchDirectory + ">;(&(objectCategory=User)" + "(" + searchField + "=" +
                          searchString + "));" + searchField + "," + returnField + ";subtree";
                var adCmd = new OleDbCommand(cmd, adCon);
                var adReader = adCmd.ExecuteReader();
                if (adReader != null && adReader.HasRows)
                {
                    adReader.Read();
                    return adReader[returnField].ToString();
                }

                return "";
            }
            catch (Exception)
            {
                adCon.Close();
                return null;
            }
        }

        struct LogRecord
        {
            public string UserName;
            public string IP;
            public DateTime DateTime;
            public string Reference;
            public string Host;
            public string Status;
            public string FileName;
        }

        private void tbPeriod_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !int.TryParse(tbPeriod.Text, out _);
            errorProvider1.SetError(tbPeriod, "Введено некорректное значение!");
        }

        private void tbPeriod_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(tbPeriod, "");
            Properties.Settings.Default.Period = int.Parse(tbPeriod.Text);
            var deltaDate = dtTo.Value.AddMonths(-1 * Properties.Settings.Default.Period);
            dtFrom.Value = new DateTime(deltaDate.Year, deltaDate.Month, 15);
        }

    }

}
