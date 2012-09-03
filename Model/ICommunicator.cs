using System.Collections.Generic;
using System.Configuration;

namespace Model
{
    public interface ICommunicator
    {
        string FileName { get; }
        List<Entry> GetList();
        void DeleteItem(string key, string id);
        void InsertOrUpdateItem(Entry item, string key, string id);
    }

    public static class CommunicatorSwitcher
    {
        /// <summary>
        /// Allows to choose current communicator
        /// </summary>
        public static ICommunicator GetCommunicator
        {
            get
            {
                switch (ConfigurationManager.AppSettings["Communicator"])
                {
                    case "SQLite":
                        return new SQLiteCommunicator();
                    case "XML":
                        return new XMLCommunicator();
                    default:
                        return new SQLiteCommunicator();
                }
            }
        }
    }
}