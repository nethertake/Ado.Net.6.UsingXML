using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AdoXML.Model;

namespace AdoXML
{
    class Program
    {
        static public Model1 db = new Model1();
        static void Main(string[] args)
        {

            //Exmpl_02();
            //Exmpl_03();
            Exmpl_07();
        }

        public static void Exmpl_01()
        {
            XElement xBook = new XElement("BookParticipants",
                new XElement("BookParticipant", new XAttribute("type", "Author")),
                new XElement("FirstName", "Joe"),
                new XElement("LastName", "Rattz"),

                new XElement("BookParticipant", new XAttribute("type", "Editor")),
                new XElement("FirstName", "Buckingham"),
                new XElement("LastName", "Rattz")
                );

            Console.WriteLine(xBook.ToString());
        }

        public static void Exmpl_02()
        {

            //1 method из файла

            XDocument doc = XDocument.Load(@"\\dc\Студенты\ПКО\SDP-162\ADO.NET\PBook.xml");
            Console.WriteLine(doc.ToString());

            //2 method из строки

            string str = doc.ToString();
            XDocument docFromStr = XDocument.Parse(str);
            Console.WriteLine(docFromStr.ToString());

        }

        public static void Exmpl_03()
        {
            //сохранение в файл
            XDocument doc = XDocument.Load(@"\\dc\Студенты\ПКО\SDP-162\ADO.NET\PBook.xml");
            // doc.Save("test.xml", SaveOptions.DisableFormatting);
            doc.Save("test.xml");
        }

        public static void Exmpl_04()
        {
            XElement serviceHistory = new XElement("TrackServiceHistory", from service in db.TrackServiceHistory.ToList()
                                                                          select
                                                                          new XElement("ServiceHistory", new XAttribute("ServiceHistoryId", service.intServiceHistoryId),
                                                                          new XElement("dRepairDate", service.dRepairDate),
                                                                          new XElement("strDescriptionProblem", service.strDescriptionProblem)));
            /*
           навигационные узлы
           -XNode
           -XElement  

           */

            Console.WriteLine(serviceHistory.ToString());




        }

        public static void Exmpl_05()
        {
            XElement serviceHistory = new XElement("TrackServiceHistory", from service in db.TrackServiceHistory.Take(10).ToList()
                                                                          select
                                                                          new XElement("ServiceHistory", new XAttribute("ServiceHistoryId", service.intServiceHistoryId),
                                                                          new XElement("dRepairDate", service.dRepairDate),
                                                                          new XElement("strDescriptionProblem", service.strDescriptionProblem),
                                                                          new XElement("Equipment", new XElement("StopReason", service.intStopReason),
                                                                          new XElement("SMCSJob", service.strSMCSJob)

                                                                          )));
            //Console.WriteLine(serviceHistory.ToString());

            IEnumerable<string> findSMCSJob = from doc in serviceHistory.Elements()
                                              where doc.Elements().Any(a => a.Value == "Плановое ТО-500")
                                              select doc.Value;

            foreach (string item in findSMCSJob)
            {
                Console.WriteLine("-->" + item);
            }

            //string findSMCSJob2 = serviceHistory.Element("dRepairDate").Value;

            // Console.WriteLine(findSMCSJob2);


            //foreach (XElement item in serviceHistory.Elements())
            //{
            //    Console.WriteLine("-->" + item.Name + " - " + item.Value);
            //}




        }

        public static void Exmpl_06()
        {
            XElement f = new XElement("Test", "001");

            XNamespace ns = "https:\\google.kz";

            f.Save("test2.xml");
            XDocument xDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(ns + "rootElement",
                new XCData("<H1>SomeText</H1>"))
                );

            xDoc.Save("test4.xml");
        }

        //добавление
        public static void Exmpl_07()
        {
            XElement xBook = new XElement("BookParticipants",
             new XElement("BookParticipant", new XAttribute("type", "Author")),
             new XElement("FirstName", "Joe"),
             new XElement("LastName", "Rattz"));

            xBook.Element("BookParticipant").Add(new XElement("BookParticipant", new XAttribute("type", "Editor")),
                new XElement("FirstName", "Buckingham"),
                new XElement("LastName", "Rattz"));

            xBook.Element("BookParticipant").Elements("BookParticipant").Where(w => (string)w.Element("FirstName") == "Joe")
            .Single<XElement>().AddAfterSelf(new XElement("BookParticipant", new XAttribute("type", "Editor")),
                new XElement("FirstName", "Vladimir"),
                new XElement("LastName", "Skoromniy"));


        }


        public static void Exmpl_08()
        {
            XElement bookParticipants;
            XDocument xDoc = new XDocument(
                 new XElement("BookParticipants", bookParticipants = 
              new XElement("BookParticipant", new XAttribute("type", "Author")),
              new XElement("FirstName", "Joe"),
              new XElement("LastName", "Rattz"),

              new XElement("BookParticipant", new XAttribute("type", "Editor")),
              new XElement("FirstName", "Buckingham"),
              new XElement("LastName", "Rattz"),

                new XElement("BookParticipant", new XAttribute("type", "Editor")),
              new XElement("FirstName", "Yevgeniy"),
              new XElement("LastName", "Gertsen")
              )
            );

            //detele by namespace

            bookParticipants.Remove();


            //delete element
            xDoc.Elements().Where(w=>(string)w.Attribute("type")=="Author").Remove();
            xDoc.Elements().Where(w => w.Name == "FirstName").Where(w => w.Value == "Yevgeniy").Remove();


            
        }
        //обновление значений
        public static void Exmpl_09()
        {
            XDocument xDoc = new XDocument(
                           new XElement("BookParticipants", 
                        new XElement("BookParticipant", new XAttribute("type", "Author")),
                        new XElement("FirstName", "Joe"),
                        new XElement("LastName", "Rattz"),

                        new XElement("BookParticipant", new XAttribute("type", "Editor")),
                        new XElement("FirstName", "Buckingham"),
                        new XElement("LastName", "Rattz"),

                          new XElement("BookParticipant", new XAttribute("type", "Editor")),
                        new XElement("FirstName", "Yevgeniy"),
                        new XElement("LastName", "Gertsen")
                        )
                      );

            xDoc.Nodes().OfType<XElement>().Where(w=>w.Name== "FirstName").Where(w => w.Value == "Yevgeniy").Single().Value = "Timur";

            var element = xDoc.Nodes().OfType<XElement>().Where(w => w.Name == "FirstName").Single(w => w.Value == "Yevgeniy");
            element.SetValue("5555");


        }

    }
}
