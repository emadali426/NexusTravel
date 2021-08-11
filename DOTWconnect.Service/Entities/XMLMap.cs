using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DOTWconnect.Service.Entities
{
    public class XMLMap<T> where T : class
    {
        private XmlSerializer _ser;
        private StringBuilder _xml;
        private XmlWriter _xmlWriter;
        public XMLMap()
        {
            _ser = new XmlSerializer(typeof(T));
            _xml = new StringBuilder();
            _xmlWriter = XmlWriter.Create(_xml, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
            });
        }

        public string ObjectToXML(T obj)
        {
            _ser.Serialize(_xmlWriter, obj);
            return _xml.ToString();
        }

        public T XMLToObject(string xml)
        {
            using (TextReader read = new StringReader(xml))
            {
               return _ser.Deserialize(read) as T;
            }
        }
    }
}
