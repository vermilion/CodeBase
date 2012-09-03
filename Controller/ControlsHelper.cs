using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace Controller
{
    public static class ControlsHelper
    {
        /// <summary>
        /// Allows to populate treeview
        /// </summary>
        /// <param name="treeView">treeview name</param>
        /// <param name="list">List populating from</param>
        private static void PupulateTreeView(TreeView treeView, IEnumerable<Entry> list)
        {
            treeView.Nodes.Clear();
            foreach (var text in list.Select(item => item.Category).OrderBy(x => x).Distinct())
                treeView.Nodes.Add(new TreeNode {Text = text, ImageIndex = 0});

            //treeView.ExpandAll();
        }

        /// <summary>
        /// Allows to populate listview
        /// </summary>
        /// <param name="list">list with values</param>
        /// <param name="lw">target listview</param>
        /// <param name="category">current category</param>
        public static void PopulateListView(IEnumerable<Entry> list, ListView lw, string category)
        {
            lw.Items.Clear();
            foreach (var item in list.Where(x => x.Category == category))
            {
                var listViewItem = new ListViewItem(item.Name);
                listViewItem.SubItems.Add(item.Root);
                listViewItem.SubItems.Add(item.DateChanged);
                listViewItem.SubItems.Add(item.Description);
                listViewItem.ToolTipText = item.Code;
                lw.Items.Add(listViewItem);
            }
        }


        /// <summary>
        /// Recursive method to find current node
        /// </summary>
        /// <param name="nodeCollection">nodecollection to look through</param>
        /// <param name="childText">node text</param>
        private static void SelectTreeViewNodeFromPath(TreeNodeCollection nodeCollection, string childText)
        {
            foreach (TreeNode node in nodeCollection)
            {
                if (string.Compare(node.Text, childText, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    node.TreeView.SelectedNode = node;
                    return;
                }
                SelectTreeViewNodeFromPath(node.Nodes, childText);
            }
        }

        /// <summary>
        /// Allows to select item in listview
        /// </summary>
        /// <param name="lw">listview</param>
        /// <param name="name">string Name to search</param>
        public static void SelectListViewItemFromPath(ListView lw, string name)
        {
            foreach (var item in lw.Items.Cast<ListViewItem>().Where(x => x.Text == name))
            {
                item.Selected = true;
                return;
            }
        }

        /// <summary>
        /// Allows to search by Code or Name in current Category
        /// </summary>
        /// <param name="list">list to search in</param>
        /// <param name="currentCategory">Category name</param>
        /// <param name="stringToSearch">search string</param>
        public static IEnumerable<Entry> SearchInCategory(IEnumerable<Entry> list, string currentCategory, string stringToSearch)
        {
            return list
                .Where(x => x.Category == currentCategory)
                .Where(x => Contains(x.Code, stringToSearch) || Contains(x.Name, stringToSearch)).ToList();            
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
            PupulateTreeView(view.GetTreeView, list);
            view.FillCategory(list);

            PopulateListView(list, view.GetListView, currentCategory);

            SelectTreeViewNodeFromPath(view.GetTreeView.Nodes, view.EntryItem.Category);
            SelectListViewItemFromPath(view.GetListView, view.EntryItem.Name);
            view.GetTreeView.EndUpdate();
        }

        /// <summary>
        /// Allows to find occurences ignorecase
        /// </summary>
        private static bool Contains(string a, string b)
        {
            return a.ToUpper().Contains(b.ToUpper());
        }
    }
}