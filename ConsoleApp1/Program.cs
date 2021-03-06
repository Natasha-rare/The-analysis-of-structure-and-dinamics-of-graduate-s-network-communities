using Neo4jClient;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Excel = Microsoft.Office.Interop.Excel;
using OpenQA.Selenium.IE;
using System.Text.RegularExpressions;
using LumenWorks.Framework.IO.Csv;
using System.Data;
using System.IO;

namespace ConsoleApp1
{
    class Change_of_View
    {
        static void Main()
        {
            db a = new db("12345");
            a.connect();
            a.add_list("FB");
            /*for (int i =1993; i<=2020; i++)
            {
                a.from_vk_group(i.ToString());
            }*/
            //a.vk_friends_match();

            //a.add_from_fb_group();
            friends_searcher fr = new friends_searcher("id", a.Client);
            fr.Friends_Searh_two(a.Full_list);
            //a.Inst_Add();
            //a.add_from_inst_group();
            Console.WriteLine("Working");
        }

            static string[] Russian = {"КС","А","Б","В","Г","Д","Е","Ж", "З","И","Й","К","Л","М", "Н",
"О","П","Р","С","Т","У","Ф","Х", "Ц", "Ч", "Ш", "Щ",
"Э","Ю", "Я","ЬЕ","К","И","Ю","Й","В","И","ЕЙ","ИЕ","Ф","ИЙ","Ё","Х","Х","Т","АЙ"};
            static string[] English = {"X","A","B","V","G","D","E","ZH","Z","I","YO","K","L","M","N",
"O","P","R","S","T","U","F","KH","TS","CH","SH","SHCH",
"E","YU","YA","YE","C","Y","JU","J","FF","EE","EY","IE","PH","IY","YO","KH","H","TH","AI"};
            static Dictionary<string, string> names;
            string original;
            public Change_of_View(string original)
            {
                this.original = original;
                names = new Dictionary<string, string>();
                Add_Names();
                Changing();
            }
            static public string reverse(string original)
            {
                int index = original.IndexOf(" ");
                string name = original.Substring(0, index);
                original = original.Remove(0, index + 1);
                original += " " + name;
                return original;
            }
            static void Add_Names()
            {
                names.Add("ВАНЯ", "ИВАН");
                names.Add("АНЯ", "АННА");
                names.Add("АНН", "АННА");
                names.Add("МАША", "МАРИЯ");
                names.Add("ЖЕНЯ", "ЕВГЕНИЙ");
                names.Add("САША", "АЛЕКСАНДР");
                names.Add("КАТЯ", "ЕКАТЕРИНА");
                names.Add("СОНЯ", "СОФЬЯ");
                names.Add("НАСТЯ", "АНАСТАСИЯ");
                names.Add("ТАНЯ", "ТАТЬЯНА");
                names.Add("ЛИЗА", "ЕЛИЗАВЕТА");
                names.Add("ПОЛЯ", "ПОЛИНА");
                names.Add("ЛЕНА", "ЕЛЕНА");
                names.Add("ЮЛЯ", "ЮЛИЯ");
                names.Add("СВЕТА", "СВЕТЛАНА");
                names.Add("ЛЕРА", "ВАЛЕРИЯ");
                names.Add("ДАША", "ДАРЬЯ");
                names.Add("КСЮША", "КСЕНИЯ");
                names.Add("МАРИ", "МАРИЯ");
                names.Add("ВАРЯ", "ВАРВАРА");
                names.Add("ТЕМА", "АРТЕМ");
                names.Add("ЛЕША", "АЛЕКСЕЙ");
                names.Add("ДИМА", "ДМИТРИЙ");
                names.Add("ВОВА", "ВЛАДИМИР");
                names.Add("КОЛЯ", "НИКОЛАЙ");
                names.Add("СЕРЕЖА", "СЕРГЕЙ");
                names.Add("ВИТЯ", "ВИКТОР");
                names.Add("ЮРА", "ЮРИЙ");
                names.Add("АНДРЕИ", "АНДРЕЙ");
                names.Add("КАТЕРИНЕ", "ЕКАТЕРИНА");
                names.Add("АЛЕКСАНДЕР", "АЛЕКСАНДР");
                names.Add("КСУША", "КСЕНИЯ");
                names.Add("АЛЕКСИС", "АЛЕКСЕЙ");
                names.Add("МИЧАЕЛ", "МИХАИЛ");
                names.Add("ИЛЯ", "ИЛЬЯ");
                names.Add("АНАСТАСИА", "АНАСТАСИЯ");
                names.Add("ДАРИА", "ДАРЬЯ");
                names.Add("АЛЕКСЕИ", "АЛЕКСЕЙ");
                names.Add("ВИКТОРИА", "ВИКТОРИЯ");
                names.Add("АЛЕКС", "АЛЕКСАНДР");
                names.Add("АНИА", "АННА");
                names.Add("ОЛГА", "ОЛЬГА");
                names.Add("КАТИА", "ЕКАТЕРИНА");
                names.Add("СОФЯ", "СОФЬЯ");
                names.Add("ЛУСИА", "ЛЮДМИЛА");
                names.Add("ЛЮСЯ", "ЛЮДМИЛА");
                names.Add("НАТАЛЯ", "НАТАЛИЯ");
                names.Add("КАТЕ", "ЕКАТЕРИНА");
                names.Add("ТАТЯНА", "ТАТЬЯНА");
                names.Add("КАТРИН", "ЕКАТЕРИНА");
                names.Add("АЛЕКСЙ", "АЛЕКСЕЙ");
                names.Add("ЮЛИА", "ЮЛИЯ");
                names.Add("ВЛАД", "ВЛАДИСЛАВ");
                names.Add("НАТАЛИА", "НАТАЛЬЯ");
                names.Add("ТАТИАНА", "ТАТЬЯНА");
                names.Add("СЕРГЙ", "СЕРГЕЙ");
                names.Add("ДАРЯ", "ДАРЬЯ");
                names.Add("АНДРЙ", "АНДРЕЙ");
                names.Add("МИЛА", "ЛЮДМИЛА");
                names.Add("ИЛИЯ", "ИЛЬЯ");
                names.Add("ДМИТРИ", "ДМИТРИЙ");
                names.Add("ЮРИ", "ЮРИЙ");
                names.Add("МИКЕ", "МИХАИЛ");
                names.Add("ИЛИА", "ИЛЬЯ");
                names.Add("КАТ", "ЕКАТЕРИНА");
                names.Add("МАРИА", "МАРИЯ");
                names.Add("СОФИА", "СОФИЯ");
                names.Add("УЛИАНА", "УЛЬЯНА");
                names.Add("СЕРГЕ", "СЕРГЕЙ");
                names.Add("ЕУГЕНЕ", "ЕВГЕНИЙ");
                names.Add("ИГОР", "ИГОРЬ");
                names.Add("АНАСТАСИ", "АНАСТАСИЯ");
                names.Add("НАТАЛИ", "НАТАЛИЯ");
                names.Add("НАДЯ", "НАДЕЖДА");
                names.Add("СТАС", "СТАНИСЛАВ");
                names.Add("ХЕЛЕН", "ЕЛЕНА");
                names.Add("ФИЛ", "ФИЛИПП");
                names.Add("ЕЛИНА", "ЭЛИНА");
                names.Add("МИХАЙЛ", "МИХАИЛ");
                names.Add("ТОНИЙ", "ТОНИ");
                names.Add("АЛЕКСАНДРЕ", "АЛЕКСАНДР");
                names.Add("НИКК", "НИКОЛАЙ");
                names.Add("ДЕН", "ДЕНИС");
                names.Add("АНТ", "АНТОН");
                names.Add("ТИМ", "ТИМУР");
                names.Add("ЯША", "ЯКОВ");
                names.Add("МИКАЕЛ", "МИХАИЛ");
                names.Add("АНУТА", "АННА");
                names.Add("ДАНИЕЛ", "ДАНИИЛ");
                names.Add("ПАША", "ПАВЕЛ");
                names.Add("СЕРГИЕ", "СЕРГЕЙ");
                names.Add("СВЯТ", "СВЯТОСЛАВ");
                names.Add("ЛЕКС", "АЛЕКСЕЙ");
                names.Add("ЯНЕ", "ЯНА");
                names.Add("АНДРЕ", "АНДРЕЙ");
                names.Add("ЛИЗОН", "ЕЛИЗАВЕТА");
                names.Add("МИХАЕЛ", "МИХАИЛ");
                names.Add("НАСТИК", "АНАСТАСИЯ");
                names.Add("ОЛЯ", "ОЛЬГА");
                names.Add("ЬЕЛЕНА", "ЕЛЕНА");
                names.Add("САНЯ", "АЛЕКСАНДР");
                names.Add("ИРИНКА", "ИРИНА");
                names.Add("КСЕНИА", "КСЕНИЯ");
                names.Add("РОМА", "РОМАН");
                names.Add("ЛУДМИЛА", "ЛЮДМИЛА");
                names.Add("МАКС", "МАКСИМ");
                names.Add("МАРИША", "МАРИНА");
                names.Add("ГЕОРГЕ", "ГЕОГРИЙ");
                names.Add("МИТЯ", "ДМИТРИЙ");
                names.Add("МИША", "МИХАИЛ");
                names.Add("СЕРГЕИ", "СЕРГЕЙ");
                names.Add("ЛИУДМИЛА", "ЛЮДМИЛА");
                names.Add("ЛЮБОВ", "ЛЮБОВЬ");
                names.Add("НИКОЛАИЙ", "НИКОЛАЙ");
                names.Add("СТАНИС", "СТАНИСЛАВ");
                names.Add("ЕУГЕНИЯ", "ЕВГЕНИЯ");
                names.Add("ГЕНА", "ГЕННАДИЙ");
                names.Add("НАТАЛИЯ", "НАТАЛЬЯ");
                names.Add("СОФИЯ", "СОФЬЯ");
            }
            public string Result
            {
                get { return original; }
            }
            static public string change(string name)
            {
            name = Regex.Replace(name, @"\s+", " ");
            int index1, index2;
                if (name.Contains("("))
                {
                    index1 = name.IndexOf("(");
                    name = name.Remove(0, index1 + 1);
                    index2 = name.IndexOf(")");
                    name = name.Remove(index2, 1);
                }
                index1 = name.IndexOf(" ");
                index2 = name.LastIndexOf(" ");
                if (index1 != index2)
                {
                    name = name.Remove(index2 + 1, name.Length - index2 - 1);
                }
                int d = name.IndexOf(" ");
                if (d != -1)
                {
                    string s = name.Substring(0, d);
                    string b = name.Substring(d + 1, name.Count() - d - 1);
                    Change_of_View a = new Change_of_View(s);
                    s = a.Result;
                    a = new Change_of_View(b);
                    b = Regex.Replace(a.Result, @"\s+", "");
                    return s + " " + b;
                }
                return "";
            }
            void Changing()
            {
                original = original.ToUpper();
                to_Russian();
                int index = original.IndexOf("ЙА");
                if (index != -1)
                {
                    string tm = "";
                    for (int i = 0; i < index; i++)
                    {
                        tm += original[i];
                    }
                    tm += "Я";
                    index += 2;
                    for (int i = index; i < original.Length; i++)
                    {
                        tm += original[i];
                    }
                    original = tm;
                }
                Console.WriteLine(original);
                Short_To_Long();
                char a = original[0];
                string tmp = "";
                tmp += a;
                tmp += original.Substring(1, original.Count() - 1).ToLower();
                original = tmp;

            }
            void Short_To_Long()
            {
                if (names.ContainsKey(original))
                {
                    original = names[original];
                }
            }
            void to_Russian()
            {
                string rus = "";
                bool flag = true;
                for (int i = 0; i < original.Count(); i++)
                {
                    flag = true;
                    if ((i + 3 < original.Count()) && ("SHCH" == original.Substring(i, 4)))
                    {
                        rus += "Щ";
                        i += 3;
                        flag = false;
                    }
                    else if ((i + 2 < original.Count()) && ("EYE" == original.Substring(i, 3)))
                    {
                        rus += "ЕЕ";
                        i += 2;
                        flag = false;
                    }
                    else if (i + 1 < original.Count())
                    {
                        for (int j = 0; j < English.Count(); j++)
                        {
                            if (English[j].Count() == 2)
                            {

                                if (original.Substring(i, 2) == English[j])
                                {
                                    if ((English[j] == "EE") && (i + 2 < original.Count()) && (original[i + 2] == 'V')) rus += "ЕЕ";
                                    else rus += Russian[j];
                                    i += 1;
                                    flag = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (flag)
                    {
                        for (int j = 0; j < English.Count(); j++)
                        {
                            if (English[j].Count() == 1)
                            {
                                if (original.Substring(i, 1) == English[j])
                                {

                                    if ((English[j] == "C") && (i == original.Count() - 1)) rus += "Ч";
                                    else rus += Russian[j];
                                    if ((English[j] == "Y") && (i + 1 == original.Count())) rus += "Й";
                                    break;
                                }
                            }
                        }
                    }
                }
                if (rus != "") original = rus;
            }
            class vk_parser
            {
                static string email, password;
                static IWebDriver driver;
                string vk_name, id;
                static VkApi api;
                string name;
            bool logged = false;
                public vk_parser(string name)
                {
                    this.name = name;
                }
                public vk_parser(string id, string vk_name, string name)
                {
                    this.id = id;
                    this.vk_name = vk_name;
                    this.name = name;
                }
                static vk_parser()
                {
                    email = "89150040845";
                    password = "2182Rwc4";
                    ChromeOptions options = new ChromeOptions();
                    options.PageLoadStrategy = PageLoadStrategy.Eager;
                    options.AddArguments("--disable-notifications");
                    driver = new ChromeDriver(@"C:\Users\nattt\Downloads\chromedriver", options);
                driver.Navigate().GoToUrl("https://vk.com/search?c%5Bgroup%5D=44034");
                    login();
                }

                static void login()
                {
                    driver.Navigate().GoToUrl("https://vk.com/search?c%5Bgroup%5D=44034");
                    driver.FindElement(By.Id("search_query")).SendKeys(email + "\n");
                Thread.Sleep(100);
                    //driver.FindElement(By.Id("search_query")).SendKeys(Keys.Enter);
                    driver.FindElement(By.Id("email")).SendKeys(email);
                    driver.FindElement(By.Id("pass")).SendKeys(password);
                    driver.FindElement(By.Id("login_button")).Click();
                }
                public string Vk_name
                {
                    get { return vk_name; }
                }
                public string Id { get { return id; } }
                public void get_ids()
                {
                    Thread.Sleep(1000);
                    driver.Navigate().GoToUrl("https://vk.com/" + id);
                    driver.FindElement(By.ClassName("module_header")).Click();
                    Thread.Sleep(1000);
                    string url = driver.Url;
                    int index = url.IndexOf("id=") + 3;
                    int index2 = url.IndexOf("&", index);
                    id = url.Substring(index, index2 - index);
                }
                static public void vk_api(GraphClient client)
                {
                    api = new VkApi();

                    api.Authorize(new ApiAuthParams
                    {
                        ApplicationId = 6310764,
                        Login = "89150040845",
                        Password = "2182Rwc4",
                        Settings = VkNet.Enums.Filters.Settings.All
                    });

                    var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Vk_name != "0").Return(per => per.As<Person>()).Results;
                    foreach (Person x in people)
                    {
                    Console.WriteLine(x.Name);
                        try
                        {
                            var users = api.Friends.Get((new FriendsGetParams { UserId = long.Parse(x.Vk_id) }));
                            foreach (var y in users)
                            {
                                string id1 = y.Id.ToString();
                                var query = client.Cypher
            .Match("(friend1:Person)", "(friend2:Person)")
                                       .Where((Person friend1) => friend1.Vk_id == x.Vk_id)
                                                         .AndWhere((Person friend2) => friend2.Vk_id == id1)
            .CreateUnique("(friend1)-[:VK_FRIENDS]->(friend2)");
                                query.ExecuteWithoutResults();
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        
                    }
                }
                public void login_extra()
                {
                    driver.Navigate().GoToUrl("https://vk.com/search?c%5Bgroup%5D=44034");
                    
                    logged = true;
                    get_from_group();
                }
                public void get_from_group()
                {
                    driver.FindElement(By.Id("search_query")).SendKeys(name + "\n");
                    driver.FindElement(By.Id("search_query")).SendKeys(Keys.Enter);
                    Thread.Sleep(300);
                    if (driver.PageSource.Contains("Ваш запрос не дал результатов"))
                    { id = "0"; vk_name = "0"; }
                    else
                    {
                    try
                    {
                        int search_after = driver.PageSource.IndexOf("people_row search_row clear_fix");
                        int index1 = driver.PageSource.IndexOf("a href=", search_after) + 9;
                        int index2 = driver.PageSource.IndexOf("onclick", index1) - 2;

                        id = driver.PageSource.Substring(index1, index2 - index1);
                        index1 = driver.PageSource.IndexOf("alt=", index2) + 5;
                        index2 = driver.PageSource.IndexOf("></a>", index1) - 1;
                        vk_name = driver.PageSource.Substring(index1, index2 - index1);
                    }
                    catch { }
                    finally
                    {
                    }
                    }
                    driver.FindElement(By.Id("search_query")).Clear();
                }
            }
            public class Person
            {
                [JsonProperty("Clan")]
                public string Clan { get; set; }
                [JsonProperty("Graduation")]
                public string Graduation { get; set; }
                [JsonProperty("Project")]
                public string Project { get; set; }
                [JsonProperty("Fb_name")]
                public string Fb_name { get; set; }
                [JsonProperty("Fb_id")]
                public string Fb_id { get; set; }
                [JsonProperty("Name")]
                public string Name { get; set; }
                [JsonProperty("Group")]
                public string Group { get; set; }
                [JsonProperty("Occupation")]
                public List<string> Occupation { get; set; }
                [JsonProperty("Education")]
                public List<string> Education { get; set; }
                [JsonProperty("FieldOfEducation")]
                public List<string> FieldOfEducation { get; set; }
                [JsonProperty("Position")]
                public List<string> Position { get; set; }
                [JsonProperty("patronym")]
                public String patronym { get; set; }
                [JsonProperty("First_name")]
                public string First_name { get; set; }
                [JsonProperty("Lyceum_surname")]
                public string Lyceum_surname { get; set; }
                [JsonProperty("Current_surname")]
                public string Current_surname { get; set; }
                [JsonProperty("Vk_name")]
                public string Vk_name { get; set; }
                [JsonProperty("Vk_id")]
                public string Vk_id { get; set; }
                [JsonProperty("Inst_name")]
                public string Inst_name { get; set; }
                [JsonProperty("Inst_id")]
                public string Inst_id { get; set; }
                [JsonProperty("Inst_nick")]
                public string Inst_nick { get; set; }
        }
        class db
        {
            GraphClient client;
            Dictionary<string, string> full_list;
            string currentWindow;
            public Dictionary<string, string> Full_list
            {
                get { return this.full_list; }
            }
            public db(string password)
            {
                client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "12345");
                full_list = new Dictionary<string, string>();

            }

            public GraphClient Client
            {
                get { return this.client; }
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
            public void stop()
            {

            }
            public void Inst_Add()
            {
                string id, nickname, name;
                var csvTable = new DataTable();
                using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"C:\Users\nattt\project\All_users_lit1533_official_20210509_1000.csv")), true))
                {
                    csvTable.Load(csvReader);
                }
                add_list("INST");
                for (int i = 0; i < csvTable.Rows.Count; i++)
                {
                    id = csvTable.Rows[i][0].ToString();
                    nickname = csvTable.Rows[i][1].ToString();
                    name = csvTable.Rows[i][2].ToString();
                    if (name == "" || name == null) continue;
                    var people = client.Cypher.Match("(per:Person)")
            .Where((Person per) => per.Inst_name == name).Return(per => per.As<Person>()).Results;
                    if (people.Count() == 0)
                    {
                        var people2 = client.Cypher.Match("(per:Person)")
.Where((Person per) => per.Inst_id == id).Return(per => per.As<Person>()).Results;
                        if (people2.Count() == 0)
                        {
                            string name2 = Change_of_View.change(name);
                            if (name2 == "") continue;
                            name2 = Change_of_View.reverse(name2);
                            if (full_list.ContainsKey(name2) || full_list.ContainsValue(name2))
                            {
                                string p_name = full_list[name2];
                                Console.WriteLine(full_list[name2]);
                                var person = client.Cypher.Match("(per:Person)")
                                .Where((Person per) => per.Name == p_name).Return(per => per.As<Person>()).Results;
                                client.Cypher.Match("(per:Person)")
                                .Where((Person per) => per.Name == p_name)
                                .Set("per.Inst_name = {name}").WithParam("name", name).ExecuteWithoutResults();
                                client.Cypher.Match("(per:Person)")
        .Where((Person per) => per.Name == p_name).Set("per.Inst_id = {id}").WithParam("id", id).ExecuteWithoutResults();
                                client.Cypher.Match("(per:Person)").Where((Person per) => per.Name == p_name)
                                .Set("per.Inst_nick = {nick}").WithParam("nick", nickname).ExecuteWithoutResults();

                                Console.WriteLine("+" + name2);
                            }
                            else
                            {
                                /* Person person = new Person { Name = "0", Fb_name = name, Graduation = "0", Project = "0", Fb_id = id, Group = "0" };
                                 client.Cypher.Create("(per:Person {person})").WithParam("person", person).ExecuteWithoutResults();*/
                                Console.WriteLine("-" + name2);
                                Console.WriteLine("new was created");
                            }

                            /*var people1 = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_id == id).Return(per => per.As<Person>()).Results;
                            foreach (Person x in people1) add_work(x);*/
                        }
                        else { }
                    }
                    else
                    {
                        friends_searcher_inst fr_search = new friends_searcher_inst(nickname, client);
                        fr_search.Friends_Searh_two();
                    }
                    /* catch (Exception e) { Console.WriteLine(e); continue; }*/
                }
                Console.WriteLine(csvTable.Rows[1][0].ToString());
                //
            }
            public void IT_Clan()

            {
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_name != "0").Return(per => per.As<Person>()).Results;
                string pathToFile = @"C:\Users\nattt\Documents\IT.xlsx";
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                //Открываем книгу.                                                                                                                                                        
                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(pathToFile, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                //Выбираем таблицу(лист).
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                // Указываем номер столбца (таблицы Excel) из которого будут считываться данные.
                int numCol = 1;
                Microsoft.Office.Interop.Excel.Range usedColumn = (Excel.Range)ObjWorkSheet.UsedRange.Columns[numCol];
                System.Array myvalues = (System.Array)usedColumn.Cells.Value2;
                string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();

                ObjExcel.Quit();
                foreach (Person x in people)
                {
                    for (int i = 0; i < 73; i++)
                    {
                        if (x.Name == strArray[i])
                            client.Cypher.Match("(per:Person)")
                               .Where((Person per) => per.Name == x.Name).Set("per.Clan = {IT}").WithParam("IT", x.Clan).ExecuteWithoutResultsAsync()
                               .Wait();
                    }
                }
            }
            public void Media_Clan()

            {
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_name != "0").Return(per => per.As<Person>()).Results;
                string pathToFile = @"C:\Users\nattt\Documents\Media.xlsx";
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                //Открываем книгу.                 
                Console.WriteLine("Opened");
                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(pathToFile, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                //Выбираем таблицу(лист).
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                Console.WriteLine(ObjWorkSheet);
                // Указываем номер столбца (таблицы Excel) из которого будут считываться данные.
                int numCol = 1;
                Console.WriteLine(ObjWorkSheet.UsedRange.Columns.Count);
                Microsoft.Office.Interop.Excel.Range usedColumn = (Excel.Range)ObjWorkSheet.UsedRange.Columns[numCol];
                System.Array myvalues = (System.Array)usedColumn.Cells.Value2;
                string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();

                ObjExcel.Quit();
                foreach (Person x in people)
                {
                    for (int i = 0; i < 70; i++)
                    {
                        if (x.Name == strArray[i])
                            client.Cypher.Match("(per:Person)")
                               .Where((Person per) => per.Name == x.Name).Set("per.Clan = {Media}").WithParam("Media", x.Clan).ExecuteWithoutResultsAsync()
                               .Wait();
                    }
                }
            }
            public void Research_Clan()

            {
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_name != "0").Return(per => per.As<Person>()).Results;
                string pathToFile = @"C:\Users\nattt\Documents\Research.xlsx";
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                //Открываем книгу.                                                                                                                                                        
                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(pathToFile, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                //Выбираем таблицу(лист).
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                // Указываем номер столбца (таблицы Excel) из которого будут считываться данные.
                int numCol = 1;
                Microsoft.Office.Interop.Excel.Range usedColumn = (Excel.Range)ObjWorkSheet.UsedRange.Columns[numCol];
                System.Array myvalues = (System.Array)usedColumn.Cells.Value2;
                string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();

                ObjExcel.Quit();
                foreach (Person x in people)
                {
                    for (int i = 0; i < 67; i++)
                    {
                        if (x.Name == strArray[i])
                            client.Cypher.Match("(per:Person)")
                               .Where((Person per) => per.Name == x.Name).Set("per.Clan = {Research}").WithParam("Research", x.Clan).ExecuteWithoutResultsAsync()
                               .Wait();
                    }
                }
            }
            public void get_vk_id()
            {
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Vk_name != "0").Return(per => per.As<Person>()).Results;
                foreach (Person x in people)
                {
                    int num;
                    if (!(int.TryParse(x.Vk_id, out num)))
                    {
                        vk_parser a = new vk_parser(x.Vk_id, x.Vk_name, x.Name);
                        a.get_ids();
                        x.Vk_id = a.Id;
                        client.Cypher.Match("(per:Person)")
                                .Where((Person per) => per.Name == x.Name).Set("per.Vk_id = {Vk_id}").WithParam("Vk_id", x.Vk_id).ExecuteWithoutResultsAsync()
                                .Wait();
                    }
                }
            }
            public void from_vk_group(string graduation_year)
            {
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Graduation.ToString() == graduation_year).Return(per => per.As<Person>()).Results;
                //var first = people.First();
                foreach (Person x in people)
                {
                    if (x.Vk_id == "0" || x.Vk_id == null || x.Vk_id == "")
                    {
                        string name = x.First_name + " " + x.Current_surname;
                        vk_parser tmp = new vk_parser(name);
                        tmp.get_from_group();
                        x.Vk_id = tmp.Id;
                        x.Vk_name = tmp.Vk_name;
                        if (tmp.Id != "0")
                        {
                            Console.WriteLine("Woe");
                        }
                        client.Cypher.Match("(per:Person)")
                              .Where((Person per) => per.Name == x.Name).Set("per.Vk_name = {Vk_name}").WithParam("Vk_name", x.Vk_name).ExecuteWithoutResultsAsync()
                              .Wait();
                        client.Cypher.Match("(per:Person)")
                              .Where((Person per) => per.Name == x.Name).Set("per.Vk_id = {Vk_id}").WithParam("Vk_id", x.Vk_id).ExecuteWithoutResultsAsync()
                              .Wait();
                    }
                }

            }

