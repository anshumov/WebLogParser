namespace WebLogParser
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.btChoseFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDomenPath = new System.Windows.Forms.TextBox();
            this.tbPeriod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(12, 25);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(126, 20);
            this.dtFrom.TabIndex = 0;
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(144, 25);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(126, 20);
            this.dtTo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Период:";
            // 
            // tbFolder
            // 
            this.tbFolder.Location = new System.Drawing.Point(13, 106);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(516, 20);
            this.tbFolder.TabIndex = 3;
            // 
            // btChoseFolder
            // 
            this.btChoseFolder.Location = new System.Drawing.Point(535, 106);
            this.btChoseFolder.Name = "btChoseFolder";
            this.btChoseFolder.Size = new System.Drawing.Size(24, 20);
            this.btChoseFolder.TabIndex = 4;
            this.btChoseFolder.Text = "...";
            this.btChoseFolder.UseVisualStyleBackColor = true;
            this.btChoseFolder.Click += new System.EventHandler(this.btChoseFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Путь к логам:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Собрать данные";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Адрес домена:";
            // 
            // tbDomenPath
            // 
            this.tbDomenPath.Location = new System.Drawing.Point(334, 25);
            this.tbDomenPath.Name = "tbDomenPath";
            this.tbDomenPath.Size = new System.Drawing.Size(225, 20);
            this.tbDomenPath.TabIndex = 11;
            // 
            // tbPeriod
            // 
            this.tbPeriod.Location = new System.Drawing.Point(276, 25);
            this.tbPeriod.MaxLength = 2;
            this.tbPeriod.Name = "tbPeriod";
            this.tbPeriod.Size = new System.Drawing.Size(52, 20);
            this.tbPeriod.TabIndex = 12;
            this.tbPeriod.Validating += new System.ComponentModel.CancelEventHandler(this.tbPeriod_Validating);
            this.tbPeriod.Validated += new System.EventHandler(this.tbPeriod_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Кол. мес.";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(547, 20);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "default.aspx;index.html;publish.htm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Фильтр по главной странице:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 167);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPeriod);
            this.Controls.Add(this.tbDomenPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btChoseFolder);
            this.Controls.Add(this.tbFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Name = "Form1";
            this.Text = "Парсер лог файлов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Button btChoseFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDomenPath;
        private System.Windows.Forms.TextBox tbPeriod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}

