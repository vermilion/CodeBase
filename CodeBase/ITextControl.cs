using System.Collections.Generic;
using Model;

namespace CodeBase
{
    public interface ITextControl
    {
        string TcLanguage { get; set; }
        string TcName { get; set; }
        string TcDescription { get; set; }
        string TcCode { get; set; }
        string TcCategory { get; set; }

        void ShowDetails();
        void HideDetails();

        /// <summary>
        /// Allows to fill category combobox
        /// </summary>
        /// <param name="list">input list</param>
        void FillCategory(IEnumerable<Entry> list);

        void ApplyResources();
    }
}