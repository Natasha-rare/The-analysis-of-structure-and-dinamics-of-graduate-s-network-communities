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
    }
}
