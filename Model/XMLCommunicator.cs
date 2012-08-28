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

            return  (XDocument.Load(FileName).Descendants("Entry")
                .Select(dr => new Entry
                                  {
                                      ID = dr.Element("ID").Value,
                                      Root = dr.Element("Root").Value,
                                      Name = dr.Element("Name").Value,
                                      Description = dr.Element("Description").Value,
                                      Code = dr.Element("Code").Value,
                                      Category = dr.Element("Category").Value,
                                      DateChanged = dr.Element("DateChanged").Value
                                  })).ToList();
        }

        /// <summary>
        /// Allows to delete item from XML document
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id">unique key value</param>
        public void DeleteItem(string key, string id)
        {
            var list = GetList();
            list.Remove(list.Find(x => x.ID == id));
            using (var myWriter = new StreamWriter(FileName, false, Encoding.Default))
            {
                new XmlSerializer(typeof (List<Entry>), new[] {typeof (Entry)}).Serialize(myWriter, list);
            }
        }

        /// <summary>
        /// Allows to insert or update item in XML document by Key
        /// </summary>
        /// <param name="entry"> </param>
        /// <param name="key"></param>
        /// <param name="currentID">unique key value</param>
        public void InsertOrUpdateItem(Entry entry, string key, string currentID)
        {
            var list = GetList();
            if (list.Find(x => x.ID == currentID) == null)
            {
                entry.ID = list.Count == 0 ? "1" : (Int32.Parse(list.Select(x => x.ID).Max()) + 2).ToString(CultureInfo.InvariantCulture);
                list.Add(entry);
            }
            else
            {
                list.Remove(list.Find(x => x.ID == currentID));
                entry.ID = currentID;                
                list.Add(entry);
            }

            using (var myWriter = new StreamWriter(FileName, false, Encoding.Default))
            {
                new XmlSerializer(typeof (List<Entry>), new[] {typeof (Entry)}).Serialize(myWriter, list);
            }
        }

        #endregion
    }
}