namespace OOAP_1_without_pattern
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
            this.txtNewNumber = new System.Windows.Forms.TextBox();
            this.txtNewContent = new System.Windows.Forms.TextBox();
            this.dtpNewDate = new System.Windows.Forms.DateTimePicker();
            this.panelTrip = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.txtEmployee = new System.Windows.Forms.TextBox();
            this.panelLetter = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCorrespondent = new System.Windows.Forms.TextBox();
            this.txtLetterType = new System.Windows.Forms.TextBox();
            this.panelOrder = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.txtResponsible = new System.Windows.Forms.TextBox();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.btnCreateDoc = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDocType = new System.Windows.Forms.ComboBox();
            this.panelTrip.SuspendLayout();
            this.panelLetter.SuspendLayout();
            this.panelOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNewNumber
            // 
            this.txtNewNumber.Location = new System.Drawing.Point(33, 72);
            this.txtNewNumber.Name = "txtNewNumber";
            this.txtNewNumber.Size = new System.Drawing.Size(189, 22);
            this.txtNewNumber.TabIndex = 1;
            this.txtNewNumber.TextChanged += new System.EventHandler(this.txtNewNumber_TextChanged);
            // 
            // txtNewContent
            // 
            this.txtNewContent.Location = new System.Drawing.Point(33, 165);
            this.txtNewContent.Name = "txtNewContent";
            this.txtNewContent.Size = new System.Drawing.Size(193, 22);
            this.txtNewContent.TabIndex = 2;
            this.txtNewContent.TextChanged += new System.EventHandler(this.txtNewContent_TextChanged);
            // 
            // dtpNewDate
            // 
            this.dtpNewDate.Location = new System.Drawing.Point(33, 120);
            this.dtpNewDate.Name = "dtpNewDate";
            this.dtpNewDate.Size = new System.Drawing.Size(189, 22);
            this.dtpNewDate.TabIndex = 3;
            this.dtpNewDate.ValueChanged += new System.EventHandler(this.dtpNewDate_ValueChanged);
            // 
            // panelTrip
            // 
            this.panelTrip.Controls.Add(this.label14);
            this.panelTrip.Controls.Add(this.label15);
            this.panelTrip.Controls.Add(this.label16);
            this.panelTrip.Controls.Add(this.txtDestination);
            this.panelTrip.Controls.Add(this.txtPeriod);
            this.panelTrip.Controls.Add(this.txtEmployee);
            this.panelTrip.Location = new System.Drawing.Point(33, 204);
            this.panelTrip.Name = "panelTrip";
            this.panelTrip.Size = new System.Drawing.Size(256, 189);
            this.panelTrip.TabIndex = 4;
            this.panelTrip.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTrip_Paint);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 16);
            this.label14.TabIndex = 19;
            this.label14.Text = "Сотрудник";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 16);
            this.label15.TabIndex = 20;
            this.label15.Text = "Период";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 136);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(130, 16);
            this.label16.TabIndex = 21;
            this.label16.Text = "Место назначения";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(19, 155);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(173, 22);
            this.txtDestination.TabIndex = 2;
            this.txtDestination.TextChanged += new System.EventHandler(this.txtDestination_TextChanged);
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new System.Drawing.Point(20, 91);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(175, 22);
            this.txtPeriod.TabIndex = 1;
            this.txtPeriod.TextChanged += new System.EventHandler(this.txtPeriod_TextChanged);
            // 
            // txtEmployee
            // 
            this.txtEmployee.Location = new System.Drawing.Point(20, 31);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Size = new System.Drawing.Size(179, 22);
            this.txtEmployee.TabIndex = 0;
            this.txtEmployee.TextChanged += new System.EventHandler(this.txtEmployee_TextChanged);
            // 
            // panelLetter
            // 
            this.panelLetter.Controls.Add(this.label11);
            this.panelLetter.Controls.Add(this.label10);
            this.panelLetter.Controls.Add(this.txtCorrespondent);
            this.panelLetter.Controls.Add(this.txtLetterType);
            this.panelLetter.Location = new System.Drawing.Point(36, 202);
            this.panelLetter.Name = "panelLetter";
            this.panelLetter.Size = new System.Drawing.Size(254, 188);
            this.panelLetter.TabIndex = 2;
            this.panelLetter.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLetter_Paint);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 16);
            this.label11.TabIndex = 21;
            this.label11.Text = "Тип (вх/исх)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 16);
            this.label10.TabIndex = 20;
            this.label10.Text = "Корреспондент";
            // 
            // txtCorrespondent
            // 
            this.txtCorrespondent.Location = new System.Drawing.Point(22, 138);
            this.txtCorrespondent.Name = "txtCorrespondent";
            this.txtCorrespondent.Size = new System.Drawing.Size(137, 22);
            this.txtCorrespondent.TabIndex = 1;
            this.txtCorrespondent.TextChanged += new System.EventHandler(this.txtCorrespondent_TextChanged);
            // 
            // txtLetterType
            // 
            this.txtLetterType.Location = new System.Drawing.Point(20, 73);
            this.txtLetterType.Name = "txtLetterType";
            this.txtLetterType.Size = new System.Drawing.Size(139, 22);
            this.txtLetterType.TabIndex = 0;
            this.txtLetterType.TextChanged += new System.EventHandler(this.txtLetterType_TextChanged);
            // 
            // panelOrder
            // 
            this.panelOrder.Controls.Add(this.label9);
            this.panelOrder.Controls.Add(this.label8);
            this.panelOrder.Controls.Add(this.label7);
            this.panelOrder.Controls.Add(this.dtpDueDate);
            this.panelOrder.Controls.Add(this.txtResponsible);
            this.panelOrder.Controls.Add(this.txtDepartment);
            this.panelOrder.Location = new System.Drawing.Point(39, 198);
            this.panelOrder.Name = "panelOrder";
            this.panelOrder.Size = new System.Drawing.Size(253, 195);
            this.panelOrder.TabIndex = 3;
            this.panelOrder.Paint += new System.Windows.Forms.PaintEventHandler(this.panelOrder_Paint);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(198, 16);
            this.label9.TabIndex = 19;
            this.label9.Text = "Ответственный исполнитель";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 16);
            this.label8.TabIndex = 18;
            this.label8.Text = "Срок выполнения";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Подразделение";
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Location = new System.Drawing.Point(23, 93);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(159, 22);
            this.dtpDueDate.TabIndex = 2;
            this.dtpDueDate.ValueChanged += new System.EventHandler(this.dtpDueDate_ValueChanged);
            // 
            // txtResponsible
            // 
            this.txtResponsible.Location = new System.Drawing.Point(23, 160);
            this.txtResponsible.Name = "txtResponsible";
            this.txtResponsible.Size = new System.Drawing.Size(159, 22);
            this.txtResponsible.TabIndex = 1;
            this.txtResponsible.TextChanged += new System.EventHandler(this.txtResponsible_TextChanged);
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(23, 29);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(159, 22);
            this.txtDepartment.TabIndex = 0;
            this.txtDepartment.TextChanged += new System.EventHandler(this.txtDepartment_TextChanged);
            // 
            // btnCreateDoc
            // 
            this.btnCreateDoc.Location = new System.Drawing.Point(82, 410);
            this.btnCreateDoc.Name = "btnCreateDoc";
            this.btnCreateDoc.Size = new System.Drawing.Size(131, 51);
            this.btnCreateDoc.TabIndex = 6;
            this.btnCreateDoc.Text = "Создать документ";
            this.btnCreateDoc.UseVisualStyleBackColor = true;
            this.btnCreateDoc.Click += new System.EventHandler(this.btnCreateDoc_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(307, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 54);
            this.button1.TabIndex = 7;
            this.button1.Text = "Показать список документов";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(558, 410);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 51);
            this.button2.TabIndex = 8;
            this.button2.Text = "Вся информация";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(349, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(365, 292);
            this.listBox1.TabIndex = 9;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(546, 371);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 22);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Вид документа";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Номер документа";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Дата";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Краткое содержание";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(555, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Номер документа";
            // 
            // cmbDocType
            // 
            this.cmbDocType.FormattingEnabled = true;
            this.cmbDocType.Location = new System.Drawing.Point(33, 26);
            this.cmbDocType.Name = "cmbDocType";
            this.cmbDocType.Size = new System.Drawing.Size(130, 24);
            this.cmbDocType.TabIndex = 16;
            this.cmbDocType.SelectedIndexChanged += new System.EventHandler(this.cmbDocType_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 504);
            this.Controls.Add(this.cmbDocType);
            this.Controls.Add(this.panelOrder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelLetter);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCreateDoc);
            this.Controls.Add(this.panelTrip);
            this.Controls.Add(this.dtpNewDate);
            this.Controls.Add(this.txtNewContent);
            this.Controls.Add(this.txtNewNumber);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelTrip.ResumeLayout(false);
            this.panelTrip.PerformLayout();
            this.panelLetter.ResumeLayout(false);
            this.panelLetter.PerformLayout();
            this.panelOrder.ResumeLayout(false);
            this.panelOrder.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNewNumber;
        private System.Windows.Forms.TextBox txtNewContent;
        private System.Windows.Forms.DateTimePicker dtpNewDate;
        private System.Windows.Forms.Panel panelTrip;
        private System.Windows.Forms.TextBox txtLetterType;
        private System.Windows.Forms.TextBox txtCorrespondent;
        private System.Windows.Forms.Panel panelLetter;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.TextBox txtEmployee;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtResponsible;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Panel panelOrder;
        private System.Windows.Forms.Button btnCreateDoc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbDocType;
    }
}

