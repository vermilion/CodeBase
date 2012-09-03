using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Model
{
    public class SQLiteCommunicator : ICommunicator
    {
        private readonly SQLiteDatabaseHelper _db;

        /// <summary>
        /// Constructor with database name
        /// </summary>
        public SQLiteCommunicator()
        {
            _db = new SQLiteDatabaseHelper(FileName);
        }

        /// <summary>
        /// Sqlite table name
        /// </summary>
        private string TableName
        {
            get { return "Snippet"; }
        }

        #region ICommunicator Members

        /// <summary>
        /// Name of file to work with
        /// </summary>
        public string FileName
        {
            get { return "snippet.db3"; }
        }

        /// <summary>
        /// Allows to convert Dataset to List of class
        /// </summary>
        /// <returns>List of class</returns>
        public List<Entry> GetList()
        {
            if (_db.ExecuteScalar(string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}'", TableName)) == "")
                if (MessageBox.Show("Sqlite database is empty or corrupt.\r\nNew one will be created...", "Sqlite warning", MessageBoxButtons.OK) == DialogResult.OK)
                    _db.ExecuteNonQuery("CREATE TABLE Snippet (ID integer PRIMARY KEY, Root text, Name text, Description text, Code varchar, Category varchar, DateChanged varchar)");

            return (from DataRow dr in _db.FillDataset("select * from " + TableName).Tables[0].Rows
                    select new Entry
                               {
                                   ID = dr["ID"].ToString(),
                                   Root = dr["Root"].ToString(),
                                   Name = dr["Name"].ToString(),
                                   Description = dr["Description"].ToString(),
                                   Code = dr["Code"].ToString(),
                                   Category = dr["Category"].ToString(),
                                   DateChanged = dr["DateChanged"].ToString()
                               }).ToList();
        }

        /// <summary>
        /// Allows to delete item from database by Key
        /// </summary>
        /// <param name="key">unique key name for sql queue</param>
        /// <param name="id">unique key value for sql queue</param>
        public void DeleteItem(string key, string id)
        {
            _db.ExecuteNonQuery(String.Format("delete from {0} where {1};", TableName, key + "=" + id));
        }

        /// <summary>
        /// Allows to insert or update item in database by Key
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="key">unique key name for sql queue</param>
        /// <param name="id">unique key value for sql queue</param>
        public void InsertOrUpdateItem(Entry item, string key, string id)
        {
            var dict = new Dictionary<String, String>
                           {
                               {"Root", item.Root},
                               {"Name", item.Name},
                               {"Description", item.Description},
                               {"Code", item.Code.Replace("'", "''")},
                               {"Category", item.Category},
                               {"DateChanged", item.DateChanged.ToString(CultureInfo.InvariantCulture)}
                           };

            if (_db.ExecuteScalar(String.Format("select {0} from {1} where {2}={3}", key, TableName, key, id)) == string.Empty)
                _db.Insert(TableName, dict);
            else
                _db.Update(TableName, dict, String.Format("{0}={1}", key, id));
        }

        #endregion
    }
}