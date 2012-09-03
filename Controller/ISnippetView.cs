using System.Collections.Generic;
using System.Windows.Forms;
using Model;

namespace Controller
{
    public interface ISnippetView
    {
        string SearchBoxText { set; }
        TreeView GetTreeView { get; }
        ListView GetListView { get; }
        UserControl GetUserControl { get; }
        Entry EntryItem { get; set; }

        void SetController(SnippetController controller);
        void FillCategory(IEnumerable<Entry> list);
    }
}