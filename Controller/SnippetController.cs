using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace Controller
{
    public class SnippetController
    {
        private readonly ICommunicator _currentCommunicator;
        private readonly ISnippetView _view;

        /// <summary>
        /// Main data storage
        /// </summary>
        private List<Entry> _entries;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view">MVC view</param>
        /// <param name="communicator">MVC model's communicate class</param>
        public SnippetController(ISnippetView view, ICommunicator communicator)
        {
            _currentCommunicator = communicator;
            _view = view;
            view.SetController(this);
        }

        public string CurrentCategory { get; private set; }

        /// <summary>
        /// Allows to operate with loaded data
        /// </summary>
        public List<Entry> Entries
        {
            private set { ControlsHelper.EntriesChanged(_view, CurrentCategory, _entries = value); }
            get { return _entries; }
        }


        /// <summary>
        /// Allows to load data to the form's view
        /// </summary>
        public void LoadView()
        {
            Entries = CommunicatorSwitcher.GetCommunicator.GetList();
        }

        /// <summary>
        /// Allows to fill text fields woth default data
        /// </summary>
        public void AddNew()
        {
            _view.EntryItem = new Entry
                                  {
                                      Root = "C#",
                                      Category = CurrentCategory,
                                      Name = "New item in " + CurrentCategory
                                  };

            _view.GetListView.Items.Add(new ListViewItem(_view.EntryItem.Name));
            ControlsHelper.SelectListViewItemFromPath(_view.GetListView, _view.EntryItem.Name);
            _view.GetUserControl.Visible = true;
        }

        /// <summary>
        /// Allows to delete entry from database
        /// </summary>
        public void Delete()
        {
            if (_view.GetListView.SelectedItems.Count == 0) return;
            _currentCommunicator.DeleteItem("ID", _view.EntryItem.ID);
            LoadView();
            _view.GetUserControl.Visible = false;
        }

        /// <summary>
        /// Allows to save entry
        /// </summary>
        public void Save()
        {
            if (_view.GetListView.SelectedItems.Count == 0) return;
            _currentCommunicator.InsertOrUpdateItem(_view.EntryItem, "ID", _view.EntryItem.ID);
            LoadView();
        }

        /// <summary>
        /// Allows to serialize selected entry
        /// </summary>
        public bool Serialize()
        {
            if (_view.GetListView.SelectedItems.Count == 0) return false;
            return XmlSerialize.SerializeBaseClass(Entries, _view.GetListView.SelectedItems[0].Text, _view.GetTreeView.SelectedNode.Text) != null;
        }

        /// <summary>
        /// Allows to deserialize entry from file
        /// </summary>
        public void Deserialize()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = string.Format("{0}\\serialized\\", Application.StartupPath);

                if (dialog.ShowDialog() != DialogResult.OK) return;
                _view.EntryItem = XmlSerialize.DeserializeBaseClass(_currentCommunicator, dialog.FileName);
            }
            LoadView();
        }


        /// <summary>
        /// Event of choosing new entry
        /// </summary>
        public void AfterSelectEvent(ListView lw, TreeViewEventArgs e)
        {
            CurrentCategory = e.Node.Text;
            ControlsHelper.PopulateListView(Entries, lw, e.Node.Text);
            _view.GetUserControl.Visible = false;

            _view.SearchBoxText = CurrentCategory;
        }


        public void ListViewSelectNodes(object sender)
        {
            var lw = (ListView) sender;
            if (lw.SelectedItems.Count == 0)
            {
                _view.GetUserControl.Visible = false;
                return;
            }

            try
            {
                _view.EntryItem = Entries.First(x => x.Name == lw.SelectedItems[0].Text);
            }
            catch
            {
                _view.EntryItem = new Entry
                                      {
                                          Name = _view.GetListView.SelectedItems[0].SubItems[0].Text,
                                          Category = CurrentCategory,
                                          Root = "C#"
                                      };
            }
            _view.GetUserControl.Visible = true;
        }
    }
}