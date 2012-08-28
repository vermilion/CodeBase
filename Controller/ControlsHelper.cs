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
        public static void PupulateTreeView(TreeView treeView, IEnumerable<Entry> list)
        {
            treeView.Nodes.Clear();
            foreach (var snode in list.Select(item => item.Category).Distinct())
                treeView.Nodes.Add(new TreeNode {Text = snode, ImageIndex = 0});

            treeView.ExpandAll();
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
            foreach (var item in list.Select(x => x).Where(x => x.Category == category))
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
        /// <param name="nodes">nodecollection to look through</param>
        /// <param name="childText">node text</param>
        public static void SelectTreeViewNodeFromPath(TreeNodeCollection nodes, string childText)
        {
            foreach (TreeNode node in nodes)
            {
                if (AreEqual(node.Text, childText, true))
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
            foreach (var item in lw.Items.Cast<ListViewItem>().Where(item => item.Text == name))
            {
                item.Selected = true;
                return;
            }
        }

        /// <summary>
        /// Allows to search by Code or Name in current Category
        /// </summary>
        /// <param name="lw">target lw to show items in</param>
        /// <param name="list">list to search in</param>
        /// <param name="category">Category name</param>
        /// <param name="stringToSearch">search string</param>
        public static void SearchInCategory(ListView lw, IEnumerable<Entry> list, string category, string stringToSearch)
        {
            var varList = list
                .Where(x => x.Category == category)
                .Where(var => AreEqual(var.Code, stringToSearch, true) || AreEqual(var.Name, stringToSearch, true)).ToList();
            PopulateListView(varList, lw, category);
        }

        /// <summary>
        /// Allows to compare ignorecase or not (string.Compare extension)
        /// </summary>
        private static bool AreEqual(string a, string b, bool ignoreCase)
        {
            return string.Compare(a, b, ignoreCase) == 0;
        }
    }
}