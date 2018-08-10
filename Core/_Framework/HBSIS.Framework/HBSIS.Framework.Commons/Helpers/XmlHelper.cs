using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HBSIS.Framework.Commons.Helper
{
    public static class XmlHelper
    {
        public static string Serialize<T>(T obj)
        {
            var ret = string.Empty;

            if (obj != null)
            {
                var ms = new MemoryStream();
                var xmlWriter = new XmlTextWriter(ms, Encoding.Default);

                var xs = new XmlSerializer(obj.GetType());
                xs.Serialize(xmlWriter, obj, null);

                ms.Seek(0, SeekOrigin.Begin);
                var streamReader = new StreamReader(ms, Encoding.Default);
                ret = streamReader.ReadToEnd();
            }

            return ret;
        }

        public static string XmlSerialize<T>(this T obj)
        {
            return Serialize(obj);
        }

        public static T Deserialize<T>(string xml)
        {
            T ret = default(T);

            if (string.IsNullOrWhiteSpace(xml)) return ret;

            var xs = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(xml))
            {
                ret = (T)xs.Deserialize(reader);
            }

            return ret;
        }

        public static T XmlDeserialize<T>(this string xml)
        {
            return Deserialize<T>(xml);
        }
    }
}