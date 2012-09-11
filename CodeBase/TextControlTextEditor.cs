using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace CodeBase
{
    public partial class TextControlTextEditor : UserControl, ITextControl
    {
        public TextControlTextEditor()
        {
            InitializeComponent();
            HideDetails();

            _cbLanguage.SelectedIndexChanged += (s, e) => SetHighlighting = ((ComboBox) s).Text;
        }

        /// <summary>
        /// Allows to set editor Highlight language
        /// </summary>
        private string SetHighlighting
        {
            set
            {
                _teCode.SetHighlighting(value);
                _teCode.Refresh();
            }
        }

        #region ITextControl Members

        public string TcLanguage
        {
            get { return _cbLanguage.Text; }
            set
            {
                _cbLanguage.Text = value;
                SetHighlighting = _cbLanguage.Text;
            }
        }

        public string TcName
        {
            get { return _tbName.Text; }
            set { _tbName.Text = value; }
        }

        public string TcDescription
        {
            get { return _tbDescription.Text; }
            set { _tbDescription.Text = value; }
        }

        public string TcCode
        {
            get { return _teCode.Text; }
            set
            {
                _teCode.BeginUpdate();
                _teCode.Text = value;
                _teCode.EndUpdate();
            }
        }

        public string TcCategory
        {
            get { return _tbCategory.Text; }
            set { _tbCategory.Text = value; }
        }


        public void FillCategory(IEnumerable<Entry> list)
        {
            _tbCategory.Items.Clear();
            _tbCategory.Items.AddRange(list.Select(x => (object) x.Category).Distinct().OrderBy(x => x).ToArray());
        }

        public void HideDetails()
        {
            panel1.Visible = false;
            panel2.Height = 38;
        }

        public void ShowDetails()
        {
            panel1.Visible = true;
            panel2.Height = 112;
        }

        #endregion

        private void Button1Click(object sender, EventArgs e)
        {
            HideDetails();
        }

        private void Button2Click(object sender, EventArgs e)
        {
            ShowDetails();
        }
    }
}