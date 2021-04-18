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
    public partial class LoginForm : Form
    {
        public string login, password;

        private void LoginTB_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void PasswordTB_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            login = LoginTB.Text;
            password = PasswordTB.Text;
            Console.WriteLine($"login {login} password {password}");
            Refresh();
            //Close();
        }

        public LoginForm()
        {
            InitializeComponent();
        }

    }
}
