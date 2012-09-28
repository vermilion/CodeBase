using System.Collections.Generic;
using System.Windows.Forms;
using Model;

namespace Presenter
{
    public interface ISnippetView
    {
        string SearchBoxText { set; }

        /// <summary>
        /// Allows to get current treeview name
        /// </summary>
        TreeView GetTreeView { get; }

        /// <summary>
        /// Listview with category items
        /// </summary>
        ListView GetListView { get; }

        /// <summary>
        /// Usercontrol with dataedit
        /// </summary>
        UserControl GetUserControl { get; }

        /// <summary>
        /// Allows to set or get current entry item
        /// </summary>
        Entry EntryItem { get; set; }

        /// <summary>
        /// Allows to implement MVC's presenter in View
        /// </summary>
        /// <param name="presenter">current presenter</param>
        void SetPresenter(SnippetPresenter presenter);

        /// <summary>
        /// Allows to fill GetUserControl combobox with items
        /// </summary>
        /// <param name="list">target list</param>
        void FillCategory(IEnumerable<Entry> list);
    }
}