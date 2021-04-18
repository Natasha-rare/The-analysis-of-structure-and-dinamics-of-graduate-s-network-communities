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
    public partial class Person_Info : Form
    {
        public Person person;
        public bool is_admin = true;
        public bool form_changed = false;
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
            fieldOfEducation.Text = person.FieldOfEducation.Count() > 0? String.Join(",", person.FieldOfEducation):"";
            Fb_name.Text = person.Fb_name;
            Vk_name.Text = person.Vk_name;
            Group.Text = person.Group;
            Graduation.Text = person.Graduation;
            Project.Text = person.Project;
            Clan.Text = person.Clan;
            text = "";
            if (person.Education.Count() > 0)
                foreach (string edu in person.Education)
                    text += edu + ", ";
            Education.Text = text.Length > 0 ? text.Substring(0, text.Length - 2) : "-";
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
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
                //Clan.ReadOnly = false;
                Education.ReadOnly = false;
            }
            form_changed = true;
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
    }
}
