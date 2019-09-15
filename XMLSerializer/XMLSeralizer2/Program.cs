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

namespace XMLSeralizer2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\Documents\Projects\Serialisation\Cars.xml";
            CarCollection cars = null;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CarCollection));
                cars =(CarCollection)serializer.Deserialize(reader);
                foreach(Car car in cars.Cars)
                {
                    var message = JsonConvert.SerializeObject(car);
                    Console.WriteLine(message);
                }
                Console.ReadKey();
            }
        }
    }

    public class Car
    {
        public string StockNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }

    [XmlRootAttribute("Cars")]
    public class CarCollection
    {
        [XmlElement("Car")]
        public Car[] Cars { get; set; }
    }
}
