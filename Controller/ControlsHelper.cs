using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace Presenter
{
    public static class ControlsHelper
    {
        /// <summary>
        /// Allows to populate treeview
        /// </summary>
        /// <param name="treeView">treeview name</param>
        /// <param name="list">List populating from</param>
        /// <param name="f">Lambda expression</param>
        private static void PupulateTreeView(TreeView treeView, IEnumerable<Entry> list, Func<Entry, string> f)
        {
            treeView.Nodes.Clear();
            list
                .Select(f)
                .OrderBy(x => x)
                .Distinct()
                .ToList()
                .ForEach(x => treeView.Nodes.Add(new TreeNode {Text = x, ImageIndex = 1}));
        }

        /// <summary>
        /// Allows to populate listview
        /// </summary>
        /// <param name="list">list with values</param>
        /// <param name="listView">target listview</param>
        /// <param name="f">Lambda expression</param>
        public static void PopulateListView(IEnumerable<Entry> list, ListView listView, Func<Entry, bool> f)
        {
            listView.Items.Clear();
            list
                .Where(f)
                .ToList()
                .ForEach(x => AddListViewItem(listView, x, false));
        }

        /// <summary>
        /// Allows to add items to ListView
        /// </summary>
        /// <param name="listView">target listview</param>
        /// <param name="item">current entry</param>
        /// <param name="isNew">Empty if new item</param>
        public static void AddListViewItem(ListView listView, Entry item, bool isNew)
        {
            var listViewItem = new ListViewItem(item.Name);
            listViewItem.SubItems.Add(item.Root);
            listViewItem.Tag = item.ID;
            if (!isNew)
            {
                listViewItem.SubItems.Add(item.DateChanged);
                listViewItem.SubItems.Add(item.Description);
                listViewItem.ToolTipText = item.Code;
                listViewItem.ImageIndex = 0;
            }
            listView.Items.Add(listViewItem);
        }

        /// <summary>
        /// Allows to find and select target node
        /// </summary>
        /// <param name="treeView">treeview nodecollection to look through</param>
        /// <param name="text">node text</param>
        private static void SelectTreeViewNodeByNodeName(TreeView treeView, string text)
        {
            treeView.SelectedNode = treeView.Nodes
                .OfType<TreeNode>()
                .FirstOrDefault(x => x.Text == text);
        }

        /// <summary>
        /// Allows to select item in listview
        /// </summary>
        /// <param name="listView">target listview</param>
        /// <param name="f">Lambda expression</param>
        public static void SelectListViewItem(ListView listView, Func<ListViewItem, bool> f)
        {
            ListViewItem item = listView.Items
                .Cast<ListViewItem>()
                .Where(f)
                .FirstOrDefault();
            if (item != null) item.Selected = true;
        }

        /// <summary>
        /// Allows to search by Code or Name in current Category
        /// </summary>
        /// <param name="list">list to search in</param>
        /// <param name="category">Category name</param>
        /// <param name="stringToSearch">search string</param>
        public static IEnumerable<Entry> SearchInCategory(IEnumerable<Entry> list, string category, string stringToSearch)
        {
            return list
                .Where(x => x.Category == category)
                .Where(x => x.Code.Contains(stringToSearch, true) || x.Name.Contains(stringToSearch, true))
                .ToList();
        }

        /// <summary>
        /// Allows to update ListView with data from selected category
        /// </summary>
        /// <param name="view">current view</param>
        /// <param name="currentCategory">category</param>
        /// <param name="list"></param>
        public static void EntriesChanged(ISnippetView view, string currentCategory, List<Entry> list)
        {
            view.GetTreeView.BeginUpdate();
            PupulateTreeView(view.GetTreeView, list, x => x.Category);
            SelectTreeViewNodeByNodeName(view.GetTreeView, view.EntryItem.Category);
            view.GetTreeView.EndUpdate();
            view.FillCategory(list);
            PopulateListView(list, view.GetListView, x => x.Category == currentCategory);
            SelectListViewItem(view.GetListView, x => Equals(x.Tag, view.EntryItem.ID));
        }

        /// <summary>
        /// Allows to find occurences ignorecase or not
        /// </summary>
        private static bool Contains(this string a, string b, bool ignoreCase)
        {
            return ignoreCase ? a.ToUpper().Contains(b.ToUpper()) : a.Contains(b);
        }
    }
}