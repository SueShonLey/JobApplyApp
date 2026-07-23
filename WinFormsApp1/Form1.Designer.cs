namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            groupBox1 = new GroupBox();
            label6 = new Label();
            button2 = new Button();
            label2 = new Label();
            button4 = new Button();
            label1 = new Label();
            button1 = new Button();
            groupBox2 = new GroupBox();
            linkLabel7 = new LinkLabel();
            linkLabel6 = new LinkLabel();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            label8 = new Label();
            linkLabel3 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            label7 = new Label();
            checkBox2 = new CheckBox();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            textBox1 = new TextBox();
            label5 = new Label();
            label4 = new Label();
            comboBox2 = new ComboBox();
            label3 = new Label();
            comboBox1 = new ComboBox();
            checkBox1 = new CheckBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new Point(12, 11);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(221, 654);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "工具栏";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("宋体", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(64, 579);
            label6.Name = "label6";
            label6.Size = new Size(87, 15);
            label6.TabIndex = 6;
            label6.Text = "黄金时间档";
            label6.Click += label6_Click;
            // 
            // button2
            // 
            button2.Location = new Point(23, 213);
            button2.Name = "button2";
            button2.Size = new Size(176, 56);
            button2.TabIndex = 5;
            button2.Text = "信息清空";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("宋体", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label2.ForeColor = Color.Blue;
            label2.Location = new Point(62, 299);
            label2.Name = "label2";
            label2.Size = new Size(96, 255);
            label2.TabIndex = 4;
            label2.Text = "国企：0\r\n\r\n私企：0\r\n\r\n外企：0\r\n\r\n通过：0\r\n\r\n拒绝：0\r\n\r\n评估：0\r\n\r\n未有结果：0\r\n\r\n未有结果：0\r\n\r\n未有结果：0";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // button4
            // 
            button4.Location = new Point(23, 127);
            button4.Name = "button4";
            button4.Size = new Size(176, 56);
            button4.TabIndex = 3;
            button4.Text = "渠道维护";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("宋体", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.Location = new Point(23, 623);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // button1
            // 
            button1.Location = new Point(23, 37);
            button1.Name = "button1";
            button1.Size = new Size(176, 56);
            button1.TabIndex = 0;
            button1.Text = "新增公司";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.Transparent;
            groupBox2.Controls.Add(linkLabel7);
            groupBox2.Controls.Add(linkLabel6);
            groupBox2.Controls.Add(linkLabel5);
            groupBox2.Controls.Add(linkLabel4);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(linkLabel3);
            groupBox2.Controls.Add(linkLabel2);
            groupBox2.Controls.Add(linkLabel1);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(checkBox2);
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(comboBox2);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(comboBox1);
            groupBox2.Controls.Add(checkBox1);
            groupBox2.Location = new Point(249, 11);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(1003, 654);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "信息栏";
            // 
            // linkLabel7
            // 
            linkLabel7.AutoSize = true;
            linkLabel7.Location = new Point(775, 75);
            linkLabel7.Name = "linkLabel7";
            linkLabel7.Size = new Size(67, 15);
            linkLabel7.TabIndex = 18;
            linkLabel7.TabStop = true;
            linkLabel7.Text = "前程无忧";
            linkLabel7.LinkClicked += linkLabel7_LinkClicked;
            // 
            // linkLabel6
            // 
            linkLabel6.AutoSize = true;
            linkLabel6.Location = new Point(732, 75);
            linkLabel6.Name = "linkLabel6";
            linkLabel6.Size = new Size(37, 15);
            linkLabel6.TabIndex = 17;
            linkLabel6.TabStop = true;
            linkLabel6.Text = "猎聘";
            linkLabel6.LinkClicked += linkLabel6_LinkClicked;
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.Location = new Point(659, 75);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(67, 15);
            linkLabel5.TabIndex = 16;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "智联招聘";
            linkLabel5.LinkClicked += linkLabel5_LinkClicked;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.Location = new Point(587, 75);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(67, 15);
            linkLabel4.TabIndex = 15;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "高德地图";
            linkLabel4.LinkClicked += linkLabel4_LinkClicked;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(510, 76);
            label8.Name = "label8";
            label8.Size = new Size(82, 15);
            label8.TabIndex = 14;
            label8.Text = "快速访问：";
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new Point(332, 76);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(172, 15);
            linkLabel3.TabIndex = 13;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "全国法院执行信息公开网";
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(150, 76);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(173, 15);
            linkLabel2.TabIndex = 12;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "中国裁判文书网(需登录)";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(90, 76);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(52, 15);
            linkLabel1.TabIndex = 11;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "天眼查";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 76);
            label7.Name = "label7";
            label7.Size = new Size(82, 15);
            label7.TabIndex = 10;
            label7.Text = "便捷背调：";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Location = new Point(877, 39);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(119, 19);
            checkBox2.TabIndex = 9;
            checkBox2.Text = "剔除拒绝状态";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += comboBox2_SelectedIndexChanged;
            // 
            // button3
            // 
            button3.Location = new Point(911, 64);
            button3.Name = "button3";
            button3.Size = new Size(83, 29);
            button3.TabIndex = 2;
            button3.Text = "查询";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(11, 97);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(983, 541);
            dataGridView1.TabIndex = 8;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(61, 37);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(157, 25);
            textBox1.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 43);
            label5.Name = "label5";
            label5.Size = new Size(52, 15);
            label5.TabIndex = 6;
            label5.Text = "检索：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(476, 43);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 5;
            label4.Text = "阶段：";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "全部类型", "已投递", "评估中", "已面试", "已有结果" });
            comboBox2.Location = new Point(531, 39);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(142, 23);
            comboBox2.TabIndex = 4;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(235, 45);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 3;
            label3.Text = "类型：";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "全部类型", "国企", "私企", "外企" });
            comboBox1.Location = new Point(288, 39);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(155, 23);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(715, 39);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(142, 19);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "最新7天内有反馈";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += comboBox2_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1262, 676);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            DoubleBuffered = true;
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "应聘通分析平台";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button button1;
        private TextBox textBox1;
        private Label label5;
        private Label label4;
        private ComboBox comboBox2;
        private Label label3;
        private ComboBox comboBox1;
        private CheckBox checkBox1;
        private DataGridView dataGridView1;
        private Label label1;
        private Button button3;
        private Button button4;
        private CheckBox checkBox2;
        private Label label2;
        private Button button2;
        private Label label6;
        private LinkLabel linkLabel1;
        private Label label7;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel7;
        private LinkLabel linkLabel6;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private Label label8;
    }
}
