using System;
using System.Collections.Generic;
using Model;

namespace CodeBase
{
    public interface ITextControl
    {
        string EditLanguage { get; set; }
        string EditName { get; set; }
        string EditDescription { get; set; }
        string EditCode { get; set; }
        string EditCategory { get; set; }

        void ManageDetailsPanel(bool show);

        /// <summary>
        /// Allows to fill category combobox
        /// </summary>
        /// <param name="list">input list</param>
        /// <param name="f">Lambda expression</param>
        void FillCategory(IEnumerable<Entry> list, Func<Entry, object> f);

        void ApplyResources();
    }
}