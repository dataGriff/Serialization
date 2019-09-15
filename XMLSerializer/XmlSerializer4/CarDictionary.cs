using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace XmlSerializer4
{
    public class CarDictionary : IXmlSerializable
    {
        private List<Dictionary<string, string>> carDictionary = new List<Dictionary<string, string>>();

        public CarDictionary(List<Dictionary<string, string>> dictionary)
        {
            carDictionary = dictionary;
        }

        public CarDictionary()
        {
            carDictionary = null;
        }

        public XmlSchema GetSchema()
        {
            return (null);
        }

        public void ReadXml(XmlReader reader)
        {
            XElement attributesElement = XElement.Parse(reader.ReadOuterXml());
            List<XElement> attributeElements = attributesElement.Elements("Car").ToList();
            carDictionary = attributeElements.Select(x => x.Attributes().ToDictionary(y => y.Name.LocalName, z => z.Value)).ToList();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Cars");

            foreach (Dictionary<string, string> dictionary in carDictionary)
            {
                writer.WriteStartElement("Car");

                foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                {
                    writer.WriteAttributeString(keyValuePair.Key, keyValuePair.Value);
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }
    }
}
