using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace project_05
{
    class DataManager
    {
        public static List<User> Users = new List<User>();
        public static List<User> Results = new List<User>();

        static DataManager()
        {
            Load();
        }

        public static void Load()
        {
            try
            {
                string usersOutput = File.ReadAllText(@"./Users.xml");
                XElement usersXElement = XElement.Parse(usersOutput);

                Users = (from item in usersXElement.Descendants("user")
                         select new User()
                         {
                             Id = item.Element("id").Value,
                             Pwd = int.Parse(item.Element("pwd").Value),
                             Count = int.Parse(item.Element("count").Value)
                         }
                        ).ToList<User>();

                Results = (from item in usersXElement.Descendants("user")
                         select new User()
                         {
                             Id = item.Element("id").Value,
                             Count = int.Parse(item.Element("count").Value)
                         }
                        ).ToList<User>();
            }
            catch (FileNotFoundException e)
            {
                Save();
            }
        }

        public static void Save()
        {
            string usersOutput = "";
            usersOutput += "<users>\n";
            foreach (var item in Users)
            {
                usersOutput += "<user>\n";

                usersOutput += "<id>" + item.Id + "</id>\n";
                usersOutput += "<pwd>" + item.Pwd + "</pwd>\n";
                usersOutput += "<count>" + item.Count + "</count>\n";

                usersOutput += "</user>\n";
            }
            usersOutput += "</users>\n";

            File.WriteAllText(@"./Users.xml", usersOutput);

        }
    }
}
