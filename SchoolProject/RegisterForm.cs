using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolProject
{
    public partial class RegisterForm : Form
    {
        public string login = "";
        public string password = "";
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            if (name_tb.Text != "" && surname_tb.Text != ""
                && login_tb.Text != "" && password_tb.Text != "" && password_repeat_tb.Text != "")
            {
                if (password_repeat_tb.Text == password_tb.Text)
                {
                    Console.WriteLine("equal");
                    login = login_tb.Text;
                    password = password_tb.Text;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают. Пожалуйста, повторите ввод паролей.", "Password Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                if (name_tb.Text == "") name_tb.BackColor = Color.Red; 
                if (surname_tb.Text == "") surname_tb.BackColor = Color.Red; 
                if (login_tb.Text == "") login_tb.BackColor = Color.Red; 
                if (password_tb.Text == "") password_tb.BackColor = Color.Red; 
                if (password_repeat_tb.Text == "") password_repeat_tb.BackColor = Color.Red;
                MessageBox.Show("Вы заполнили не все обязательные поля. Пожалуйста, заполните все поля со звездчкой (*).", "Empty Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                name_tb.BackColor = Color.White;
                surname_tb.BackColor = Color.White;
                login_tb.BackColor = Color.White;
                password_tb.BackColor = Color.White;
                 password_repeat_tb.BackColor = Color.White;
            }
        }
    }
}
