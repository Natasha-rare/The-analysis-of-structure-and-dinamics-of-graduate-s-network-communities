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
        private string login, password;
        public bool clicked = false;

        private void LoginTB_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void PasswordTB_TextChanged(object sender, EventArgs e)
        {
            
        }

        public string Login
        {
            get { return this.login; }
        }

        public string Password
        {
            get { return this.password; }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            login = LoginTB.Text;
            password = PasswordTB.Text;
            this.DialogResult = DialogResult.OK;
        }

        public LoginForm()
        {
            InitializeComponent();
        }

    }
}
