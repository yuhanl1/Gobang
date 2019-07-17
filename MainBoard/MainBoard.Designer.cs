namespace MainBoard
{
    partial class MainBoard
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
            this.GoBang = new System.Windows.Forms.Label();
            this.button人机对弈 = new System.Windows.Forms.Button();
            this.button自动对弈 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GoBang
            // 
            this.GoBang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GoBang.AutoSize = true;
            this.GoBang.BackColor = System.Drawing.Color.Transparent;
            this.GoBang.Font = new System.Drawing.Font("STXinwei", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GoBang.ForeColor = System.Drawing.Color.Black;
            this.GoBang.Location = new System.Drawing.Point(253, 69);
            this.GoBang.Name = "GoBang";
            this.GoBang.Size = new System.Drawing.Size(193, 57);
            this.GoBang.TabIndex = 0;
            this.GoBang.Text = "五子棋";
            this.GoBang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GoBang.Click += new System.EventHandler(this.GoBang_Click);
            // 
            // button人机对弈
            // 
            this.button人机对弈.BackColor = System.Drawing.Color.Transparent;
            this.button人机对弈.Font = new System.Drawing.Font("SimHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button人机对弈.Location = new System.Drawing.Point(263, 197);
            this.button人机对弈.Name = "button人机对弈";
            this.button人机对弈.Size = new System.Drawing.Size(183, 47);
            this.button人机对弈.TabIndex = 1;
            this.button人机对弈.Text = "指定开局";
            this.button人机对弈.UseVisualStyleBackColor = false;
            this.button人机对弈.Click += new System.EventHandler(this.button人机对弈_Click);
            // 
            // button自动对弈
            // 
            this.button自动对弈.BackColor = System.Drawing.Color.Transparent;
            this.button自动对弈.Font = new System.Drawing.Font("SimHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button自动对弈.Location = new System.Drawing.Point(263, 282);
            this.button自动对弈.Name = "button自动对弈";
            this.button自动对弈.Size = new System.Drawing.Size(183, 47);
            this.button自动对弈.TabIndex = 2;
            this.button自动对弈.Text = "自由开局";
            this.button自动对弈.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(306, 385);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "北京科技大学";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(282, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "计算机与通信工程学院";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(302, 425);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "刘雨涵  邢璐茜";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(315, 445);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "2015年5月";
            // 
            // MainBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BackgroundImage = global::MainBoard.Properties.Resources.MainBoardBG;
            this.ClientSize = new System.Drawing.Size(684, 512);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button自动对弈);
            this.Controls.Add(this.button人机对弈);
            this.Controls.Add(this.GoBang);
            this.Name = "MainBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainBoard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GoBang;
        private System.Windows.Forms.Button button人机对弈;
        private System.Windows.Forms.Button button自动对弈;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

