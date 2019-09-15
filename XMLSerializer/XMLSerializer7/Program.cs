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

namespace XMLSerializer7
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\Documents\Projects\Serialisation\TestXML3.xml";
            var xDoc = XDocument.Load(path);
            string name;
            string value;
            foreach (var Car in xDoc.Descendants("Cars").Elements())
            {
                Car car = new Car();
                car.CarID = Car.Attribute("id").Value;
                foreach (var elem in Car.Descendants("Attributes").Elements())
                {
                    foreach (var attr in elem.Attributes())
                    {
                        name = attr.Name.ToString();
                        value = attr.Value.ToString();
                        car.Attributes.Add(name, value);
                    }
                }
                var message = JsonConvert.SerializeObject(car);
                Console.WriteLine(message);
                Console.ReadKey();
            }
        }
    }

    public class Car
    {
        public string CarID;
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
    }


}
