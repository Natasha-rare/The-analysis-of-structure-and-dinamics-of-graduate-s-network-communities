﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neo4jClient;
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
        db a = new db("12345");
        public List<List<int>> fb_lines = new List<List<int>>();
        public List<List<int>> inst_lines = new List<List<int>>();
        public List<List<int>> vk_lines = new List<List<int>>();
        //запрос
        public string query;
        public Form1()
        {
            
            InitializeComponent();
            a.connect();
        }


        private void Draw_People()
        {
            //a.connect();
            Random r = new Random();
            int x = 100;
            int y = 100;
            
            var results = a.GetPeople();
            foreach (Person person in results)
            {
                if (!people.Contains(person))
                {
                    x = r.Next(50, 600);
                    y = r.Next(50, 500);
                    shapes.Add(new Circle(x, y));
                    people.Add(person);
                }
            }
            Refresh_Connections();
            Refresh();
        }

        public void Refresh_Connections()
        {
            //a.connect();
            fb_lines.Clear();
            inst_lines.Clear();
            vk_lines.Clear();
            foreach (Person per1 in people)
            {
                var people2 = a.GetConnection(per1, 0);
                foreach (Person per2 in people2)
                {
                    Person p1 = Contains(per2);
                    if (p1 != per2)
                    {
                        List<int> lines = new List<int>();
                        lines.Add(shapes[people.IndexOf(per1)].X);
                        lines.Add(shapes[people.IndexOf(per1)].Y);
                        lines.Add(shapes[people.IndexOf(p1)].X);
                        lines.Add(shapes[people.IndexOf(p1)].Y);
                        fb_lines.Add(lines);
                    }
                }

                people2 = a.GetConnection(per1, 1);
                foreach (Person per2 in people2)
                {
                    Person p1 = Contains(per2);
                    if (p1 != per2)
                    {
                        List<int> lines = new List<int>();
                        lines.Add(shapes[people.IndexOf(per1)].X);
                        lines.Add(shapes[people.IndexOf(per1)].Y);
                        lines.Add(shapes[people.IndexOf(p1)].X);
                        lines.Add(shapes[people.IndexOf(p1)].Y);
                        vk_lines.Add(lines);
                    }
                }
                people2 = a.GetConnection(per1, 2);
                foreach (Person per2 in people2)
                {

                    Person p1 = Contains(per2);
                    if (p1 != per2)
                    {
                        List<int> lines = new List<int>();
                        lines.Add(shapes[people.IndexOf(per1)].X);
                        lines.Add(shapes[people.IndexOf(per1)].Y);
                        lines.Add(shapes[people.IndexOf(p1)].X);
                        lines.Add(shapes[people.IndexOf(p1)].Y);
                        inst_lines.Add(lines);
                    }

                }
            }
        }

        class db
        {
            GraphClient client;
            Dictionary<string, string> full_list;
            public db(string password)
            {
                client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", password);
                full_list = new Dictionary<string, string>();

            }
            public void connect()
            {
                try
                {
                    client.Connect();
                    Console.WriteLine("Connected");
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                }
            }

            public IEnumerable<Person> GetPeople()
            {
                var results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == "IT").Return(per => per.As<Person>()).Results;
                return results;
            }

            public IEnumerable<Person> GetConnections(Person person)
            {
                var results = client.Cypher.Match("(per:Person)-[r]->(per2:Person)").Where((Person per) => per.Name == person.Name).Return(per2 => per2.As<Person>()).Results;
                return results;
            }

            //connections of one social net: 0  - fb 1- vk 2 - inst
            public IEnumerable<Person> GetConnection(Person person1,  byte type)
            {
                IEnumerable<Person> results;
                switch (type)
                {
                    case 0:
                        results = client.Cypher.Match("(per1:Person)-[:FB_FRIENDS]->(per2:Person)")
                            .Where((Person per1) => per1.Name == person1.Name)
                            .Return(per2 => per2.As<Person>()).Results;
                        break;
                    case 1:
                        results = client.Cypher.Match("(per1:Person)-[:VK_FRIENDS]->(per2:Person)")
                            .Where((Person per1) => per1.Name == person1.Name)
                            .Return(per2 => per2.As<Person>()).Results;
                        break;
                    case 2:
                        results = client.Cypher.Match("(per1:Person)-[:IN_FRIENDS]->(per2:Person)")
                            .Where((Person per1) => per1.Name == person1.Name)
                            .Return(per2 => per2.As<Person>()).Results;
                        break;
                    default:
                        results = client.Cypher.Match("(per1:Person)-[:FB_FRIENDS]->(per2:Person)")
                            .Where((Person per1) => per1.Name == person1.Name)
                            .Return(per2 => per2.As<Person>()).Results;
                        break;
                }
                return results;
            }
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

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            
            foreach (Shape shape in shapes)
            {
                shape.Draw(g);
            }

            foreach (List<int> line in fb_lines)
            {
                g.DrawLine(new Pen(Color.Blue), line[0], line[1], line[2], line[3]);
            }

            foreach (List<int> line in vk_lines)
            {
                g.DrawLine(new Pen(Color.Black), line[0], line[1], line[2], line[3]);
            }

            foreach (List<int> line in inst_lines)
            {
                g.DrawLine(new Pen(Color.Red), line[0], line[1], line[2], line[3]);
            }
        }

        public Person Contains(Person p)
        {
            Person p2 = p;
            foreach (Person p1 in people)
            {
                if (p1.Name == p.Name) return p1;
            }
            return p2;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Shape figure in shapes)
            {
                figure.is_checked = false;
            }
            Refresh();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            bool flag_checked = false;
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                if (shapes[i].IsInside(e.X, e.Y))
                {
                    flag_checked = true;
                    shapes[i].is_checked = true;
                    shapes[i].D_X = e.X - shapes[i].X;
                    shapes[i].D_Y = e.Y - shapes[i].Y;
                    ShowInfo(i);
                }
            }
            if (!flag_checked)
            {
                this.info.Text = "";
            }
            Refresh_Connections();
            Refresh();
        }

        //info about person
        private void ShowInfo(int n)
        {
            Person person = people[n];
            string text = $"Name: {person.Name}\n" +
                $"Group: {person.Group} Graduation: {person.Graduation}\n" +
                $"Clan: {person.Clan} Project: {person.Project}\n Occupation:";
            if (Occupation != null)
            foreach (string i in person.Occupation)
            {
                text += " " + i;
            }
            if (Education != null)
            {
                text += "\nEducation: ";
                foreach (string i in person.Education)
                {
                    text += " " + i;
                }
            }
            
            this.info.Text = text;
            Refresh();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            bool flag_checked = false;
            foreach (Shape figure in shapes)
                if (figure.is_checked)
                {
                    flag_checked = true;
                    figure.X = e.X - figure.D_X;
                    figure.Y = e.Y - figure.D_Y;
                }
            Refresh_Connections();
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            this.Invalidate();
        }

        private void search_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Education);
            //query = $"MATCH (a:Person) WHERE a.Graduation CONTAINS '{Year}' AND a.Occupation CONTAINS '{Occupation}' AND a.Education CONTAINS '{Education}' AND a.Clan CONTAINS '{Clan}'";

            query = $"MATCH (a:Person) WHERE a.Graduation CONTAINS '{Year}' AND a.Clan CONTAINS '{Clan}'";
            query += " RETURN (a)";
            this.fb_l.Visible = true;
            this.vk_l.Visible = true;
            this.inst_l.Visible = true;
            Console.WriteLine(query);
            Draw_People();
            Refresh();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            query = "";
        }

        

    }
}
