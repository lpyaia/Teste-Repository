using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HBSIS.GE.FileImporter.Producer.Utils
{
    public static class XmlParser
    {
        public static string ObjectToXml(object obj)
        {
            using (StringWriter stringWriter = new StringWriter())
            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
            {

                try
                {
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());

                    //Xml limpo. Sem namespace.
                    XmlSerializerNamespaces xmlNamespace = new XmlSerializerNamespaces();
                    xmlNamespace.Add("", "");

                    serializer.Serialize(xmlTextWriter, obj, xmlNamespace);
                }

                catch (Exception ex)
                {
                    Console.Error.Write($"[XmlParser] - Erro ao serializar o XML.\nException Message: {ex}");
                }

                return RemoveAllNamespaces(stringWriter.ToString());
            }
        }

        public static void CreateXmlFile(string xml, string path, string fileName)
        {
            var filePath = $@"{path}\{fileName}.xml";

            File.Create(filePath).Close();

            TextWriter tw = new StreamWriter(filePath);
            tw.Write(xml);
            tw.Close();
        }
        
        private static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }
        
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }

            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }
    }
}
