using System.Collections.Generic;
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
        private List<Entry> _cList;

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

        /// <summary>
        /// Id of selected entry item
        /// </summary>
        private string CurrentID { get; set; }

        public string CurrentCategory { get; private set; }

        /// <summary>
        /// Allows to operate with loaded data
        /// </summary>
        private List<Entry> CList
        {
            set
            {
                _view.GetTreeView.BeginUpdate();
                ControlsHelper.PupulateTreeView(_view.GetTreeView, _cList = value);
                _view.FillCategory(_cList);
                _view.GetTreeView.Sort();
                ControlsHelper.PopulateListView(CList, _view.GetListView, CurrentCategory);

                ControlsHelper.SelectTreeViewNodeFromPath(_view.GetTreeView.Nodes, _view.EntryItem.Category);
                ControlsHelper.SelectListViewItemFromPath(_view.GetListView, _view.EntryItem.Name);
                _view.GetTreeView.EndUpdate();
            }
            get { return _cList; }
        }

        /// <summary>
        /// Allows to load data to the form's view
        /// </summary>
        public void LoadView()
        {
            CList = CommunicatorSwitcher.GetCommunicator.GetList();
        }

        /// <summary>
        /// Allows to fill text fields woth default data
        /// </summary>
        public void AddNew()
        {
            CurrentID = "-1"; //new element default ID            
            if (_view.GetListView.SelectedItems.Count > 0)
                _view.GetListView.SelectedItems[0].Selected = false;

            _view.EntryItem = new Entry
            {
                Root = "C#",
                Category = CurrentCategory,
                Name = "New item in " + CurrentCategory
            };

            _view.GetListView.Items.Add(new ListViewItem(_view.EntryItem.Name));
            ControlsHelper.SelectListViewItemFromPath(_view.GetListView, _view.EntryItem.Name);
            _view.GetControl.Visible = true;
        }

        /// <summary>
        /// Allows to delete entry from database
        /// </summary>
        public void Delete()
        {
            if (_view.GetListView.SelectedItems.Count < 1) return;
            _currentCommunicator.DeleteItem("ID", CurrentID);
            LoadView();
            _view.GetControl.Visible = false;
        }

        /// <summary>
        /// Allows to save entry
        /// </summary>
        public void Save()
        {
            if (_view.GetListView.SelectedItems.Count < 1) return;
            _currentCommunicator.InsertOrUpdateItem(_view.EntryItem, "ID", CurrentID);
            LoadView();
        }

        /// <summary>
        /// Allows to serialize selected entry
        /// </summary>
        public bool Serialize()
        {
            if (_view.GetListView.SelectedItems.Count < 1) return false;
            return XmlSerialize.SerializeBaseClass(CList, _view.GetListView.SelectedItems[0].Text, _view.GetTreeView.SelectedNode.Text) != null;
        }

        /// <summary>
        /// Allows to deserialize entry from file
        /// </summary>
        public void Deserialize()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = string.Format("{0}\\serialized\\", Application.StartupPath);

                if (ofd.ShowDialog() != DialogResult.OK) return;
                _view.EntryItem = XmlSerialize.DeserializeBaseClass(_currentCommunicator, ofd.FileName);
            }
            LoadView();
        }


        /// <summary>
        /// Event of choosing new entry
        /// </summary>
        public void AfterSelectEvent(ListView lw, TreeViewEventArgs e)
        {
            CurrentCategory = e.Node.Text;
            ControlsHelper.PopulateListView(CList, lw, e.Node.Text);
            _view.GetControl.Visible = false;

            _view.SearchBoxText = CurrentCategory;
        }


        public void ListViewSelectNodes(object sender)
        {
            var lw = (ListView) sender;
            if (lw.SelectedItems.Count == 0)
            {
                _view.GetControl.Visible = false;
                return;
            }

            try
            {
                CurrentID = CList.Find(item => item.Name == lw.SelectedItems[0].Text).ID;
                _view.EntryItem = CList.Find(i => i.ID == CurrentID);
            }
            catch
            {
                CurrentID = "-1";
                _view.EntryItem = new Entry
                                      {
                                          Name = _view.GetListView.SelectedItems[0].SubItems[0].Text,
                                          Category = CurrentCategory,
                                          Root = "C#"
                                      };
            }
            _view.GetControl.Visible = true;
        }

        public void FilterLw()
        {
            ControlsHelper.SearchInCategory(_view.GetListView, CList, CurrentCategory, _view.SearchBoxText);            
        }
    }
}