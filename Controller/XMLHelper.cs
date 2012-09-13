using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Controller
{
    public static class XmlHelper
    {
        /// <summary>
        /// Allows to deserialize xml to object
        /// </summary>
        /// <param name="type">type of object in xml</param>
        /// <param name="filename">path to file</param>
        /// <returns></returns>
        public static object LoadXml(Type type, string filename)
        {
            using (var reader = new StreamReader(filename, Encoding.Default))
            {
                return new XmlSerializer(type).Deserialize(reader);
            }
        }

        /// <summary>
        /// Allows to serialize object
        /// </summary>
        /// <param name="obj">target object</param>
        /// <param name="filename">output filename</param>
        public static void SaveXml(object obj, string filename)
        {
            using (var writer = new StreamWriter(filename, false, Encoding.Default))
            {
                new XmlSerializer(obj.GetType()).Serialize(writer, obj);
            }
        }
    }
}