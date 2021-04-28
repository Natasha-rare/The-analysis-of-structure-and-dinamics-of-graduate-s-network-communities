using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace SchoolProject
{
    public partial class Person_Info : Form
    {
        public Person person;
        public bool is_admin = false;
        public bool form_changed = false;
        LoginForm LoginForm = null;
        public Person_Info(Person person)
        {
            InitializeComponent();
            this.person = person;
        }

        private void Person_Info_Load(object sender, EventArgs e)
        {

        }

        private void Person_Info_Paint(object sender, PaintEventArgs e)
        {
            Lyceum_surname.Text = person.Lyceum_surname;
            Current_surname.Text = person.Current_surname;
            First_name.Text = person.First_name;
            patronym.Text = person.patronym;
            string text = "";
            if (person.Occupation.Count() > 0)
                foreach (string occup in person.Occupation)
                    text += occup + ", ";
            Occupation.Text = text.Length > 0? text.Substring(0, text.Length - 2): "-";
            fieldOfEducation.Text = person.FieldOfEducation.Count() > 0 && person.FieldOfEducation[0] != "0"? String.Join(",", person.FieldOfEducation):"-";
            Fb_name.Text = person.Fb_name.Count() > 1 ? person.Fb_name : "-";
            Vk_name.Text = person.Vk_name.Count() > 1 ? person.Vk_name : "-";
            Group.Text = person.Group;
            Graduation.Text = person.Graduation.Count() > 1? person.Graduation: "-";
            Project.Text = person.Project;
            Clan.Text = person.Clan;
            if (person.Other != null)
                Phone.Text = ((string)person.Other).Any(char.IsDigit) ? person.Other : "-";
            text = "";
            if (person.Education.Count() > 0)
                foreach (string edu in person.Education)
                    text += edu + ", ";
            Education.Text = text.Length > 0 ? text.Substring(0, text.Length - 2) : "-";
        }

        private void Login()
        {
            LoginForm = new LoginForm();
            LoginForm.ShowDialog();
            if (LoginForm.DialogResult == DialogResult.OK)
            {
                Console.WriteLine($"login {LoginForm.Login} password {LoginForm.Password}");
                if (LoginForm.Login == "admin" && LoginForm.Password == "Iamadm1n")
                {
                    is_admin = true;
                }
                LoginForm.Close();
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            Login();
            if (is_admin)
            {
                Current_surname.ReadOnly = false;
                Occupation.ReadOnly = false;
                fieldOfEducation.ReadOnly = false;
                Fb_name.ReadOnly = false;
                Vk_name.ReadOnly = false;
                Group.ReadOnly = false;
                Graduation.ReadOnly = false;
                Project.ReadOnly = false;
                Clan.Enabled = true;
                Education.ReadOnly = false;
                form_changed = true;
                LinkedIn_name.ReadOnly = false;
                Inst_name.ReadOnly = false;
                Telegram.ReadOnly = false;
                Phone.ReadOnly = false;
                email.ReadOnly = false;
                position.ReadOnly = false;
                hobbies.Enabled = true;
                this.saveBtn.Visible = true;
            }
        }

        private void Current_surname_TextChanged(object sender, EventArgs e)
        {
            person.Current_surname = Current_surname.Text;
        }

        private void Occupation_TextChanged(object sender, EventArgs e)
        {
            person.Occupation = Occupation.Text.Split(',').ToList<string>();
        }

        private void Fb_name_TextChanged(object sender, EventArgs e)
        {
            person.Fb_name = Fb_name.Text;
        }

        private void Vk_name_TextChanged(object sender, EventArgs e)
        {
            person.Vk_name = Vk_name.Text;
        }

        private void Group_TextChanged(object sender, EventArgs e)
        {
            person.Group = Group.Text;
        }

        private void Graduation_TextChanged(object sender, EventArgs e)
        {
            person.Graduation = Graduation.Text;
        }

        private void Project_TextChanged(object sender, EventArgs e)
        {
            person.Project = Project.Text;
        }

        private void Education_TextChanged(object sender, EventArgs e)
        {
            person.Education = Education.Text.Split(',').ToList<string>();
        }

        private void fieldOfEducation_TextChanged(object sender, EventArgs e)
        {
            person.FieldOfEducation = fieldOfEducation.Text.Split(',').ToList<string>();
        }

        private void Clan_SelectedIndexChanged(object sender, EventArgs e)
        {
            person.Clan = Clan.Text;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Current_surname.ReadOnly = true;
            Occupation.ReadOnly = true;
            fieldOfEducation.ReadOnly = true;
            Fb_name.ReadOnly = true;
            Vk_name.ReadOnly = true;
            Group.ReadOnly = true;
            Graduation.ReadOnly = true;
            Project.ReadOnly = true;
            Clan.Enabled = false;
            Education.ReadOnly = true;
            form_changed = false;
            LinkedIn_name.ReadOnly = true;
            Inst_name.ReadOnly = true;
            Telegram.ReadOnly = true;
            Phone.ReadOnly = true;
            email.ReadOnly = true;
            position.ReadOnly = true;
            hobbies.Enabled = false;
            Thread.Sleep(100);
            this.saveBtn.Visible = false;
        }

        private void LinkedIn_name_TextChanged(object sender, EventArgs e)
        {
            person.LinkedIn_name = LinkedIn_name.Text;
        }

        private void Inst_name_TextChanged(object sender, EventArgs e)
        {
            person.Inst_name = Inst_name.Text;
        }

        private void Telegram_TextChanged(object sender, EventArgs e)
        {
            person.Telegram = Telegram.Text;
        }

        private void Phone_TextChanged(object sender, EventArgs e)
        {
            person.Phone = Phone.Text;
        }

        private void email_TextChanged(object sender, EventArgs e)
        {
            person.Email = email.Text;
        }

        private void position_TextChanged(object sender, EventArgs e)
        {
            person.Position = position.Text.Split(',').ToList<string>();
        }

        private void hobbies_SelectedIndexChanged(object sender, EventArgs e)
        {
            person.Other = hobbies.Text;
        }
    }
}
