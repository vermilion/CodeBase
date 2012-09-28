using System;
using System.Collections.Generic;

namespace Model
{
    public interface ICommunicator
    {
        /// <summary>
        /// Allows to convert Dataset to List of class
        /// </summary>
        /// <returns>List of class</returns>
        List<Entry> GetList();

        /// <summary>
        /// Allows to delete item from database by Key
        /// </summary>
        /// <param name="key">unique key name</param>
        /// <param name="id">unique key value</param>
        void DeleteItem(object key, object id);

        /// <summary>
        /// Allows to insert or update item in database by Key
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="key">unique key name</param>
        /// <param name="id">unique key value</param>
        /// <returns>rowid</returns>
        Int64 ModifyItem<T>(T item, object key, object id)
            where T : class;
    }
}