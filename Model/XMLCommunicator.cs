using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Model
{
    public class XMLCommunicator : ICommunicator
    {
        #region ICommunicator Members

        /// <summary>
        /// Name of file to work with
        /// </summary>
        public string FileName
        {
            get { return "snippet.xml"; }
        }

        /// <summary>
        /// Allows to convert XML document to List of class
        /// </summary>
        /// <returns>List of class</returns>
        public List<Entry> GetList()
        {
            if (!File.Exists(FileName))
                if (MessageBox.Show("XML file is empty or corrupt.\r\nNew one will be created...", "XML warning", MessageBoxButtons.OK) == DialogResult.OK)
                    File.WriteAllText(FileName, "<?xml version=\"1.0\" encoding=\"windows-1251\"?><ArrayOfEntry xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" />");

            return (XDocument.Load(FileName).Descendants("Entry")
                .Select(x => new Entry
                                  {
                                      ID = Int32.Parse(x.Element("ID").Value),
                                      Root = x.Element("Root").Value,
                                      Name = x.Element("Name").Value,
                                      Description = x.Element("Description").Value,
                                      Code = x.Element("Code").Value,
                                      Category = x.Element("Category").Value,
                                      DateChanged = x.Element("DateChanged").Value
                                  })).ToList();
        }

        /// <summary>
        /// Allows to delete item from XML document
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id">unique key value</param>
        public void DeleteItem(string key, Int64 id)
        {
            List<Entry> list = GetList();
            list.Remove(list.Find(x => x.ID == id));
            using (var myWriter = new StreamWriter(FileName, false, Encoding.Default))
            {
                new XmlSerializer(typeof (List<Entry>), new[] {typeof (Entry)}).Serialize(myWriter, list);
            }
        }

        /// <summary>
        /// Allows to insert or update item in XML document by Key
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="key"></param>
        /// <param name="id">unique key value</param>
        public Int64 InsertOrUpdateItem(Entry item, string key, Int64 id)
        {
            List<Entry> list = GetList();
            if (list.Find(x => x.ID == id) == null)
            {
                item.ID = list.Count == 0 ? 1 : list.Select(x => x.ID).Max() + 2;
                list.Add(item);
            }
            else
            {
                list.Remove(list.Find(x => x.ID == id));
                item.ID = id;
                list.Add(item);
            }

            using (var myWriter = new StreamWriter(FileName, false, Encoding.Default))
            {
                new XmlSerializer(typeof (List<Entry>), new[] {typeof (Entry)}).Serialize(myWriter, list);
            }
            return item.ID;
        }

        #endregion
    }
}