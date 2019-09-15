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

namespace XMLSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\Documents\Projects\Serialisation\TestXML2.xml";
            AttributeCollection attributes = null;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AttributeCollection));
                attributes = (AttributeCollection)serializer.Deserialize(reader);
                foreach (Attribute attribute in attributes.Attributes)
                {
                    var message = JsonConvert.SerializeObject(attribute);
                    Console.WriteLine(message);
                }
                Console.ReadKey();
            }
        }
    }

    public partial class Attribute
    {
        public AttributeDictionary Attributes { get; set; }
    }

    [XmlRootAttribute("Attributes")]
    public class AttributeCollection
    {
        [XmlElement("Attribute")]
        public Attribute[] Attributes { get; set; }
    }
}

