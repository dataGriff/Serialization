using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLSerializer
{
    public class AttributeDictionary : IXmlSerializable
    {
        private List<Dictionary<string, string>> attributeDictionary = new List<Dictionary<string, string>>();

        public AttributeDictionary(List<Dictionary<string, string>> dictionary)
        {
            attributeDictionary = dictionary;
        }

        public AttributeDictionary()
        {
            attributeDictionary = null;
        }

        public XmlSchema GetSchema()
        {
            return (null);
        }

        public void ReadXml(XmlReader reader)
        {
            XElement attributesElement = XElement.Parse(reader.ReadOuterXml());
            List<XElement> attributeElements = attributesElement.Elements("Attribute").ToList();
            attributeDictionary = attributeElements.Select(x => x.Attributes().ToDictionary(y => y.Name.LocalName, z => z.Value)).ToList();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Attributes");

            foreach (Dictionary<string, string> dictionary in attributeDictionary)
            {
                writer.WriteStartElement("Attribute");

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
