using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShapeClass;

namespace SchoolProject
{
    public partial class Form1 : Form
    {
        //поля: которые меняет пользователь
        public int Year = 0;
        public string Occupation, Clan, Education;
        public List<Person> people = new List<Person>();
        List<Shape> shapes = new List<Shape>();
        //запрос
        public string query;
        public Form1()
        {
            InitializeComponent();
            
        }


        private void first_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.Text;
            Year = Convert.ToInt32(e.ClickedItem.Text);
        }

        private void second_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.Text;
            Year = Convert.ToInt32(e.ClickedItem.Text);
        }

        private void third_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.Text;
            Year = Convert.ToInt32(e.ClickedItem.Text);
        }

        private void clanToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Clan = e.ClickedItem.Text;
        }


        private void educationToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string education = e.ClickedItem.Text;
            switch (education)
            {
                case ("МГУ"):
                    break;
                case ("НИУ ВШЭ"):
                    break;
                default:
                    Education = education;
                    break;
            }
        }

        private void toolStripMenuItem32_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Education = e.ClickedItem.Text;
        }

        private void нИУВШЭToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Education = e.ClickedItem.Text;
        }

        private void search_Click(object sender, EventArgs e)
        {
           query = $"MATCH (a:Person) WHERE a.Year CONTAINS {Year} AND a.Occupation CONTAINS {Occupation} AND a.Education CONTAINS {Education} AND a.Clan CONTAINS {Clan}";
            query += "RETURN (a)";
            StartDrawing();
            
        }

        private void clear_Click(object sender, EventArgs e)
        {
            query = "";
        }

        private void StartDrawing()
        {
            int x = 20;
            int y = 20;
            var results = session.Run(query);
            foreach (Person person in results)
            {
                shapes.Add(new Circle(x, y));
                people.Add(person);
                x += 20;
                y += 10;
            }
        }

    }
}
