using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
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

        private ListViewItem _itemOnMouseDown;

        public MainForm()
        {
            Thread.CurrentThread.CurrentUICulture = GetCultureInfo(ConfigurationManager.AppSettings["Language"]);
            InitializeComponent();

            splitContainer2.Panel2.Controls.Add((UserControl) (_currentTextControl = new TextControlTextEditor()));
            ((UserControl) _currentTextControl).Dock = DockStyle.Fill;
            ((UserControl) _currentTextControl).Visible = false;

            GetListView.Layout += (s, e) => GetListView.Columns[GetListView.Columns.Count - 1].Width = -2;
            exitToolStripMenuItem.Click += (s, e) => Close();

            //GetListView.SmallImageList = imageList2;
        }

        /// <summary>
        /// Entry Id property
        /// </summary>
        private Int64 EntryId { get; set; }

        #region ISnippetView Members

        public ListView GetListView
        {
            get { return listView1; }
        }

        public UserControl GetUserControl
        {
            get { return (UserControl) _currentTextControl; }
        }

        public TreeView GetTreeView
        {
            get { return activeTreeView; }
        }

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
                               DateChanged = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                               ID = EntryId
                           };
            }
            set
            {
                _currentTextControl.TcName = value.Name;
                _currentTextControl.TcDescription = value.Description;
                _currentTextControl.TcCode = value.Code;
                _currentTextControl.TcCategory = value.Category;
                _currentTextControl.TcLanguage = value.Root;
                EntryId = value.ID;
            }
        }


        public void SetController(SnippetController controller)
        {
            _controller = controller;
        }


        public void FillCategory(IEnumerable<Entry> list)
        {
            _currentTextControl.FillCategory(list);
        }

        public string SearchBoxText
        {
            private get { return searchTextBox.Text; }
            set { searchTextBox.Text = string.Format(Resources.Search + " {0}", value); }
        }

        #endregion

        private void OnSearchTextBoxLeave(object s, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchBoxText))
                SearchBoxText = _controller.CurrentCategory;
        }

        private void OnSearchTextBoxTextChanged(object s, EventArgs e)
        {
            if (SearchBoxText.Contains(Resources.Search) || SearchBoxText.Length <= 0) return;
            IEnumerable<Entry> entry = ControlsHelper.SearchInCategory(_controller.Entries, _controller.CurrentCategory, SearchBoxText);
            ControlsHelper.PopulateListView(entry, GetListView, _controller.CurrentCategory);
        }

        private void OnSearchTextBoxMouseDown(object s, MouseEventArgs e)
        {
            if (SearchBoxText.Contains(Resources.Search))
                searchTextBox.Text = string.Empty;
        }

        private void OnTreeViewAfterSelect(object s, TreeViewEventArgs e)
        {
            _controller.AfterSelectEvent(GetListView, e);
        }

        private void OnSelectedIndexChanged(object s, EventArgs e)
        {
            if (_itemOnMouseDown != null && GetListView.SelectedIndices.Count == 0)
                return;
            _controller.ListViewSelectNodes((ListView) s);
        }

        private void LvTransactionsMouseDown(object sender, MouseEventArgs e)
        {
            _itemOnMouseDown = GetListView.GetItemAt(e.X, e.Y);
        }

        /// <summary>
        /// Allows to select nodes not clicking on them directly
        /// </summary>
        private void TreeViewSelectNodes(object sender, MouseEventArgs e)
        {
            TreeNode node = GetTreeView.GetNodeAt(e.X, e.Y);
            if (node != null) GetTreeView.SelectedNode = node;
        }

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

        private void SerializeToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (GetListView.SelectedItems.Count > 0 && _controller.Serialize())
                MessageBox.Show(Resources.Done);
        }

        private void RestoreToolStripMenuItemClick(object sender, EventArgs e)
        {
            _controller.Deserialize();
        }

        private void EnglishToolStripMenuItemClick(object sender, EventArgs e)
        {
            russianToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = true;
            ApplyCulture("en");
        }

        private void RussianToolStripMenuItemClick(object sender, EventArgs e)
        {
            russianToolStripMenuItem.Checked = true;
            englishToolStripMenuItem.Checked = false;
            ApplyCulture("ru");
        }

        private void ApplyCulture(string language)
        {
            Thread.CurrentThread.CurrentUICulture = GetCultureInfo(language);
            ApplyResources();
            _currentTextControl.ApplyResources();
        }

        private void MainFormResize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        private void NotifyIconDoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
            else
            {
                Hide();
                WindowState = FormWindowState.Minimized;
            }
        }
    }
}