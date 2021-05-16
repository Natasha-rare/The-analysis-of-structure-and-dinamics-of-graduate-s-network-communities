
namespace SchoolProject
{
    partial class RegisterForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.namelbl = new System.Windows.Forms.Label();
            this.name_tb = new System.Windows.Forms.TextBox();
            this.login_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.patronym_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.surname_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.password_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.password_repeat_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.register_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(56, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(669, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Зарегестрируйтесь для использования приложения\r\n";
            // 
            // namelbl
            // 
            this.namelbl.AutoSize = true;
            this.namelbl.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namelbl.Location = new System.Drawing.Point(62, 95);
            this.namelbl.Name = "namelbl";
            this.namelbl.Size = new System.Drawing.Size(79, 32);
            this.namelbl.TabIndex = 1;
            this.namelbl.Text = "Имя*";
            // 
            // name_tb
            // 
            this.name_tb.BackColor = System.Drawing.SystemColors.Window;
            this.name_tb.Font = new System.Drawing.Font("Arial", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_tb.Location = new System.Drawing.Point(68, 141);
            this.name_tb.Name = "name_tb";
            this.name_tb.Size = new System.Drawing.Size(310, 32);
            this.name_tb.TabIndex = 2;
            // 
            // login_tb
            // 
            this.login_tb.Font = new System.Drawing.Font("Arial", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_tb.Location = new System.Drawing.Point(68, 476);
            this.login_tb.Name = "login_tb";
            this.login_tb.Size = new System.Drawing.Size(310, 32);
            this.login_tb.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(62, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Логин или e-mail*";
            // 
            // patronym_tb
            // 
            this.patronym_tb.Font = new System.Drawing.Font("Arial", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patronym_tb.Location = new System.Drawing.Point(68, 364);
            this.patronym_tb.Name = "patronym_tb";
            this.patronym_tb.Size = new System.Drawing.Size(310, 32);
            this.patronym_tb.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(316, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отчество (при наличии)";
            // 
            // surname_tb
            // 
            this.surname_tb.Font = new System.Drawing.Font("Arial", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.surname_tb.Location = new System.Drawing.Point(68, 255);
            this.surname_tb.Name = "surname_tb";
            this.surname_tb.Size = new System.Drawing.Size(310, 32);
            this.surname_tb.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(62, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 32);
            this.label4.TabIndex = 7;
            this.label4.Text = "Фамилия*";
            // 
            // password_tb
            // 
            this.password_tb.Font = new System.Drawing.Font("Arial", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_tb.Location = new System.Drawing.Point(68, 582);
            this.password_tb.Name = "password_tb";
            this.password_tb.PasswordChar = '*';
            this.password_tb.Size = new System.Drawing.Size(310, 32);
            this.password_tb.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(62, 536);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 32);
            this.label5.TabIndex = 9;
            this.label5.Text = "Пароль*";
            // 
            // password_repeat_tb
            // 
            this.password_repeat_tb.Font = new System.Drawing.Font("Arial", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_repeat_tb.Location = new System.Drawing.Point(68, 703);
            this.password_repeat_tb.Name = "password_repeat_tb";
            this.password_repeat_tb.PasswordChar = '*';
            this.password_repeat_tb.Size = new System.Drawing.Size(310, 32);
            this.password_repeat_tb.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(62, 657);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(253, 32);
            this.label6.TabIndex = 11;
            this.label6.Text = "Повторите пароль*";
            // 
            // register_btn
            // 
            this.register_btn.BackColor = System.Drawing.Color.Aquamarine;
            this.register_btn.Font = new System.Drawing.Font("Verdana", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.register_btn.Location = new System.Drawing.Point(490, 173);
            this.register_btn.Name = "register_btn";
            this.register_btn.Size = new System.Drawing.Size(266, 68);
            this.register_btn.TabIndex = 13;
            this.register_btn.Text = " Зарегистрироваться";
            this.register_btn.UseVisualStyleBackColor = false;
            this.register_btn.Click += new System.EventHandler(this.register_btn_Click);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 814);
            this.Controls.Add(this.register_btn);
            this.Controls.Add(this.password_repeat_tb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.password_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.surname_tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.patronym_tb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.login_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.name_tb);
            this.Controls.Add(this.namelbl);
            this.Controls.Add(this.label1);
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label namelbl;
        private System.Windows.Forms.TextBox name_tb;
        private System.Windows.Forms.TextBox login_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox patronym_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox surname_tb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox password_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox password_repeat_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button register_btn;
    }
}