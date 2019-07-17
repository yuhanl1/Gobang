namespace ManMachine
{
    partial class ManMachine
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Board = new System.Windows.Forms.PictureBox();
            this.buttonBegin = new System.Windows.Forms.Button();
            this.我方 = new System.Windows.Forms.Label();
            this.机方 = new System.Windows.Forms.Label();
            this.请选择颜色 = new System.Windows.Forms.Label();
            this.radioButton黑子 = new System.Windows.Forms.RadioButton();
            this.radioButton白子 = new System.Windows.Forms.RadioButton();
            this.panelColor = new System.Windows.Forms.Panel();
            this.pictureBoxMan = new System.Windows.Forms.PictureBox();
            this.pictureBoxComputer = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label步数 = new System.Windows.Forms.Label();
            this.label五手N打 = new System.Windows.Forms.Label();
            this.panelnpiece = new System.Windows.Forms.Panel();
            this.radioButton2子 = new System.Windows.Forms.RadioButton();
            this.radioButton4子 = new System.Windows.Forms.RadioButton();
            this.button悔棋 = new System.Windows.Forms.Button();
            this.button三手交换 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Board)).BeginInit();
            this.panelColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxComputer)).BeginInit();
            this.panelnpiece.SuspendLayout();
            this.SuspendLayout();
            // 
            // Board
            // 
            this.Board.BackColor = System.Drawing.Color.Wheat;
            this.Board.Location = new System.Drawing.Point(155, 35);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(500, 500);
            this.Board.TabIndex = 0;
            this.Board.TabStop = false;
            this.Board.Click += new System.EventHandler(this.Board_Click);
            // 
            // buttonBegin
            // 
            this.buttonBegin.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonBegin.Location = new System.Drawing.Point(38, 419);
            this.buttonBegin.Name = "buttonBegin";
            this.buttonBegin.Size = new System.Drawing.Size(82, 32);
            this.buttonBegin.TabIndex = 1;
            this.buttonBegin.Text = "开始";
            this.buttonBegin.UseVisualStyleBackColor = true;
            this.buttonBegin.Click += new System.EventHandler(this.buttonBegin_Click);
            // 
            // 我方
            // 
            this.我方.AutoSize = true;
            this.我方.BackColor = System.Drawing.Color.Transparent;
            this.我方.Font = new System.Drawing.Font("SimHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.我方.Location = new System.Drawing.Point(27, 15);
            this.我方.Name = "我方";
            this.我方.Size = new System.Drawing.Size(65, 21);
            this.我方.TabIndex = 2;
            this.我方.Text = "我方:";
            // 
            // 机方
            // 
            this.机方.AutoSize = true;
            this.机方.BackColor = System.Drawing.Color.Transparent;
            this.机方.Font = new System.Drawing.Font("SimHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.机方.Location = new System.Drawing.Point(27, 58);
            this.机方.Name = "机方";
            this.机方.Size = new System.Drawing.Size(65, 21);
            this.机方.TabIndex = 3;
            this.机方.Text = "机方:";
            // 
            // 请选择颜色
            // 
            this.请选择颜色.AutoSize = true;
            this.请选择颜色.BackColor = System.Drawing.Color.Transparent;
            this.请选择颜色.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.请选择颜色.Location = new System.Drawing.Point(27, 152);
            this.请选择颜色.Name = "请选择颜色";
            this.请选择颜色.Size = new System.Drawing.Size(96, 16);
            this.请选择颜色.TabIndex = 4;
            this.请选择颜色.Text = "请选择颜色:";
            // 
            // radioButton黑子
            // 
            this.radioButton黑子.AutoSize = true;
            this.radioButton黑子.BackColor = System.Drawing.Color.Transparent;
            this.radioButton黑子.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton黑子.Location = new System.Drawing.Point(14, 12);
            this.radioButton黑子.Name = "radioButton黑子";
            this.radioButton黑子.Size = new System.Drawing.Size(58, 20);
            this.radioButton黑子.TabIndex = 5;
            this.radioButton黑子.TabStop = true;
            this.radioButton黑子.Text = "黑子";
            this.radioButton黑子.UseVisualStyleBackColor = false;
            this.radioButton黑子.CheckedChanged += new System.EventHandler(this.radioButton黑子_CheckedChanged);
            // 
            // radioButton白子
            // 
            this.radioButton白子.AutoSize = true;
            this.radioButton白子.BackColor = System.Drawing.Color.Transparent;
            this.radioButton白子.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton白子.Location = new System.Drawing.Point(14, 38);
            this.radioButton白子.Name = "radioButton白子";
            this.radioButton白子.Size = new System.Drawing.Size(58, 20);
            this.radioButton白子.TabIndex = 6;
            this.radioButton白子.TabStop = true;
            this.radioButton白子.Text = "白子";
            this.radioButton白子.UseVisualStyleBackColor = false;
            this.radioButton白子.CheckedChanged += new System.EventHandler(this.radioButton白子_CheckedChanged);
            // 
            // panelColor
            // 
            this.panelColor.BackColor = System.Drawing.Color.Transparent;
            this.panelColor.Controls.Add(this.radioButton黑子);
            this.panelColor.Controls.Add(this.radioButton白子);
            this.panelColor.Location = new System.Drawing.Point(34, 173);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(87, 68);
            this.panelColor.TabIndex = 7;
            // 
            // pictureBoxMan
            // 
            this.pictureBoxMan.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxMan.Location = new System.Drawing.Point(96, 11);
            this.pictureBoxMan.Name = "pictureBoxMan";
            this.pictureBoxMan.Size = new System.Drawing.Size(30, 30);
            this.pictureBoxMan.TabIndex = 8;
            this.pictureBoxMan.TabStop = false;
            // 
            // pictureBoxComputer
            // 
            this.pictureBoxComputer.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxComputer.Location = new System.Drawing.Point(96, 52);
            this.pictureBoxComputer.Name = "pictureBoxComputer";
            this.pictureBoxComputer.Size = new System.Drawing.Size(30, 30);
            this.pictureBoxComputer.TabIndex = 9;
            this.pictureBoxComputer.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(27, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "黑棋先走";
            // 
            // label步数
            // 
            this.label步数.AutoSize = true;
            this.label步数.BackColor = System.Drawing.Color.Transparent;
            this.label步数.Font = new System.Drawing.Font("SimHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label步数.Location = new System.Drawing.Point(27, 101);
            this.label步数.Name = "label步数";
            this.label步数.Size = new System.Drawing.Size(65, 21);
            this.label步数.TabIndex = 11;
            this.label步数.Text = "步数:";
            // 
            // label五手N打
            // 
            this.label五手N打.AutoSize = true;
            this.label五手N打.BackColor = System.Drawing.Color.Transparent;
            this.label五手N打.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label五手N打.Location = new System.Drawing.Point(27, 246);
            this.label五手N打.Name = "label五手N打";
            this.label五手N打.Size = new System.Drawing.Size(112, 32);
            this.label五手N打.TabIndex = 12;
            this.label五手N打.Text = "请选择五手N打\r\n棋子数:";
            // 
            // panelnpiece
            // 
            this.panelnpiece.BackColor = System.Drawing.Color.Transparent;
            this.panelnpiece.Controls.Add(this.radioButton2子);
            this.panelnpiece.Controls.Add(this.radioButton4子);
            this.panelnpiece.Location = new System.Drawing.Point(34, 281);
            this.panelnpiece.Name = "panelnpiece";
            this.panelnpiece.Size = new System.Drawing.Size(87, 68);
            this.panelnpiece.TabIndex = 13;
            // 
            // radioButton2子
            // 
            this.radioButton2子.AutoSize = true;
            this.radioButton2子.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2子.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton2子.Location = new System.Drawing.Point(14, 12);
            this.radioButton2子.Name = "radioButton2子";
            this.radioButton2子.Size = new System.Drawing.Size(50, 20);
            this.radioButton2子.TabIndex = 5;
            this.radioButton2子.TabStop = true;
            this.radioButton2子.Text = "2子";
            this.radioButton2子.UseVisualStyleBackColor = false;
            this.radioButton2子.CheckedChanged += new System.EventHandler(this.radioButton2子_CheckedChanged);
            // 
            // radioButton4子
            // 
            this.radioButton4子.AutoSize = true;
            this.radioButton4子.BackColor = System.Drawing.Color.Transparent;
            this.radioButton4子.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton4子.Location = new System.Drawing.Point(14, 38);
            this.radioButton4子.Name = "radioButton4子";
            this.radioButton4子.Size = new System.Drawing.Size(50, 20);
            this.radioButton4子.TabIndex = 6;
            this.radioButton4子.TabStop = true;
            this.radioButton4子.Text = "4子";
            this.radioButton4子.UseVisualStyleBackColor = false;
            this.radioButton4子.CheckedChanged += new System.EventHandler(this.radioButton4子_CheckedChanged);
            // 
            // button悔棋
            // 
            this.button悔棋.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button悔棋.Location = new System.Drawing.Point(38, 461);
            this.button悔棋.Name = "button悔棋";
            this.button悔棋.Size = new System.Drawing.Size(82, 32);
            this.button悔棋.TabIndex = 14;
            this.button悔棋.Text = "悔棋";
            this.button悔棋.UseVisualStyleBackColor = true;
            this.button悔棋.Click += new System.EventHandler(this.button悔棋_Click);
            // 
            // button三手交换
            // 
            this.button三手交换.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button三手交换.Location = new System.Drawing.Point(38, 503);
            this.button三手交换.Name = "button三手交换";
            this.button三手交换.Size = new System.Drawing.Size(82, 32);
            this.button三手交换.TabIndex = 15;
            this.button三手交换.Text = "三手交换";
            this.button三手交换.UseVisualStyleBackColor = true;
            this.button三手交换.Click += new System.EventHandler(this.button三手交换_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(28, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "请选择指定开局:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "残月局",
            "瑞星局",
            "雨月局",
            "疏星局",
            "松月局",
            "新月局",
            "花月局",
            "寒星局",
            "丘月局",
            "山月局",
            "金星局",
            "溪月局",
            "游星局",
            "斜月局",
            "名月局",
            "恒星局",
            "岚月局",
            "明星局",
            "峡月局",
            "长星局",
            "浦月局",
            "云月局",
            "水月局",
            "银月局",
            "流星局",
            "彗星局"});
            this.comboBox1.Location = new System.Drawing.Point(28, 380);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(111, 20);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ManMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.BackgroundImage = global::ManMachine.Properties.Resources.WoodBG;
            this.ClientSize = new System.Drawing.Size(684, 562);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button三手交换);
            this.Controls.Add(this.button悔棋);
            this.Controls.Add(this.panelnpiece);
            this.Controls.Add(this.label五手N打);
            this.Controls.Add(this.label步数);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxComputer);
            this.Controls.Add(this.pictureBoxMan);
            this.Controls.Add(this.panelColor);
            this.Controls.Add(this.请选择颜色);
            this.Controls.Add(this.机方);
            this.Controls.Add(this.我方);
            this.Controls.Add(this.buttonBegin);
            this.Controls.Add(this.Board);
            this.Name = "ManMachine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManMachine";
            ((System.ComponentModel.ISupportInitialize)(this.Board)).EndInit();
            this.panelColor.ResumeLayout(false);
            this.panelColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxComputer)).EndInit();
            this.panelnpiece.ResumeLayout(false);
            this.panelnpiece.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Board;
        private System.Windows.Forms.Button buttonBegin;
        private System.Windows.Forms.Label 我方;
        private System.Windows.Forms.Label 机方;
        private System.Windows.Forms.Label 请选择颜色;
        private System.Windows.Forms.RadioButton radioButton黑子;
        private System.Windows.Forms.RadioButton radioButton白子;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.PictureBox pictureBoxMan;
        private System.Windows.Forms.PictureBox pictureBoxComputer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label步数;
        private System.Windows.Forms.Label label五手N打;
        private System.Windows.Forms.Panel panelnpiece;
        private System.Windows.Forms.RadioButton radioButton2子;
        private System.Windows.Forms.RadioButton radioButton4子;
        private System.Windows.Forms.Button button悔棋;
        private System.Windows.Forms.Button button三手交换;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

