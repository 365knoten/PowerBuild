using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Helpers
{
    public class Variables :Dictionary<string,string>,IXmlSerializable
    {

        public Variables() : base() { }


        public Variables(Dictionary<string,string> data) : base(data) { }


        public void saveSet(string key, string value)
        {
            string data;
            if (this.TryGetValue(key,out data))
            {
                this[key] = value;
            }
            else
            {
                this.Add(key, value);
            }
        }


        public Variables join(Variables other)
        {
            Variables result = new Variables(this);

            foreach(string key in other.Keys)
            {
                if (result.ContainsKey(key))
                {
                    result[key] = other[key];
                }
                else
                {
                    result.Add(key, other[key]);
                }
            }
            return result;

        }

        public Variables Add(Variables other)
        {
            Variables result = new Variables(this);

            foreach (string key in other.Keys)
            {
                if (!result.ContainsKey(key))
                {
                    result.Add(key, other[key]);
                }
            }
            return result;
        }



        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(string));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {                            
                string key=reader.GetAttribute("key");
                string value = reader.GetAttribute("value");
                this.Add(key, value);
                reader.ReadStartElement("variable");
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(string));

            foreach (string key in this.Keys)
            {
                writer.WriteStartElement("variable");
                writer.WriteAttributeString("value", this[key]);
                writer.WriteAttributeString("key", key);
                writer.WriteEndElement();
            }
        }
    }
}
