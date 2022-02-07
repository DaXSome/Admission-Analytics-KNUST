using System;
using HtmlAgilityPack;
using Constants;
using Model;
using Sql;
using Dapper;
using System.Data.SqlClient;
using System.Data;


namespace Data
{
    public class Data
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("[+] Beginning data extract...");

            await Extract();

            Console.WriteLine("[+] Extraction complete!!");

        }

        public static async Task Extract()
        {
            HtmlWeb Web = new();

            using SqlConnection conn = new SqlConnection(Constants.Constants.ConnectionString);

            await conn.ExecuteAsync(Sql.Queries.CreatePerson);

            for (int i = 1; i < 410; i++)
            {

                String url = Constants.Constants.BaseURL + i.ToString();

                Console.WriteLine("Current url: " + url);


                HtmlDocument Document = Web.Load(url);

                HtmlNodeCollection SelectNodes = Document.DocumentNode.SelectNodes("//tr");

                foreach (HtmlNode Node in SelectNodes)
                {

                    HtmlNode[] data = Node.ChildNodes.ToArray();


                    if (!data[1].InnerText.Contains("#"))
                    {
                        string[] names = data[5].InnerText.Split(",");
                        string[] degree = data[7].InnerText.Split(". ");

                        Console.WriteLine("[+] ID: " + data[1].InnerText);
                        Console.WriteLine("[+] Student ID: " + data[3].InnerText);
                        Console.WriteLine("[+] Name: " + data[5].InnerText);
                        Console.WriteLine("[+] Course: " + data[7].InnerText);
                        Console.WriteLine("[+] Degree: " + degree[0]);
                        Console.WriteLine("*********************");



                        Person person = new Person
                        {
                            ID = data[1].InnerText,
                            StudentID = data[3].InnerText,
                            FirstName = names[1],
                            Surname = names[0],
                            Course = data[7].InnerText,
                            Degree = degree[0],
                        };

                        await conn.ExecuteAsync(
                            Sql.Queries.InsertPerson,
                            new
                            {
                                person.ID,
                                person.StudentID,
                                person.Course,
                                person.Degree,
                                person.FirstName,
                                person.Surname,
                            },
                            commandType: CommandType.Text
                        );


                    }

                }

            }
        }
    }

}