            public void fb_friends_match()
            {
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_name != "0").Return(per => per.As<Person>()).Results;
                int start = 0;
                List<Person> my_list = people.ToList();
                foreach (Person i in my_list)
                {
                    if (i.Fb_name != "Mikhail Kryu")
                    {
                        continue;
                    }
                    else
                    {
                        start = my_list.IndexOf(i);
                    }
                }
                for (int f = start; f < my_list.Count(); f++)
                {
                    Person x = my_list[f];
                    if (x.Fb_id != "0")
                    {
                        friends_searcher a = new friends_searcher(x.Fb_id.ToString(), client);
                        a.Friends_Searh_two(full_list);
                    }
                }
            }

            public void vk_friends_match()
            {
                vk_parser.vk_api(client);
            }
            public void friend_into_txt()
            {
                string line = "";
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_name != "0").Return(per => per.As<Person>()).Results;
                string[] lines = new string[25];
                for (int i = 1993; i <= 2017; i++)
                {
                    line = "";
                    line += i.ToString() + ": ";
                    foreach (Person x in people)
                    {
                        if (x.Graduation == i.ToString())
                        {
                            line += x.Name + ", ";
                            var number = client.Cypher.OptionalMatch("(person:Person)-[FB_FRIENDS]-(friend:Person)").Where((Person person) => person.Name == x.Name).Return((person, friend) => new { number = friend.Count() }).Results;
                            long number1 = 0;
                            foreach (var x1 in number)
                            {
                                number1 = x1.number;
                            }
                            line += number1.ToString() + "; ";
                        }
                    }
                    lines[i - 1993] = line;
                }
                System.IO.File.WriteAllLines(@"text4.txt", lines);
            }
            public void separate_names()
            {
                var people = client.Cypher.Match("(per:Person)").Return(per => per.As<Person>()).Results;
                foreach (Person x in people)
                {
                    string full_name = x.Name;
                    if (full_name != "0" && full_name != null)
                    {
                        string current_surname, lyceum_surname, name, otchestvo;
                        int index2;
                        if (full_name.Contains("("))
                        {
                            int index1 = full_name.IndexOf("(");
                            index2 = full_name.IndexOf(")");
                            current_surname = full_name.Substring(index1 + 1, index2 - index1 - 1);
                            lyceum_surname = full_name.Substring(0, index1 - 1);
                            index2 += 1;
                        }
                        else
                        {
                            int index1 = full_name.IndexOf(" ");
                            index2 = index1;


                            current_surname = full_name.Substring(0, index1);
                            lyceum_surname = current_surname;
                        }
                        int index = full_name.IndexOf(" ", index2 + 1);
                        if (index == -1) index = full_name.Length;
                        name = full_name.Substring(index2 + 1, index - index2 - 1);
                        if (index + 1 < full_name.Length)
                        {
                            otchestvo = full_name.Substring(index + 1, full_name.Length - index - 1);

                        }
                        else otchestvo = "0";
                        client.Cypher.Match("(per:Person)")
                              .Where((Person per) => per.Name == full_name).Set("per.Current_surname = {cur_sur}").WithParam("cur_sur", current_surname).ExecuteWithoutResultsAsync();
                        client.Cypher.Match("(per:Person)")
                              .Where((Person per) => per.Name == full_name).Set("per.Lyceum_surname= {lyc_sur}").WithParam("lyc_sur", lyceum_surname).ExecuteWithoutResultsAsync();
                        client.Cypher.Match("(per:Person)")
                           .Where((Person per) => per.Name == full_name).Set("per.First_name = {nam}").WithParam("nam", name).ExecuteWithoutResultsAsync();
                        client.Cypher.Match("(per:Person)")
                              .Where((Person per) => per.Name == full_name).Set("per.patronym = {patronym}").WithParam("patronym", otchestvo).ExecuteWithoutResultsAsync();
                    }
                }
            }
            public void add_works_for_new()
            {
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_name != "0").Return(per => per.As<Person>()).Results;
                foreach (Person x in people)
                {
                    add_work(x);
                }
            }
            public void add_work(Person x)
            {
                if (x.Education.Count() == 0) x.Education.Add("0");
                if (x.Occupation.Count() == 0) x.Occupation.Add("0");
                if (x.Position.Count() == 0) x.Position.Add("0");
                if (x.FieldOfEducation.Count() == 0) x.FieldOfEducation.Add("0");
                if ((x.Education[0] == "0") && (x.Position[0] == "0") && (x.Occupation[0] == "0") && (x.FieldOfEducation[0] == "0"))
                {
                    facebook_parser a = new facebook_parser(x.Fb_id.ToString());
                    a.Page_load();
                    client.Cypher.Match("(per:Person)")
                          .Where((Person per) => per.Name == x.Name).Set("per.Occupation = {Occupation}").WithParam("Occupation", a.Occupation).ExecuteWithoutResultsAsync();
                    client.Cypher.Match("(per:Person)")
                          .Where((Person per) => per.Name == x.Name).Set("per.Position = {Position}").WithParam("Position", a.Position).ExecuteWithoutResultsAsync();
                    client.Cypher.Match("(per:Person)")
                          .Where((Person per) => per.Name == x.Name).Set("per.Education = {Education}").WithParam("Education", a.Education).ExecuteWithoutResultsAsync();
                    client.Cypher.Match("(per:Person)")
                          .Where((Person per) => per.Name == x.Name).Set("per.FieldOfEducation = {FieldOfEducation}").WithParam("FieldOfEducation", a.FieldOfEducation).ExecuteWithoutResultsAsync();
                }
            }

            public void add_fb_id()
            {
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_id == "0").Return(per => per.As<Person>()).Results;
                foreach (Person x in people)
                {
                    if (x.Fb_name != "0")
                    {
                        string id = refresher.return_id(x.Fb_name);
                        client.Cypher.Match("(per:Person)")
        .Where((Person per) => per.Name == x.Name).Set("per.Fb_id = {id}").WithParam("id", id).ExecuteWithoutResultsAsync();
                        friends_searcher a = new friends_searcher(x.Fb_id.ToString(), client);
                        a.Friends_Searh_two(full_list);
                        add_work(x);

                    }
                }
                //separate_names();
            }
                public void add_list(string flag)
                {
                    IEnumerable<Person> people;
                    switch (flag)
                    {
                        case "FB":
                            people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_name == "0").Return(per => per.As<Person>()).Results;
                            break;
                        case "INST":
                            people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Inst_name == "").Return(per => per.As<Person>()).Results;
                            break;
                        default:
                            people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_name == "0").Return(per => per.As<Person>()).Results;
                            break;
                    }
                    string name = "";
                    foreach (Person x in people)
                    {
                        if (x.Name != "0" && x.Name != null)
                        {
                            try
                            {
                                name = Change_of_View.change(x.Name);
                                if (!full_list.ContainsKey(name))
                                    full_list.Add(name, x.Name);
                            }
                            catch (Exception error)
                            {
                                Console.WriteLine(error);
                            }
                        }
                    }
                /*foreach(string i in full_list.Keys)
                {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine(full_list[i]);
                }*/
                //PZuss
            }

            public string inst_name_by_id(string link, IJavaScriptExecutor js)
            {
                //inst_parser.driver.Navigate().GoToUrl(link);
                var windowHandles = inst_parser.driver.WindowHandles;
                js.ExecuteScript(string.Format("window.open('{0}', '_blank');", link));
                var newWindowHandles = inst_parser.driver.WindowHandles;
                var openedWindowHandle = newWindowHandles.Except(windowHandles).Single();
                inst_parser.driver.SwitchTo().Window(openedWindowHandle);
                //inst_parser.driver.SwitchTo().Window(link);
                string name = inst_parser.driver.FindElement(By.CssSelector("h1")).Text;
                //inst_parser.driver.Navigate().GoToUrl("https://www.instagram.com/lit1533_official/followers/");
                inst_parser.driver.SwitchTo().Window(currentWindow);
                return name;
            }

            public void add_from_inst_group()
            {
                inst_parser.driver.Navigate().GoToUrl("https://www.instagram.com/lit1533_official/followers/");
                Thread.Sleep(600);
                Console.WriteLine("Inst_work");
                inst_parser.driver.FindElement(By.CssSelector("a.-nal3")).Click();
                Thread.Sleep(200);
                //inst_parser.driver.Navigate().GoToUrl("https://www.instagram.com/lit1533_official/followers");
                var elements = inst_parser.driver.FindElement(By.ClassName("PZuss"));
                var index = elements.FindElements(By.CssSelector("li"));
                IJavaScriptExecutor js = (IJavaScriptExecutor)inst_parser.driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", elements);
                add_list("INST");
                int count = 0;
                do
                {
                    currentWindow = inst_parser.driver.CurrentWindowHandle;
                    for (int i = count; i < index.Count(); i ++)
                    {
                        var t = index[i].FindElements(By.TagName("a"));
                        try
                        {
                            var student1 = index[i].FindElements(By.TagName("a"))[0];
                            string link = student1.GetAttribute("href");
                            string id = link.Split('/')[3];
                            string name = index[i].FindElement(By.CssSelector("div._7UhW9.xLCgt.MMzan._0PwGv.fDxYl")).Text;
                                //.FindElements(By.TagName("div"))[1].FindElements(By.TagName("div"))[0].GetAttribute("aria-label");
                            string n2 = index[i].FindElement(By.CssSelector("div._7UhW9.xLCgt.MMzan._0PwGv.fDxYl")).Text;
                            if (name == null) continue;
                            Console.WriteLine(name);

                            Console.WriteLine("added");
                            var people = client.Cypher.Match("(per:Person)")
            .Where((Person per) => per.Inst_name == name).Return(per => per.As<Person>()).Results;
                            if (people.Count() == 0)
                            {
                                var people2 = client.Cypher.Match("(per:Person)")
        .Where((Person per) => per.Inst_id == id).Return(per => per.As<Person>()).Results;
                                if (people2.Count() == 0)
                                {
                                    string name2 = Change_of_View.change(name);
                                    name2 = Change_of_View.reverse(name2);
                                    if (full_list.ContainsKey(name2) || full_list.ContainsValue(name2))
                                    {
                                        string p_name = full_list[name2];
                                        Console.WriteLine(full_list[name2]);
                                        var person = client.Cypher.Match("(per:Person)")
                                        .Where((Person per) => per.Name == p_name).Return(per => per.As<Person>()).Results;
                                        client.Cypher.Match("(per:Person)")
                                        .Where((Person per) => per.Name == p_name)
                                        .Set("per.Inst_name = {name}").WithParam("name", name).ExecuteWithoutResults();
                                        client.Cypher.Match("(per:Person)")
                .Where((Person per) => per.Name == p_name).Set("per.Inst_id = {id}").WithParam("id", id).ExecuteWithoutResults();

                                        Console.WriteLine("+" + name2);
                                    }
                                    else
                                    {
                                        /* Person person = new Person { Name = "0", Fb_name = name, Graduation = "0", Project = "0", Fb_id = id, Group = "0" };
                                         client.Cypher.Create("(per:Person {person})").WithParam("person", person).ExecuteWithoutResults();*/
                                        Console.WriteLine("-" + name2);
                                        Console.WriteLine("new was created");
                                    }

                                    /*var people1 = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_id == id).Return(per => per.As<Person>()).Results;
                                    foreach (Person x in people1) add_work(x);*/
                                }
                                else
                                {
                                    /*friends_searcher fr_search = new friends_searcher(id, client);
                                    fr_search.Friends_Searh_two();*/
                                }
                            }
                            /*friends_searcher fr = new friends_searcher(id, client);
                            fr.Friends_Searh_two();*/
                            //separate_names();
                        }
                        catch (Exception e)
                        { Console.WriteLine(e); continue; }
                    }
                    count = index.Count() - 1;
                    js.ExecuteScript("arguments[0].scrollTop += 200", elements);
                    index = elements.FindElements(By.CssSelector("li"));
                } while (index != null);
            }
            public void add_from_fb_group()
            {
                facebook_parser.driver.Navigate().GoToUrl("https://www.facebook.com/groups/1584209605191960/members/");
                Console.WriteLine("add_from_connected");
                var elements = facebook_parser.driver.FindElements(By.ClassName("b20td4e0"));
                var index = elements[elements.Count() - 1];
                IJavaScriptExecutor js = (IJavaScriptExecutor)facebook_parser.driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", index);
                var student = index.FindElements(By.ClassName("nc684nl6"));
                add_list("FB");
                int count = 0;
                do
                {
                    for (int i = count; i < student.Count(); i += 3)
                    {
                        var t = student[i].FindElements(By.TagName("a"));
                        try
                        {
                            var student1 = student[i].FindElements(By.TagName("a"))[0];
                            string link = student1.GetAttribute("href");
                            string id = Convert.ToString(link.Split('/')[link.Split('/').Count() - 2]);
                            string name = student1.GetAttribute("aria-label");
                            if (name == null) continue;
                            Console.WriteLine(name);

                            Console.WriteLine("added");
                            var people = client.Cypher.Match("(per:Person)")
            .Where((Person per) => per.Fb_name == name).Return(per => per.As<Person>()).Results;
                            if (people.Count() == 0)
                            {
                                var people2 = client.Cypher.Match("(per:Person)")
        .Where((Person per) => per.Fb_id == id).Return(per => per.As<Person>()).Results;
                                if (people2.Count() == 0)
                                {
                                    string name2 = Change_of_View.change(name);
                                    name2 = Change_of_View.reverse(name2);
                                    if (full_list.ContainsKey(name2) || full_list.ContainsValue(name2))
                                    {
                                        string p_name = full_list[name2];
                                        Console.WriteLine(full_list[name2]);
                                        var person = client.Cypher.Match("(per:Person)")
                                        .Where((Person per) => per.Name == p_name).Return(per => per.As<Person>()).Results;

                                        client.Cypher.Match("(per:Person)")
                                        .Where((Person per) => per.Name == p_name)
                                        .Set("per.Fb_name = {name}").WithParam("name", name).ExecuteWithoutResults();
                                        client.Cypher.Match("(per:Person)")
                .Where((Person per) => per.Name == p_name).Set("per.Fb_id = {id}").WithParam("id", id).ExecuteWithoutResults();

                                        Console.WriteLine("+" + name2);
                                    }
                                    else
                                    {
                                        /* Person person = new Person { Name = "0", Fb_name = name, Graduation = "0", Project = "0", Fb_id = id, Group = "0" };
                                         client.Cypher.Create("(per:Person {person})").WithParam("person", person).ExecuteWithoutResults();*/
                                        Console.WriteLine("-" + name2);
                                        Console.WriteLine("new was created");
                                        continue;
                                    }

                                    /*var people1 = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_id == id).Return(per => per.As<Person>()).Results;
                                    foreach (Person x in people1) add_work(x);*/
                                }
                                else
                                {
                                    /*friends_searcher fr_search = new friends_searcher(id, client);
                                    fr_search.Friends_Searh_two();*/
                                }
                            }
                            friends_searcher fr = new friends_searcher(id, client);
                            fr.Friends_Searh_two(full_list);
                            //separate_names();
                        }
                        catch (Exception e)
                        { Console.WriteLine(e); continue; }
                    }
                    count = student.Count() - 1;
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    student = index.FindElements(By.ClassName("nc684nl6"));
                } while (student != null);
            }
                
         }

        class friends_searcher_inst
        {
            private List<string> friends = new List<string>();
            private string URL_friends, page_Source, filename;
            GraphClient client;
            static IWebDriver driver;
            string id;
            public friends_searcher_inst(string id, GraphClient client)
            {
                this.client = client;
                URL_friends = "https://www.instagram.com/" + id + "/followers";
                this.id = id;
            }
            public List<string> Friends
            {
                get { return friends; }
            }
            static friends_searcher_inst()
            {
                string login = "recipef1nder";
                string password = "BestTeamver2020";
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Eager;
                options.AddArguments("--disable-notifications");
                driver = new ChromeDriver(@"C:\Users\nattt\Downloads\chromedriver", options);
                driver.Navigate().GoToUrl("https://www.instagram.com/");
                driver.FindElement(By.Name("username")).SendKeys(login);
                driver.FindElement(By.Name("password")).SendKeys(password);
                driver.FindElement(By.CssSelector("button.sqdOP.L3NKy.y3zKF")).Click();
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl("https://www.instagram.com/");
            }
            public int Friends_Searh_two()
            {
                Thread.Sleep(100);
                driver.Navigate().GoToUrl(URL_friends);
                try { driver.FindElement(By.CssSelector("a.-nal3")).Click(); }
                catch { return 0; }
                
                Thread.Sleep(1000);
                var elements = driver.FindElements(By.CssSelector("div.wFPL8"));
                List<string> nicknames = new List<string>();
                for (int i = 0; i < elements.Count(); i++)
                {
                    if (elements[i].Text != "") nicknames.Add(elements[i].Text);
                }
                page_Source = driver.PageSource;
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Inst_name != "").Return(per => per.As<Person>()).Results;
                foreach (Person x in people)
                {
                    if ((nicknames.Contains(x.Inst_nick)) && (x.Inst_nick != id) && (x.Inst_name != ""))
                    {
                        try
                        {
                            var query = client.Cypher
            .Match("(friend1:Person)", "(friend2:Person)")
            .Where((Person friend1) => friend1.Inst_nick == id)
            .AndWhere((Person friend2) => friend2.Inst_nick == x.Inst_nick)
            .CreateUnique("(friend1)-[:INST_FRIENDS]->(friend2)");
                            query.ExecuteWithoutResults();
                            Console.WriteLine("cool");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error);
                        }
                    }
                }
                return 1;
            }
        }

            class refresher
            {
                static string filename;
                public static IWebDriver driver;
                public static string return_id(string name)
                {
                    if (name == "0") return "0";
                    else
                    {
                        try
                        {
                            driver.Navigate().GoToUrl("https://www.facebook.com/find-friends/browser/");
                            driver.FindElement(By.ClassName("_1frb")).SendKeys(name + "\n");
                            Thread.Sleep(5000);

                            string page = driver.PageSource;
                            int ind = page.IndexOf("data-profileid");
                            string id = "0";
                            if (ind != -1)

                            {
                                ind += 16;
                                int ind2 = page.IndexOf("type", ind) - 2;
                                id = page.Substring(ind, ind2 - ind);
                            }
                            return id;
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error);
                            return "0";
                        }

                    }
                }

                static refresher()
                {
                    string login = "natasha_ea5@mail.ru";
                    string password = "bykvah-3vyxfe-kutmYw";
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--disable-notifications");
                    driver = new ChromeDriver(@"C:\Users\nattt\Downloads\chromedriver", options);
                    driver.Navigate().GoToUrl("https://www.facebook.com/");
                    driver.FindElement(By.Id("email")).SendKeys(login);
                    driver.FindElement(By.Id("pass")).SendKeys(password);
                    driver.FindElement(By.Id("login")).Click();
                    filename = "C:\\Users\\nattt\\Desktop\\диплом\\таблица11.xlsx";

                }
            }
            class friends_searcher
            {
                private List<string> friends = new List<string>();
                private string URL_friends, page_Source, filename;
                GraphClient client;
                static IWebDriver driver;
                string id;
                public friends_searcher(string URL_friends, string filename)
                {
                    this.URL_friends = URL_friends;
                    this.filename = filename;
                    Friends_Search_Excel();
                }
                public friends_searcher(string id, GraphClient client)
                {
                    this.client = client;
                    URL_friends = "https://www.facebook.com/" + id + "/friends?lst=100006474277682%3A1200987380%3A1521566471&source_ref=pb_friends_tl";

                    this.id = id;
                }
                public List<string> Friends
                {
                    get { return friends; }
                }
                static friends_searcher()
                {
                    string login = "natasha_ea5@mail.ru";//"89161742573";
                    string password = "bykvah-3vyxfe-kutmYw";//"281292";
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--disable-notifications");
                    driver = new ChromeDriver(@"C:\Users\nattt\Downloads\chromedriver", options);
                    driver.Navigate().GoToUrl("https://www.facebook.com/");
                    driver.FindElement(By.Id("email")).SendKeys(login);
                    driver.FindElement(By.Id("pass")).SendKeys(password);
                    driver.FindElement(By.Name("login")).Click();
                driver.Navigate().GoToUrl("https://www.facebook.com/");
            }
                public void Friends_Searh_two(Dictionary<string, string> full_list)
                {
                // Thread.Sleep(10);
                driver.Navigate().GoToUrl(URL_friends);
                var people = client.Cypher.Match("(per:Person)").Where((Person per) => per.Fb_name != "0").Return(per => per.As<Person>()).Results;
                List<string> names = new List<string>();
                foreach (Person x in people)
                {
                    names.Add(x.Fb_name);
                }
                var elements = driver.FindElements(By.CssSelector("div.j83agx80.btwxx1t3.lhclo0ds.i1fnvgqd"));
                    var index = elements[0];
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("arguments[0].scrollIntoView();", index);
                    var student = index.FindElements(By.ClassName("buofh1pr"));
                int count = 0;
                do
                {
                        for (int i = count; i < student.Count(); i += 1)
                        {
                            var t = student[i].FindElements(By.TagName("a"));
                            try
                            {
                                var student1 = student[i].FindElements(By.TagName("a"))[0];
                                string link = student1.GetAttribute("href");
                                var m = student1.FindElement(By.ClassName("d2edcug0"));
                                string name = m.GetAttribute("innerHTML");
                            bool l = student[i].FindElement(By.ClassName("aahdfvyu")).GetAttribute("innerHTML").Contains("1 mutual friend");
                            string id = "";
                            if (link.Contains("=") && link.Contains("php"))  id = Convert.ToString(link.Split('=')[link.Split('=').Count() - 1]);
                            else
                                 id = Convert.ToString(link.Split('/')[link.Split('/').Count() - 1]);
                                if (name == null) continue;
                                //Console.WriteLine(name);
                                if (!names.Contains(name) && student[i].FindElement(By.ClassName("aahdfvyu")).GetAttribute("innerHTML").Contains("1 mutual friend"))
                                {
                                    string name2 = Change_of_View.change(name);
                                    name2 = Change_of_View.reverse(name2);
                                    if (full_list.ContainsKey(name2) || full_list.ContainsValue(name2))
                                    {
                                        string p_name = full_list[name2];
                                        //Console.WriteLine(full_list[name2]);
                                        var person = client.Cypher.Match("(per:Person)")
                                        .Where((Person per) => per.Name == p_name).Return(per => per.As<Person>()).Results;

                                        client.Cypher.Match("(per:Person)")
                                        .Where((Person per) => per.Name == p_name)
                                        .Set("per.Fb_name = {name}").WithParam("name", name).ExecuteWithoutResults();
                                        client.Cypher.Match("(per:Person)")
                .Where((Person per) => per.Name == p_name).Set("per.Fb_id = {id}").WithParam("id", id).ExecuteWithoutResults();

                                        Console.WriteLine("+ " + id + " " + name);
                                        names.Add(name);
                                    }
                                }
                                if (names.Contains(name))
                                {
                                    try
                                    {
                                        var query = client.Cypher
                        .Match("(friend1:Person)", "(friend2:Person)")
                        .Where((Person friend1) => friend1.Fb_id == this.id)
                        .AndWhere((Person friend2) => friend2.Fb_id == id)
                        .CreateUnique("(friend1)-[:FB_FRIENDS]->(friend2)");
                                        query.ExecuteWithoutResults();
                                        //Console.WriteLine("cool");
                                    }
                                    catch (Exception error)
                                    {
                                        Console.WriteLine(error);
                                    }

                                }
                            }
                            catch (Exception e)
                            { Console.WriteLine(e); continue; }
                        }
                        count = student.Count() - 1;
                        js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                        student = index.FindElements(By.ClassName("buofh1pr"));
                    } while (student != null);
                }
                public void Friends_Search_Excel()
                {
                    /*  driver.Navigate().GoToUrl(URL_friends);
                      page_Source = driver.PageSource;
                      Excel.Application excelApp;
                      Excel.Workbook xlWb;
                      Excel.Worksheet xlSht;
                      excelApp = new Excel.Application();
                      xlWb = excelApp.Workbooks.Open(filename);
                      xlSht = xlWb.Sheets[1];
                      for (int i = 3; i < 1199; i++)
                      {
                          if (page_Source.Contains(xlSht.Cells[i, "A"].value)) friends.Add(xlSht.Cells[i, "A"].value);
                          Console.WriteLine(i);
                      }
                      excelApp.Quit();
                 */
                }
            }
            class work_searcher
            {
                private string URL_work, page_Source;
                private string[] education, work;
                private string[] occupation, edu;
                private IWebDriver driver;
                public work_searcher(string URL_work, IWebDriver driver)
                {
                    this.URL_work = URL_work;
                    this.driver = driver;
                    Education_And_Work_Search();
                }
                public string[] Work
                {
                    get
                    {
                        if (work != null)
                        {
                            string[] tmp = new string[work.Count()];
                            for (int i = 0; i < work.Count(); i++)
                                tmp[i] = normal_view(work[i]);
                            return tmp;
                        }
                        return null;
                    }
                }
                public string[] Education
                {
                    get
                    {
                        if (education != null)
                        {
                            string[] tmp = new string[education.Count()];
                            for (int i = 0; i < education.Count(); i++)
                                tmp[i] = normal_view(education[i]);
                            return tmp;
                        }
                        return null;
                    }
                }
                public string[] Occupation
                {
                    get
                    {
                        return occupation;
                    }
                }
                public string[] Edu
                {
                    get
                    {
                        return edu;
                    }
                }
                public string Work_string
                {
                    get
                    {
                        string res = "{";
                        string[] tmp = Work;
                        for (int i = 0; i < tmp.Count(); i++)
                        {
                            res += tmp[i];
                        }
                        return tmp + "}";
                    }
                }
                public string Education_string
                {
                    get
                    {
                        string res = "";
                        string[] tmp = Education;
                        for (int i = 0; i < tmp.Count(); i++)
                        {
                            res += tmp[i];
                        }
                        return "{" + tmp + "}";
                    }
                }
                private string normal_view(string href)
                {
                    string res = " ";
                    try
                    {
                        if (href != null)
                        {
                            int index = page_Source.IndexOf(href);
                            int index2 = 0;
                            if (index > 0) index = page_Source.IndexOf("data-hovercard-prefer-more-content-show", index) + 43;
                            if (index > 44)
                            {
                                index2 = page_Source.IndexOf("<", index);
                                return page_Source.Substring(index, index2 - index);
                            }

                        }
                        return "";
                    }
                    catch (Exception error)
                    {
                        res = " ";
                        Console.WriteLine(error);
                        if (error.ToString() == "TimeoutException") Console.WriteLine(error);
                        normal_view(href);
                    }
                    return res;
                }
                private void Education_And_Work_Search()
                {
                    try
                    {
                        driver.Navigate().GoToUrl(URL_work);
                        page_Source = driver.PageSource;
                        int search_after = page_Source.IndexOf("События из жизни");
                        if (search_after > 0)
                        {
                            int not_search_after = page_Source.IndexOf("Образование", search_after);
                            if (not_search_after == -1) not_search_after = page_Source.IndexOf("pagelet_timeline_medley_friends", search_after);
                            if (page_Source.IndexOf("Эта страница недоступна") != -1) return;
                            if ((search_after == -1) || (not_search_after == -1)) return;
                            int count = 0;
                            int index = 0;
                            if (search_after > 0) index = page_Source.IndexOf("href", search_after);
                            if (index > 1) index = page_Source.IndexOf("href", index + 1);
                            int finish;
                            while ((index < not_search_after) && (index >= 10) && (not_search_after != -1))
                            {
                                if (index != -1) count++;
                                index = index + 6;
                                if (index > 6)
                                {
                                    finish = page_Source.IndexOf('"', index);
                                    index = page_Source.IndexOf("href", index);
                                    index = page_Source.IndexOf("href", index);
                                }
                                if (count > 20) break;
                            }
                            work = new string[count / 2];
                            occupation = new string[count / 2];
                            for (int i = 0; i < count / 2; i++) if (i < occupation.Count()) occupation[i] = " ";
                            int ocup = page_Source.IndexOf("jobTitle") + 11;
                            int ocup_af;
                            count = 0;
                            while (ocup > 11)
                            {
                                ocup_af = page_Source.IndexOf(",", ocup) - 2;
                                if (ocup < ocup_af)
                                    if (count < occupation.Count()) if (ocup > 0) { occupation[count] = page_Source.Substring(ocup, ocup_af - ocup); if (occupation[count].Contains("_3m1v _468f")) occupation[count] = ""; }
                                ocup = page_Source.IndexOf("jobTitle", ocup) + 11;
                                count++;
                                if (count > 20) break;

                            }
                            count = 0;
                            not_search_after = page_Source.IndexOf("pagelet_timeline_medley_friends", index);
                            index = page_Source.IndexOf("href", index);
                            while ((index < not_search_after) && (index >= 10))
                            {
                                if (index != -1) count++;
                                index = index + 6;
                                if (index > 6)
                                {
                                    finish = page_Source.IndexOf('"', index);
                                    index = page_Source.IndexOf("href", index);
                                    index = page_Source.IndexOf("href", index);
                                }
                                if (count > 20) break;

                            }
                            education = new string[count / 2];
                            edu = new string[count / 2];
                            index = 0;
                            search_after = page_Source.IndexOf("События из жизни");
                            not_search_after = page_Source.IndexOf("Образование", search_after);
                            if ((not_search_after == -1) && (search_after > 0)) not_search_after = page_Source.IndexOf("pagelet_timeline_medley_friends", search_after);
                            if (search_after > 0) index = page_Source.IndexOf("href", search_after);
                            index = page_Source.IndexOf("href", index + 1);
                            count = 0;
                            while ((index < not_search_after) && (index >= 10))
                            {
                                index = index + 6;
                                finish = page_Source.IndexOf('"', index);
                                if (count < work.Count()) if (index > 6) work[count] = page_Source.Substring(index, finish - index);
                                count++;
                                if (index > 6) index = page_Source.IndexOf("href", index + 1);
                                index = page_Source.IndexOf("href", index + 1);
                                if (index > 6) if (count > 20) break;

                            }
                            count = 0;
                            not_search_after = page_Source.IndexOf("pagelet_timeline_medley_friends", index);
                            index = page_Source.IndexOf("href", index);
                            while ((index < not_search_after) && (index >= 10))
                            {
                                index = index + 6;
                                finish = page_Source.IndexOf('"', index);
                                if (count < education.Count()) if (index > 6) { education[count] = page_Source.Substring(index, finish - index); if (education[count].Contains("_3m1v _468f")) occupation[count] = ""; }
                                count++;
                                if (index > 6) index = page_Source.IndexOf("href", index + 1);
                                if (index > 6) index = page_Source.IndexOf("href", index + 1);
                                if (count > 20) break;

                            }
                            int search_af = 0;
                            int search_not = 0;
                            for (int i = 0; i < occupation.Count(); i++)
                            {
                                search_af = page_Source.IndexOf("_173e _50f8 _2ieq", search_af) + 44;
                                search_not = page_Source.IndexOf("<", search_af);
                                occupation[i] = page_Source.Substring(search_af, search_not - search_af);
                                if ((occupation[i].Contains("_3m1v _468f")) || (occupation[i].Contains("xhtm"))) occupation[i] = "";
                            }
                            search_not += 150;
                            for (int i = 0; i < edu.Count(); i++)
                            {
                                search_af = page_Source.IndexOf("presentation", search_not) + 43;
                                search_not = page_Source.IndexOf("<", search_af);
                                int ind = page_Source.IndexOf("presentation", search_not);
                                if ((ind != -1) && (ind - search_not < 1000))
                                {
                                    edu[i] = page_Source.Substring(search_af, search_not - search_af); if (edu[i].Contains("_3m1v _468f") || (edu[i].Contains("xhtm"))) edu[i] = "";
                                }

                                else edu[i] = "";
                                search_not += 30;
                            }
                        }

                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error);
                        if (error.ToString() == "TimeoutException")
                            Education_And_Work_Search();
                    }
                }
            }

            class inst_parser
            {
                string id;
                public static IWebDriver driver;
                static string login, password;

                static inst_parser()
                {
                login = "recipef1nder";
                password = "BestTeamver2020";
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Eager;
                options.AddArguments("--disable-notifications");
                driver = new ChromeDriver(@"C:\Users\nattt\Downloads\chromedriver", options);
                driver.Navigate().GoToUrl("https://www.instagram.com/lit1533_official/followers/");
                driver.FindElement(By.Name("username")).SendKeys(login);
                driver.FindElement(By.Name("password")).SendKeys(password);
                driver.FindElement(By.CssSelector("button.sqdOP.L3NKy.y3zKF")).Click();
                Thread.Sleep(100);
                }
                public inst_parser(string id)
                {
                    this.id = id;
                }

        }
        
            class facebook_parser
            {
                string id;
                string[] education, work, occupation, edu;
                public static IWebDriver driver;
                static string login, password;
                static facebook_parser()
                {
                    login = "natasha_ea5@mail.ru";//"89161742573";
                    password = "bykvah-3vyxfe-kutmYw";//"281292";
                    ChromeOptions options = new ChromeOptions();
                    options.PageLoadStrategy = PageLoadStrategy.Eager;
                    options.AddArguments("--disable-notifications");
                    driver = new ChromeDriver(@"C:\Users\nattt\Downloads\chromedriver", options);
                    driver.Navigate().GoToUrl("https://www.facebook.com/");
                    driver.FindElement(By.Id("email")).SendKeys(login);
                    driver.FindElement(By.Id("pass")).SendKeys(password);
                    driver.FindElement(By.Name("login")).Click();
                driver.Navigate().GoToUrl("https://www.facebook.com/groups/1584209605191960/members/");
                }
                public facebook_parser(string id)
                {
                    this.id = id;
                }
                public List<string> Occupation
                {
                    get
                    {
                        if (work != null) return work.ToList();
                        else
                        {
                            List<string> a = new List<string> { };
                            a.Add("0");
                            return a;
                        }
                    }

                }
                public List<string> Position
                {
                    get
                    {
                        if (occupation != null) { return occupation.ToList(); }
                        else
                        {
                            List<string> a = new List<string> { };
                            a.Add("0");
                            return a;
                        }
                    }

                }
                public List<string> Education
                {
                    get
                    {
                        if (education != null) { return education.ToList(); }
                        else
                        {
                            List<string> a = new List<string> { };
                            a.Add("0");
                            return a;
                        }
                    }
                }
                public List<string> FieldOfEducation
                {
                    get
                    {
                        if (edu != null) { return edu.ToList(); }
                        else
                        {
                            List<string> a = new List<string> { };
                            a.Add("0");
                            return a;
                        }
                    }
                }
                public string URL_work
                {
                    get
                    {
                        return "https://www.facebook.com/" + id + "/about?lst=100006474277682%3A1269467462%3A1518714057&section=education&pnref=about";

                    }
                }
                public void Page_load()
                {
                    Thread.Sleep(7000);
                    work_searcher tmp = new work_searcher(URL_work, driver);
                    work = tmp.Work;
                    edu = tmp.Edu;
                    education = tmp.Education;
                    occupation = tmp.Occupation;
                }
            }
    } 
}

/*main user: neo4j password 12345*/
