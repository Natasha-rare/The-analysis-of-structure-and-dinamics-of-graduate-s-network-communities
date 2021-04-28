using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolProject
{
    public partial class Form1 : Form
    {
        //поля: которые меняет пользователь
        public string Occupation, Education;
        public string Clan, Year;
        public List<Person> people = new List<Person>();
        List<Shape> shapes = new List<Shape>();
        db a = new db("12345");
        public List<List<int>> fb_lines = new List<List<int>>();
        public List<List<int>> inst_lines = new List<List<int>>();
        public List<List<int>> vk_lines = new List<List<int>>();
        Person_Info Form_Info = null;
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
            
            var results = a.GetPeople(Clan, Occupation, Education, Year);
            foreach (Person person in results)
            {
                if (!people.Contains(person))
                {
                    x = r.Next(50, 600);
                    y = r.Next(50, 500);
                    shapes.Add(new Shape(x, y, person.Name));
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

            public void Update(Person person)
            {
                client.Cypher.Match("(per:Person)").Where((Person per) => per.Name == person.Name)
                    .Set("per.Clan = {Clan}").WithParam("Clan", person.Clan)
                    .Set("per.Current_surname = {surname}").WithParam("surname", person.Current_surname)
                    .Set("per.Education = {education}").WithParam("education", person.Education)
                    .Set("per.Occupation = {occupation}").WithParam("occupation", person.Occupation)
                    .Set("per.FieldOfEducation = {edu}").WithParam("edu", person.FieldOfEducation)
                    .Set("per.Fb_name = {fb_name}").WithParam("fb_name", person.Fb_name)
                    .Set("per.Vk_name = {vk_name}").WithParam("vk_name", person.Vk_name)
                    .Set("per.Group = {group}").WithParam("group", person.Group)
                    .Set("per.Graduation = {grad}").WithParam("grad", person.Graduation)
                    .Set("per.Project = {project}").WithParam("project", person.Project)
                    .ExecuteWithoutResults();
            }
            public IEnumerable<Person> GetPeople(string Clan, string Occupation, string Education, string Year)
            {
                IEnumerable<Person> results;
                if (Clan != null)
                {
                    if (Education != null)
                    {
                        if (Occupation != null)
                        {
                            if (Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per)=> per.Occupation.Contains(Occupation))
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .AndWhere((Person per) => per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Occupation.Contains(Occupation))
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                        else
                        {
                            if (Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .AndWhere((Person per) => per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                    }
                    else
                    {
                        if (Occupation != null)
                        {
                            if (Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Occupation.Contains(Occupation))
                                    .AndWhere((Person per) => per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Occupation.Contains(Occupation))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                        else
                        {
                            if (Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                    }
                }
                else
                {
                    if (Education != null)
                    {
                        if (Occupation != null)
                        {
                            if (Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)")
                                    .Where((Person per) => per.Occupation.Contains(Occupation))
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .AndWhere((Person per) => per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Occupation.Contains(Occupation))
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                        else
                        {
                            if (Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Education.Contains(Education))
                                    .AndWhere((Person per) => per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Education.Contains(Education))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                    }
                    else
                    {
                        if (Occupation != null)
                        {
                            if (Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)")
                                    .Where((Person per) => per.Occupation.Contains(Occupation))
                                    .AndWhere((Person per) => per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)")
                                    .Where((Person per) => per.Occupation.Contains(Occupation))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                        else
                        {
                            if (Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)")
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                    }
                }
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
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;
            Year = e.ClickedItem.Text;
        }

        private void second_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;
            Year = e.ClickedItem.Text;
        }

        private void third_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;
            Year = e.ClickedItem.Text;
        }

        private void clanToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;
            Clan = e.ClickedItem.Text;
        }


        private void educationToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;
            string education = e.ClickedItem.Text.ToString();
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
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;
            Education = e.ClickedItem.Text;
        }

        private void нИУВШЭToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;
            Education = e.ClickedItem.Text;
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

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


            foreach (Shape shape in shapes)
            {
                shape.Draw(g);
            }

            if (Form_Info != null)
            {
                if (Form_Info.form_changed)
                {
                    a.Update(Form_Info.person);
                }
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
                else
                {
                    shapes[i].FillC = Color.LightPink;
                }
            }
            if (!flag_checked && Form_Info != null)
            {
                Form_Info.Close();
            }
            Refresh_Connections();
            Refresh();
        }

        //info about person
        private void ShowInfo(int n)
        {
            
            if (Form_Info == null || Form_Info.IsDisposed)
            {

            }
            else
            {
                Form_Info.Close();
            }
            shapes[n].FillC = Color.LightCyan;
            Form_Info = new Person_Info(people[n]);
            Form_Info.Show();
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

        private void graduation_Click(object sender, EventArgs e)
        {

        }

        private void search_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Education);
            //query = $"MATCH (a:Person) WHERE a.Graduation CONTAINS '{Year}' AND a.Occupation CONTAINS '{Occupation}' AND a.Education CONTAINS '{Education}' AND a.Clan CONTAINS '{Clan}'";

            query = $"MATCH (a:Person) WHERE a.Graduation CONTAINS '{Year}' AND a.Clan CONTAINS '{Clan}'";
            query += " RETURN (a)";
            this.fb_l.Visible = true;
            this.vk_l.Visible = true;
            Console.WriteLine(query);
            Draw_People();
            Refresh();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            query = "";
            people.Clear();
            shapes.Clear();
            fb_lines.Clear();
            vk_lines.Clear();
            inst_lines.Clear();
            foreach (ToolStripMenuItem item in clanToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
            foreach (ToolStripMenuItem item in educationToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
            foreach (ToolStripMenuItem item in first.DropDownItems)
            {
                item.Checked = false;
            }
            foreach (ToolStripMenuItem item in second.DropDownItems)
            {
                item.Checked = false;
            }
            foreach (ToolStripMenuItem item in third.DropDownItems)
            {
                item.Checked = false;
            }
            Refresh();
        }
    }
}
