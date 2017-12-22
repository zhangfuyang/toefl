namespace toefl
{
    partial class register
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
            this.mail_textbox = new System.Windows.Forms.TextBox();
            this.name_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.passwd_textbox = new System.Windows.Forms.TextBox();
            this.passwd2_textbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mail_textbox
            // 
            this.mail_textbox.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mail_textbox.Location = new System.Drawing.Point(435, 184);
            this.mail_textbox.Name = "mail_textbox";
            this.mail_textbox.Size = new System.Drawing.Size(454, 65);
            this.mail_textbox.TabIndex = 7;
            // 
            // name_textbox
            // 
            this.name_textbox.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name_textbox.Location = new System.Drawing.Point(435, 73);
            this.name_textbox.Name = "name_textbox";
            this.name_textbox.Size = new System.Drawing.Size(454, 65);
            this.name_textbox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(173, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 50);
            this.label2.TabIndex = 5;
            this.label2.Text = "密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(143, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 50);
            this.label1.TabIndex = 4;
            this.label1.Text = "用户名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(61, 403);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(322, 50);
            this.label3.TabIndex = 8;
            this.label3.Text = "再次输入密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(173, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 50);
            this.label4.TabIndex = 12;
            this.label4.Text = "邮箱";
            // 
            // passwd_textbox
            // 
            this.passwd_textbox.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwd_textbox.Location = new System.Drawing.Point(435, 298);
            this.passwd_textbox.Name = "passwd_textbox";
            this.passwd_textbox.PasswordChar = '*';
            this.passwd_textbox.Size = new System.Drawing.Size(454, 65);
            this.passwd_textbox.TabIndex = 13;
            // 
            // passwd2_textbox
            // 
            this.passwd2_textbox.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwd2_textbox.Location = new System.Drawing.Point(435, 400);
            this.passwd2_textbox.Name = "passwd2_textbox";
            this.passwd2_textbox.PasswordChar = '*';
            this.passwd2_textbox.Size = new System.Drawing.Size(454, 65);
            this.passwd2_textbox.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(182, 550);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 92);
            this.button1.TabIndex = 15;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(563, 550);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(224, 92);
            this.button2.TabIndex = 16;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 712);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.passwd2_textbox);
            this.Controls.Add(this.passwd_textbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mail_textbox);
            this.Controls.Add(this.name_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mail_textbox;
        private System.Windows.Forms.TextBox name_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox passwd_textbox;
        private System.Windows.Forms.TextBox passwd2_textbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}