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
            object result;
            using (var reader = new StreamReader(filename, Encoding.Default))
            {
                try
                {
                    result = new XmlSerializer(type).Deserialize(reader);
                }
                finally
                {
                    reader.Close();
                }
            }
            return result;
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
                try
                {
                    new XmlSerializer(obj.GetType()).Serialize(writer, obj);
                }
                finally
                {
                    writer.Close();
                }
            }
        }
    }
}