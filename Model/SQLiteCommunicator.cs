using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

        private string FileName
        {
            get { return "snippet.db3"; }
        }

        #region ICommunicator Members

        public List<Entry> GetList()
        {
            if (_db.ExecuteScalar(string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}'", TableName)) == string.Empty)
                if (MessageBox.Show("Sqlite database is empty or corrupt.\r\nNew one will be created...", "SQLite warning", MessageBoxButtons.OK) == DialogResult.OK)
                    _db.ExecuteNonQuery("CREATE TABLE Snippet (ID integer PRIMARY KEY, Root text, Name text, Description text, Code varchar, Category varchar, DateChanged varchar)");

            return _db.FillDataset("select * from " + TableName).ToList<Entry>();
        }

        public void DeleteItem(string key, Int64 id)
        {
            _db.ExecuteNonQuery(String.Format("delete from {0} where {1};", TableName, key + "=" + id));
        }

        public Int64 ModifyItem(Entry item, string key, Int64 id)
        {
            Dictionary<string, string> dict = item.GetType()
                .GetFields()
                .ToDictionary(x => x.Name,
                              x => x.GetValue(item).ToString().Replace("'", "''"));

            if (_db.ExecuteScalar(String.Format("select {0} from {1} where {2}={3}", key, TableName, key, id)) == string.Empty)
            {
                dict.Remove("ID");
                _db.Insert(TableName, dict);
                return _db.GetLastInsertRowId();
            }
            _db.Update(TableName, dict, String.Format("{0}={1}", key, id));
            return id;
        }

        #endregion
    }

    public static class DataTableExtensions
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<FieldInfo> properties = typeof (T).GetFields().ToList();
            return (from object row in table.Rows select CreateItemFromRow<T>((DataRow) row, properties)).ToList();
        }

        private static T CreateItemFromRow<T>(DataRow row, IEnumerable<FieldInfo> properties) where T : new()
        {
            var item = new T();
            foreach (FieldInfo property in properties)
                property.SetValue(item, row[property.Name]);

            return item;
        }
    }
}