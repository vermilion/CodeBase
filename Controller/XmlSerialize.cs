using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Model;

namespace Controller
{
    internal static class XmlSerialize
    {
        /// <summary>
        /// Allows to deserialize Entry item
        /// </summary>
        /// <param name="communicator"></param>
        /// <param name="path">path of target file</param>
        /// <returns>Entry item</returns>
        public static Entry DeserializeBaseClass(ICommunicator communicator, String path)
        {
            using (var myReader = new StreamReader(path, Encoding.Default))
            {
                var mySerializer = new XmlSerializer(typeof (Entry));
                var item = (Entry) mySerializer.Deserialize(myReader);

                communicator.InsertOrUpdateItem(item, "ID", item.ID);
                return item;
            }
        }

        /// <summary>
        /// Allows to serialize Entry item
        /// </summary>
        /// <param name="list">List to be inserted to</param>
        /// <param name="childText">item Name</param>
        /// <param name="parentText">item Root</param>
        /// <returns>Entry item</returns>
        public static Entry SerializeBaseClass(List<Entry> list, string childText, string parentText)
        {
            try
            {
                Entry serializeItem = list.Find(item => item.Name == childText && item.Category == parentText);

                using (var myWriter = new StreamWriter(string.Format(@"serialized\{0}_{1}.xml", serializeItem.Category, serializeItem.Name), false, Encoding.Default))
                {
                    var mySerializer = new XmlSerializer(typeof (Entry));
                    mySerializer.Serialize(myWriter, serializeItem);
                }
                return serializeItem;
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory("serialized");
                return SerializeBaseClass(list, childText, parentText);
            }
            catch
            {
                return null;
            }
        }
    }
}