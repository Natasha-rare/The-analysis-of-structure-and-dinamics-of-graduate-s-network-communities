using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SchoolProject
{
    public partial class Form1 : Form
    {
        //поля: которые меняет пользователь
        public string Hobby, Education;
        public string Clan, Year;
        public List<Person> people = new List<Person>();
        List<Shape> shapes = new List<Shape>();
        public bool is_admin = false;
        db a = new db("12345");
        
        public List<List<int>> fb_lines = new List<List<int>>();
        public List<List<int>> inst_lines = new List<List<int>>();
        public List<List<int>> vk_lines = new List<List<int>>();
        Person_Info Form_Info = null;
        LoginForm LoginForm = null;
        RegisterForm RegisterForm = null;
        //запрос
        public string query;
        public string Glasnye = "ауоиэыяюеёАУОИЭЫЯЮЕЁ";
        public Dictionary<string, string> ShortNames = new Dictionary<string, string> {
            {"Александр", "Саша"}, {"Артем", "Артем" }, {"Григорий", "Гоша"}, {"Дарья", "Даша"},
            {"Дмитрий", "Митя"}, {"Антонина", "Тоня"}, {"Димитрий", "Дима"},
            {"Алексей", "Леша"}, {"Сергей", "Сергей"}, {"Андрей", "Андрей"}, {"Михаил", "Миша"},
            {"Иван", "Иван"}, {"Никита", "Никита"}, {"Артём", "Артём"},
            {"Максим", "Макс"}, {"Илья", "Илья"}, {"Антон", "Антон"},
            {"Павел", "Паша"}, {"Николай", "Коля"}, {"Кирилл", "Киря"},
            {"Владимир", "Володя"}, {"Володя", "Вова"}, {"Константин", "Костя"}, {"Денис", "Денис"},
            {"Евгений", "Женя"}, {"Роман", "Рома"}, {"Даниил", "Даня"}, {"Игорь", "Игорь"},
            {"Егор", "Егор"}, {"Олег", "Олег"}, {"Петр", "Петр"},
            {"Василий", "Вася"}, {"Георгий", "Гоша"}, {"Виктор", "Витя"},
            {"Григор", "Гриша"}, {"Станислав", "Стас"}, {"Арсений", "Сеня"},
            {"Борис", "Боря"}, {"Леонид", "Лёня"}, {"Вадим", "Вадим"}, {"Глеб", "Глеб"},
            {"Юрий", "Юра"}, {"Федор", "Федя"}, {"Матвей", "Матвей"},
            {"Владислав", "Влад"}, {"Тимофей", "Тима"}, {"Вячеслав", "Слава"},
            {"Филипп", "Филя"}, {"Степан", "Степа"}, {"Всеволод", "Сева"},
            {"Анатолий", "Толя"}, {"Виталий", "Виталий"}, {"Ярослав", "Яра"},
            {"Тимур", "Тимур"}, {"Яков", "Яша"}, {"Марк", "Марк"}, {"Руслан", "Руся"},
            {"Семен", "Сема"}, {"Екатерина", "Катя"}, {"Анна", "Аня"},
            {"Анастасия", "Настя"}, {"Дария", "Даша"}, {"Мария", "Маша"},
            {"Елена", "Лена"}, {"Ольга", "Оля"}, {"Наталия", "Ната"}, {"Наталья", "Ната"},
            {"Татьяна", "Таня"}, {"Елизавета", "Лиза"},
            {"Александра", "Саня"}, {"Юлия", "Юля"},
            {"Евгения", "Женя"}, {"Ирина", "Ира"},
            {"София", "Соня"}, {"Полина", "Полина"}, {"Ксения", "Ксю"},
            {"Светлана", "Света"}, {"Марина", "Марина"}, {"Виктория", "Вика"},
            {"Надежда", "Надя"}, {"Варвара", "Варя"}, {"Маргарита", "Рита"}, {"Алина", "Лина"},
            {"Людмила", "Люда"}, {"Вероника", "Ника"}, {"Яна", "Яна"},
            {"Нина", "Нина"}, {"Лариса", "Лариса"}, {"Алёна", "Алёна"},
            {"Вера", "Вера"}, {"Алиса", "Алиса"}, {"Диана", "Диана"},
            {"Кристина", "Кристи"}, {"Любовь", "Люба"}, {"Галина", "Галя"},
            {"Оксана", "Оксана"}, {"Алла", "Алла"}, {"Алеся", "Алеся"},
            {"Алехандро", "Саша"}, {"Альберт", "Алик"}, {"Альбина", "Альб"},
            {"Амина", "Амина"}, {"Ана","Ана"}, {"Ангелина", "Геля"}, {"Анфиса", "Анфиса"},
            {"Арам", "Арам"}, {"Арина", "Арина"}, {"Аркадий", "Аркаша"},
            {"Арман", "Арман"}, {"Армен", "Армен"}, {"Арсен", "Арсен"},
            {"Артур", "Артур"}, {"Ася", "Ася"}, {"Ахмед", "Ахмед"},
            {"Ашот", "Ашот"}, {"Богдан", "Богдан"}, {"Валентин", "Валя"},
            {"Валентина", "Валя"}, {"Валерий", "Валера"}, {"Валерия", "Лера"},
            {"Валерьян", "Валера"}, {"Василиса", "Вася"}, {"Вениамин", "Веня"}, {"Весна", "Весна"}, {"Виолетта", "Вита"}, {"Гагик", "Гагик"}, {"Гаджимурад", "Гаджи"}, {"Гарик", "Гарик"}, {"Гарри", "Гарри"}, {"Геннадий", "Гена"},
            {"Герман", "Герман"}, {"Глафира", "Глаша"}, {"Гулру", "Гулру"}, {"Гульнара", "Гуля"},
            {"Давид", "Давид"}, {"Далия", "Далия"}, {"Дамир", "Дамир"}, {"Дарьюш", "Дарьюш"},
            {"Демид", "Демид"}, {"Демьян", "Демьян"}, {"Джамиля", "Джамиля"}, {"Диляра", "Диляра"}, {"Дина", "Дина"}, {"Ева", "Ева"},
            {"Евфросиния", "Фрося"}, {"Захар", "Захар"}, {"Зоя", "Зоя"}, {"Игнатий", "Игнат"}, {"Илай", "Илай"}, {"Илона", "Илона"}, {"Ильдар", "Ильдар"},
            {"Инесса", "Инесса"}, {"Инна", "Инна"}, {"Иннокентий", "Кеша"}, {"Иоанн", "Иоанн"}, {"Иосиф", "Иосиф"}, {"Камилла", "Камила"}, {"Карина", "Карина"},
            {"Кевин", "Кевин"}, {"Кира", "Кира"}, {"Кызы", "Кызы"}, {"Лаврентий", "Лаврик"}, {"Лада", "Лада"}, {"Лаура", "Лаура"}, {"Лев", "Лев"}, {"Левон", "Левон"}, {"Лейля", "Лейля"}, {"Лидия", "Лида"}, {"Лилия", "Лилия"},
            {"Линара", "Линара"}, {"Линда", "Линда"}, {"Лука", "Лука"}, {"Мадина", "Мадина"},
            {"Майя", "Майя"}, {"Марат", "Марат"}, {"Марианна", "Марья"}, {"Марьяна", "Марья"},
            {"Матин", "Матин"}, {"Мелисса", "Мелиса"}, {"Мерген", "Мерген"}, {"Мередкули", "Меред"}, {"Метревели", "Метре"}, {"Микаэл", "Микаэл"},
            {"Назар", "Назар"}, {"Наргиза", "Нарги"}, {"Нелли", "Нелли"}, {"Ника", "Ника"}, {"Николь", "Николь"}, {"Олеся", "Олеся"}, {"Регина", "Регина"}, {"Ренат", "Ренат"},
            {"Рината", "Рината"}, {"Роберт", "Роб"}, {"Родион", "Родион"}, {"Ростислав", "Ростик"}, {"Рубен", "Рубен"}, {"Рувин", "Рувин"}, {"Рустам", "Рустам"}, {"Сабина", "Саби"}, {"Саман", "Саман"},
            {"Саня", "Саня"}, {"Святослав", "Свят"}, {"Серафима", "Сима"}, {"Сослан", "Сослан"}, {"Сусанна", "Сана"}, {"Сяоган", "Сяоган"}, {"Таисия", "Тася"}, {"Тамара", "Тома"}, {"Тамерлан", "Тамер"}, {"Теймур", "Теймур"}, {"Тигран", "Тигран"}, {"Ульяна", "Уля"},
            {"Фаик", "Фаик"}, {"Фатима", "Фатима"}, {"Шамиль", "Шамиль"}, {"Шенне", "Шенне"}, {"Эвелина", "Лина"}, {"Эдуард", "Эдик"}, {"Элизабет", "Элиза"},
            {"Элина", "Элина"}, {"Элла", "Элла"}, {"Эльвира", "Эля"}, {"Эмиль", "Эмиль"},
            {"Эммануил", "Эмма"}, {"Юлий", "Юлий"}, {"Юнна", "Юнна"}, {"Юсиф", "Юсиф"},
            {"Ян", "Ян"},{"Ярослава", "Яра"}, {"Яфа", "Яфа"}, { "Аглая", "Аглая"}, {"Алена", "Алена"}, {"Софья", "Софья"},
            {"Дмитриан", "Дима" }, {"Аннели", "Аня"}, {"Данило", "Даня"}, {"Агнеса", "Агнеса"},{"Семён", "Семен"}
            , {"Арсентий", "Сеня"}, {"Артемий", "Артем"}, {"Мэтти", "Мэт"}, {"Фёдор", "Федя"}, {"Игнасио", "Игнас"}, {"Данила", "Даня"}, {"Пётр", "Пётр"},
            {"Агата", "Агата"}, {"Сото", "Сото"}, {"Моника", "Мони"}, {"Азамат", "Азамат"}, {"Айна", "Айна"}, {"Адиль", "Адиль"}, {"Аджай", "Аджай"},
            {"Нино", "Нино"}, {"Динара", "Дина"}, {"Даниял", "Даня"}
        };



        public Form1()
        {
            InitializeComponent();
            a.connect();
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
                this.login_main_btn.Visible = false;
                this.register_main_btn.Visible = false;
                this.label1.Visible = false;
                this.label2.Visible = false;
                this.info_txt.Visible = true;
                this.close_info.Visible = true;
                this.Visible = true;
                this.login_lbl.Text += LoginForm.Login;
                this.login_lbl.Visible = true;
                this.graduation.Visible = true;
                this.HobbyToolStripMenuItem.Visible = true;
                this.clanToolStripMenuItem.Visible = true;
                this.educationToolStripMenuItem.Visible = true;
                this.infobttn.Visible = true;
                this.clear.Visible = true;
                this.reload.Visible = true;
                this.search.Visible = true;
            }

        }

        private string Get_soglasnye(string word)
        {
            string result = "";
            for (int i = 0; i < word.Length; i++)
            {
                if (!Glasnye.Contains(word[i].ToString()))
                    result += word[i];
            }
            return result.Length > 4? result.Remove(4): result;
        }
        private string Short_Names(string name, string surname)
        {
            string result = $"{name} {surname}";
            try
            {
                if (name.Contains("."))
                { return result; }
                result = ShortNames[name] + " ";
                if (Glasnye.Contains(surname[0].ToString()))
                {
                    result += surname[0];
                    surname = Get_soglasnye(surname);
                    result += surname.Length > 3 ? surname.Remove(3) : surname;
                }
                else
                {
                    result += Get_soglasnye(surname);
                }
            }
            catch
            {
                Console.WriteLine($"name={name} surname={surname}");
            }

            return result;
        }


        private void Draw_People()
        {
            //a.connect();
            Random r = new Random();
            int x = 100;
            int y = 100;
            
            var results = a.GetPeople(Clan, Hobby, Education, Year);
            if (results.ToList().Count() == 0)
            {
                this.label2.Visible = true;
                this.label2.Text = "По вашему запросу ничего не найдено\n" +
                    "Попробуйте, пожалуйста, другой запрос";
            }
            else {
                this.label1.Visible = false;
                this.label2.Visible = false;
            }
            foreach (Person person in results)
            {
                if (!people.Contains(person))
                {
                    x = r.Next(50, 600);
                    y = r.Next(60, 500);
                    
                    shapes.Add(new Shape(x, y, Short_Names(person.First_name, person.Current_surname)));
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
                    .Set("per.Email = {email}").WithParam("email", person.Email)
                    .Set("per.Phone = {phone}").WithParam("phone", person.Phone)
                    .Set("per.Hobby = {hobby}").WithParam("hobby", person.Hobby)
                    .Set("per.Country = {country}").WithParam("country", person.Country)
                    .ExecuteWithoutResults();
            }
            public IEnumerable<Person> GetPeople(string Clan, string Hobby, string Education, string Year)
            {
                IEnumerable<Person> results;
                /*if (Clan != "")
                    results = client.Cypher.Match("per:Person").Where((Person per) => per.Clan == Clan)
*/
                if (Clan != "" && Clan != null)
                {
                    if (Education != "" && Education != null)
                    {
                        if (Hobby != "" && Hobby != null)
                        {
                            if (Year != "" && Year != null)
                            {
                                //if (Year.Contains("/")) -- добавление слешей при множественном выборе?
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per)=> per.Hobby.Contains(Hobby))
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .AndWhere((Person per) => (string)per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Hobby.Contains(Hobby))
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                        else
                        {
                            if (Year != "" && Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .AndWhere((Person per) => (string)per.Graduation == Year)
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
                        if (Hobby != "" && Hobby != null)
                        {
                            if (Year != "" && Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Hobby.Contains(Hobby))
                                    .AndWhere((Person per) => (string)per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => per.Hobby.Contains(Hobby))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                        else
                        {
                            if (Year != "" && Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Clan == Clan)
                                    .AndWhere((Person per) => (string)per.Graduation == Year)
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
                    if (Education != "" && Education != null)
                    {
                        if (Hobby != "" && Hobby != null)
                        {
                            if (Year != "" && Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)")
                                    .Where((Person per) => per.Hobby.Contains(Hobby))
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .AndWhere((Person per) => (string)per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Hobby.Contains(Hobby))
                                    .AndWhere((Person per) => per.Education.Contains(Education))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                        else
                        {
                            if (Year != "" && Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => per.Education.Contains(Education))
                                    .AndWhere((Person per) => (string)per.Graduation == Year)
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
                        if (Hobby != "" && Hobby != null)
                        {
                            if (Year != "" && Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)")
                                    .Where((Person per) => per.Hobby.Contains(Hobby))
                                    .AndWhere((Person per) => (string)per.Graduation == Year)
                                    .Return(per => per.As<Person>()).Results;
                            }
                            else
                            {
                                results = client.Cypher.Match("(per:Person)")
                                    .Where((Person per) => per.Hobby.Contains(Hobby))
                                    .Return(per => per.As<Person>()).Results;
                            }
                        }
                        else
                        {
                            if (Year != "" && Year != null)
                            {
                                results = client.Cypher.Match("(per:Person)").Where((Person per) => (string)per.Graduation == Year)
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
                
                Console.WriteLine(results.ToList().Count());
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
                g.DrawLine(new Pen(Color.Red), line[0], line[1], line[2], line[3]);
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
            Form_Info = new Person_Info(people[n], is_admin);
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

        private void Register()
        {
            RegisterForm = new RegisterForm();
            RegisterForm.ShowDialog();
            if (RegisterForm.DialogResult == DialogResult.OK)
            {
                Console.WriteLine($"login {RegisterForm.login} password {RegisterForm.password}");
                RegisterForm.Close();
                this.login_main_btn.Visible = false;
                this.register_main_btn.Visible = false;
                this.label1.Visible = false;
                this.label2.Visible = false;
                this.info_txt.Visible = true;
                this.close_info.Visible = true;
                this.Visible = true;
                this.login_lbl.Text += RegisterForm.login;
                this.login_lbl.Visible = true;
                this.graduation.Visible = true;
                this.HobbyToolStripMenuItem.Visible = true;
                this.clanToolStripMenuItem.Visible = true;
                this.educationToolStripMenuItem.Visible = true;
                this.infobttn.Visible = true;
                this.clear.Visible = true;
                this.reload.Visible = true;
                this.search.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            this.Invalidate();
        }

        private void graduation_Click(object sender, EventArgs e)
        {

        }

        private void infobttn_Click(object sender, EventArgs e)
        {
            info_txt.Visible = true;
            close_info.Visible = true;
        }

        private void close_info_Click(object sender, EventArgs e)
        {
            info_txt.Visible = false;
            close_info.Visible = false;
        }

        private void login_main_btn_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Login();
        }

        private void register_main_btn_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Register();
        }

        private void search_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Education);
            //query = $"MATCH (a:Person) WHERE a.Graduation CONTAINS '{Year}' AND a.Occupation CONTAINS '{Occupation}' AND a.Education CONTAINS '{Education}' AND a.Clan CONTAINS '{Clan}'";
            query = "";
            if (Year != "" && Year != null) query += $"Year = {Year}";
            if (Hobby != "" && Hobby != null) query += $" Hobby = {Hobby}";
            if (Education != "" && Education != null) query += $" Education = {Education}";
            if (Clan != "" && Clan != null) query += $" Clan = {Clan}";
            query = query.Trim();
            this.fb_l.Visible = true;
            this.vk_l.Visible = true;
            this.query_l.Visible = true;
            this.query_l.Text = query;
            Console.WriteLine(query);
            Draw_People();
            Refresh();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            query = "";
            this.fb_l.Visible = false;
            this.vk_l.Visible = false;
            this.query_l.Visible = false;
            people.Clear();
            shapes.Clear();
            fb_lines.Clear();
            vk_lines.Clear();
            inst_lines.Clear();
            Year = "";
            Clan = "";
            Hobby = "";
            Education = "";
            foreach (ToolStripMenuItem item in HobbyToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
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
