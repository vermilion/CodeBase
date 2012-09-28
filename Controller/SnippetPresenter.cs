using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace Presenter
{
    public class SnippetPresenter
    {
        private readonly ICommunicator _communicator;
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
        public SnippetPresenter(ISnippetView view, ICommunicator communicator)
        {
            _communicator = communicator;
            _view = view;
            view.SetPresenter(this);
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
            Entries = _communicator.GetList();
        }

        /// <summary>
        /// Allows to fill text fields woth default data
        /// </summary>
        public void AddNew()
        {
            _view.EntryItem = new Entry
                                  {
                                      Category = CurrentCategory,
                                      Name = "New item in " + CurrentCategory
                                  };

            ControlsHelper.AddListViewItem(_view.GetListView, _view.EntryItem, true);
            ControlsHelper.SelectListViewItem(_view.GetListView, x => Equals(x.Tag, _view.EntryItem.ID));
            _view.GetUserControl.Visible = true;
        }

        /// <summary>
        /// Allows to delete entry from database
        /// </summary>
        public void Delete()
        {
            if (_view.GetListView.SelectedItems.Count == 0) return;
            _communicator.DeleteItem("ID", _view.EntryItem.ID);
            LoadView();
            _view.GetUserControl.Visible = false;
        }

        /// <summary>
        /// Allows to save entry
        /// </summary>
        public void Save()
        {
            if (_view.GetListView.SelectedItems.Count == 0) return;
            Entry item = _view.EntryItem;
            item.ID = _communicator.ModifyItem(_view.EntryItem, "ID", _view.EntryItem.ID);
            _view.EntryItem = item;
            LoadView();
        }

        /// <summary>
        /// Allows to serialize selected entry
        /// </summary>
        /// <returns>true is serialization succeeded</returns>
        public bool Serialize()
        {
            return _view.GetListView.SelectedItems.Count != 0 && _view.GetListView.SelectedItems[0].Tag != null
                   && XmlSerialize.SerializeBaseClass(Entries, _view.GetListView.SelectedItems[0].Tag) != null;
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
                _view.EntryItem = XmlSerialize.DeserializeBaseClass(_communicator, dialog.FileName);
            }
            LoadView();
        }

        /// <summary>
        /// Event of choosing new entry
        /// </summary>
        public void AfterSelectEvent(ListView lw, TreeViewEventArgs e)
        {
            CurrentCategory = e.Node.Text;
            ControlsHelper.PopulateListView(Entries, lw, x => x.Category == CurrentCategory);
            _view.GetUserControl.Visible = false;

            _view.SearchBoxText = CurrentCategory;
        }


        public void ListViewSelectNodes(ListView lw)
        {
            if (lw.SelectedItems.Count == 0)
            {
                _view.GetUserControl.Visible = false;
                return;
            }

            try
            {
                _view.EntryItem = Entries.First(x => Equals(x.ID, lw.SelectedItems[0].Tag));
            }
            catch
            {
                _view.EntryItem = new Entry
                                      {
                                          Name = _view.GetListView.SelectedItems[0].SubItems[0].Text,
                                          Category = CurrentCategory,
                                      };
            }
            _view.GetUserControl.Visible = true;
        }
    }
}