using System;
using System.Collections.Generic;
using System.IO;
using Model;

namespace Presenter
{
    public static class XmlSerialize
    {
        /// <summary>
        /// Allows to deserialize Entry item
        /// </summary>
        /// <param name="communicator"></param>
        /// <param name="path">path of target file</param>
        /// <returns>Entry item</returns>
        public static Entry DeserializeBaseClass(ICommunicator communicator, string path)
        {
            var item = (Entry) XmlHelper.LoadXml(typeof (Entry), path);
            item.ID = communicator.ModifyItem(item, "ID", item.ID);
            return item;
        }

        /// <summary>
        /// Allows to serialize Entry item
        /// </summary>
        /// <param name="list">List to be inserted to</param>
        /// <param name="id">item Name</param>
        /// <returns>Entry item</returns>
        public static Entry SerializeBaseClass(List<Entry> list, object id)
        {
            try
            {
                Entry item = list.Find(x => x.ID == Int64.Parse(id.ToString()));
                XmlHelper.SaveXml(item, string.Format(@"serialized\{0}_{1}.xml", item.Category, item.Name));
                return item;
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory("serialized");
                return SerializeBaseClass(list, id);
            }
            catch
            {
                return null;
            }
        }
    }
}