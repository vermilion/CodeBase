using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CodeBase.Properties;
using Model;
using Presenter;

namespace CodeBase
{
    public partial class MainForm : Form, ISnippetView
    {
        private static ITextControl _currentTextControl;

        private ListViewItem _itemOnMouseDown;
        private SnippetPresenter _presenter;

        public MainForm()
        {
            Thread.CurrentThread.CurrentUICulture = GetCultureInfo(ConfigurationManager.AppSettings["Language"]);
            InitializeComponent();

            splitContainer2.Panel2.Controls.Add((UserControl) (_currentTextControl = new TextControlTextEditor()));
            ((UserControl) _currentTextControl).Dock = DockStyle.Fill;
            ((UserControl) _currentTextControl).Visible = false;

            GetListView.Layout += (s, e) => GetListView.Columns[GetListView.Columns.Count - 1].Width = -2;
            exitToolStripMenuItem.Click += (s, e) => Close();
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
                               Root = _currentTextControl.EditLanguage,
                               Name = _currentTextControl.EditName,
                               Description = _currentTextControl.EditDescription,
                               Code = _currentTextControl.EditCode,
                               Category = _currentTextControl.EditCategory,
                               DateChanged = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                               ID = EntryId
                           };
            }
            set
            {
                _currentTextControl.EditLanguage = value.Root;
                _currentTextControl.EditName = value.Name;
                _currentTextControl.EditDescription = value.Description;
                _currentTextControl.EditCode = value.Code;
                _currentTextControl.EditCategory = value.Category;
                EntryId = value.ID;
            }
        }


        public void SetPresenter(SnippetPresenter presenter)
        {
            _presenter = presenter;
        }


        public void FillCategory(IEnumerable<Entry> list)
        {
            _currentTextControl.FillCategory(list, x => (object) x.Category);
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
                SearchBoxText = _presenter.CurrentCategory;
        }

        private void OnSearchTextBoxTextChanged(object s, EventArgs e)
        {
            if (SearchBoxText.Contains(Resources.Search) || SearchBoxText.Length <= 0) return;
            IEnumerable<Entry> entry = ControlsHelper.SearchInCategory(_presenter.Entries, _presenter.CurrentCategory, SearchBoxText);
            ControlsHelper.PopulateListView(entry, GetListView, x => x.Category == _presenter.CurrentCategory);
        }

        private void OnSearchTextBoxMouseDown(object s, MouseEventArgs e)
        {
            if (SearchBoxText.Contains(Resources.Search))
                searchTextBox.Text = string.Empty;
        }

        private void OnTreeViewAfterSelect(object s, TreeViewEventArgs e)
        {
            _presenter.AfterSelectEvent(GetListView, e);
        }

        private void OnSelectedIndexChanged(object s, EventArgs e)
        {
            if (_itemOnMouseDown != null && GetListView.SelectedIndices.Count == 0)
                return;
            _presenter.ListViewSelectNodes((ListView) s);
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
            _presenter.AddNew();
        }

        private void SaveMenuItemClick(object sender, EventArgs e)
        {
            _presenter.Save();
        }

        private void DeleteMenuItemClick(object sender, EventArgs e)
        {
            _presenter.Delete();
        }

        private void EmptyToolStripMenuItemClick(object sender, EventArgs e)
        {
            _currentTextControl.ManageDetailsPanel(false);
        }

        private void DetailsToolStripMenuItemClick(object sender, EventArgs e)
        {
            _currentTextControl.ManageDetailsPanel(true);
        }

        private void CopyCodeToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentTextControl.EditCode))
                Clipboard.SetText(_currentTextControl.EditCode);
        }

        private void SerializeToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (GetListView.SelectedItems.Count > 0 && _presenter.Serialize())
                MessageBox.Show(Resources.Done);
        }

        private void RestoreToolStripMenuItemClick(object sender, EventArgs e)
        {
            _presenter.Deserialize();
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
            SaveSettings("Language", language);
        }

        private static void SaveSettings(string parameter, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Remove(parameter);
            configuration.AppSettings.Settings.Add(parameter, value);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
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