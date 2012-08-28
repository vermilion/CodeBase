using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using CodeBase.Properties;
using Controller;
using Model;

namespace CodeBase
{
    public partial class MainForm : Form, ISnippetView
    {
        private static ITextControl _currentTextControl;
        private SnippetController _controller;

        public MainForm()
        {
            InitializeComponent();

            panel1.Controls.Add((UserControl) (_currentTextControl = new TextControlTextEditor()));
            ((UserControl) _currentTextControl).Dock = DockStyle.Fill;
            ((UserControl) _currentTextControl).Visible = false;

            activeTreeView.AfterSelect += (s, e) => _controller.AfterSelectEvent(GetListView, e);

            searchTextBox.TextChanged += (s, e) =>
                                             {
                                                 if (!SearchBoxText.Contains("Search in") && SearchBoxText.Length > 0) _controller.FilterLw();
                                             };

            searchTextBox.MouseDown += (s, e) =>
                                           {
                                               if (SearchBoxText.Contains("Search in"))
                                                   searchTextBox.Text = string.Empty;
                                           };

            searchTextBox.Leave += (s, e) =>
                                       {
                                           if (string.IsNullOrEmpty(SearchBoxText)) SearchBoxText = _controller.CurrentCategory;
                                       };

            listView1.SelectedIndexChanged += (s, e) =>
            {
                if (_itemOnMouseDown != null && GetListView.SelectedIndices.Count == 0)
                    return;
                _controller.ListViewSelectNodes(s);
            };

            listView1.Layout += (s, e) => listView1.Columns[listView1.Columns.Count - 1].Width = -2;
            exitToolStripMenuItem.Click += (s, e) => Close();
        }

        private ListViewItem _itemOnMouseDown;
        private void LvTransactionsMouseDown(object sender, MouseEventArgs e)
        {
            _itemOnMouseDown = GetListView.GetItemAt(e.X, e.Y);
        }

        /// <summary>
        /// Allows to select nodes not clicking on them directly
        /// </summary>
        private void TreeViewSelectNodes(object sender, MouseEventArgs e)
        {
            var node = GetTreeView.GetNodeAt(e.X, e.Y);
            if (node != null) GetTreeView.SelectedNode = node;
        }
        #region ISnippetView Members

        /// <summary>
        /// Listview with category items
        /// </summary>
        public ListView GetListView
        {
            get { return listView1; }
        }

        /// <summary>
        /// Usercontrol with dataedit
        /// </summary>
        public UserControl GetControl
        {
            get { return (UserControl) _currentTextControl; }
        }


        /// <summary>
        /// Allows to get current treeview name
        /// </summary>
        public TreeView GetTreeView
        {
            get { return activeTreeView; }
        }

        /// <summary>
        /// Allows to set or get current entry item
        /// </summary>
        public Entry EntryItem
        {
            get
            {
                return new Entry
                           {
                               Root = _currentTextControl.TcLanguage,
                               Name = _currentTextControl.TcName,
                               Description = _currentTextControl.TcDescription,
                               Code = _currentTextControl.TcCode,
                               Category = _currentTextControl.TcCategory,
                               DateChanged = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                           };
            }
            set
            {
                _currentTextControl.TcName = value.Name;
                _currentTextControl.TcDescription = value.Description;
                _currentTextControl.TcCode = value.Code;
                _currentTextControl.TcCategory = value.Category;
                _currentTextControl.TcLanguage = value.Root;
            }
        }

        /// <summary>
        /// Allows to implement MVC's controller in View
        /// </summary>
        /// <param name="controller">current controller</param>
        public void SetController(SnippetController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Allows to fill UserControl combobox with items
        /// </summary>
        /// <param name="list">target list</param>
        public void FillCategory(IEnumerable<Entry> list)
        {
            _currentTextControl.FillCategory(list);
        }

        public string SearchBoxText
        {
            get { return searchTextBox.Text; }
            set { searchTextBox.Text = string.Format("Search in {0}", value); }
        }

        /// <summary>
        /// Allows to select node
        /// </summary>
        public TreeNode SetSelectedNode
        {
            set { activeTreeView.SelectedNode = value; }
        }

        #endregion

        private void NewMenuItemClick(object sender, EventArgs e)
        {
            _controller.AddNew();
        }

        private void SaveMenuItemClick(object sender, EventArgs e)
        {
            _controller.Save();
        }

        private void DeleteMenuItemClick(object sender, EventArgs e)
        {
            _controller.Delete();
        }

        private void EmptyToolStripMenuItemClick(object sender, EventArgs e)
        {
            _currentTextControl.HideDetails();
        }

        private void DetailsToolStripMenuItemClick(object sender, EventArgs e)
        {
            _currentTextControl.ShowDetails();
        }

        private void CopyCodeToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentTextControl.TcCode))
                Clipboard.SetText(_currentTextControl.TcCode);
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (GetListView.SelectedItems.Count > 0 && _controller.Serialize()) MessageBox.Show(Resources.Done);
        }

        private void RestoreToolStripMenuItemClick(object sender, EventArgs e)
        {
            _controller.Deserialize();
        }
    }
}