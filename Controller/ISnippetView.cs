using System.Collections.Generic;
using System.Windows.Forms;
using Model;

namespace Controller
{
    public interface ISnippetView
    {
        string SearchBoxText { get; set; }

        TreeNode SetSelectedNode { set; }
        TreeView GetTreeView { get; }

        ListView GetListView { get; }
        UserControl GetControl { get; }

        Entry EntryItem { get; set; }

        void SetController(SnippetController controller);

        void FillCategory(IEnumerable<Entry> list);
    }
}