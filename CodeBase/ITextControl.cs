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
        string SetHighlighting { set; }

        void ShowDetails();
        void HideDetails();
        void FillCategory(IEnumerable<Entry> list);
    }
}