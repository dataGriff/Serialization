using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace XMLSerializer5
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\Documents\Projects\Serialisation\CarsAttributes.xml";
            XmlDocument doc = new XmlDocument();
            //doc.Load(path);
            //string xmlcontents = doc.InnerXml;
            Data MyData = Data.Load(path);

            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CarCollection));
                cars = (CarCollection)serializer.Deserialize(reader);
                foreach (Car car in cars.Cars)
                {
                    var message = JsonConvert.SerializeObject(car);
                    Console.WriteLine(message);
                }
                Console.ReadKey();
            }
        }
    }

    public class Data
    {
        public List<DataItem> Items { get; set; }

        private Data(XElement root)
        {
            Items = new List<DataItem>();

            foreach (XElement el in root.Elements())
            {
                Items.Add(new DataItem(el));
            }
        }

        public static Data Load(Stream stream)
        {
            return new Data(XDocument.Load(stream).Root);
        }
    }

    public class DataItem
    {
        public Dictionary<string, string> Vals;
        public string Id { get { return (string)Vals["StockNumber"]; } }

        public DataItem(XElement el)
        {
            Vals = new Dictionary<string, string>();

            // Load all the element attributes into the Attributes Dictionary
            foreach (XAttribute att in el.Attributes())
            {
                string name = att.Name.ToString();
                string val = att.Value;


                Vals.Add(name, val);
            }
        }
    }
}
