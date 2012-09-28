using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Model;

namespace CodeBase
{
    public partial class TextControlTextEditor : UserControl, ITextControl
    {
        public TextControlTextEditor()
        {
            InitializeComponent();
            ManageDetailsPanel(false);

            _cbLanguage.SelectedIndexChanged += (s, e) => SetHighlighting = ((ComboBox) s).Text;
        }

        /// <summary>
        /// Allows to set editor Highlight language
        /// </summary>
        private string SetHighlighting
        {
            set
            {
                switch (value)
                {
                    case "C#":
                        _teCode.Language = Language.CSharp;
                        break;
                    case "VB":
                        _teCode.Language = Language.VB;
                        break;
                    case "HTML":
                        _teCode.Language = Language.HTML;
                        break;
                    case "SQL":
                        _teCode.Language = Language.SQL;
                        break;
                    case "PHP":
                        _teCode.Language = Language.PHP;
                        break;
                    case "JS":
                        _teCode.Language = Language.JS;
                        break;
                    default:
                        _teCode.Language = Language.Custom;
                        break;
                }
                _teCode.OnSyntaxHighlight(new TextChangedEventArgs(_teCode.Range));
            }
        }

        #region ITextControl Members

        public string EditLanguage
        {
            get { return _cbLanguage.Text; }
            set
            {
                _cbLanguage.Text = value;
                SetHighlighting = _cbLanguage.Text;
            }
        }

        public string EditName
        {
            get { return _tbName.Text; }
            set { _tbName.Text = value; }
        }

        public string EditDescription
        {
            get { return _tbDescription.Text; }
            set { _tbDescription.Text = value; }
        }

        public string EditCode
        {
            get { return _teCode.Text; }
            set
            {
                _teCode.Text = value;
                _teCode.Selection.Start = Place.Empty;
                _teCode.DoCaretVisible();
            }
        }

        public string EditCategory
        {
            get { return _tbCategory.Text; }
            set { _tbCategory.Text = value; }
        }


        public void FillCategory(IEnumerable<Entry> list, Func<Entry, object> f)
        {
            _tbCategory.Items.Clear();
            _tbCategory.Items.AddRange(list.Select(f).Distinct().OrderBy(x => x).ToArray());
        }

        public void ManageDetailsPanel(bool show)
        {
            if (show)
            {
                panel1.Visible = true;
                panel2.Height = 112;
            }
            else
            {
                panel1.Visible = false;
                panel2.Height = 34;
            }
        }

        #endregion

        private void Button1Click(object sender, EventArgs e)
        {
            ManageDetailsPanel(false);
        }

        private void Button2Click(object sender, EventArgs e)
        {
            ManageDetailsPanel(true);
        }

        private void AutoIndentCurrentToolStripMenuItemClick(object sender, EventArgs e)
        {
            _teCode.DoAutoIndent();
        }

        private void IncreaseIndentToolStripMenuItemClick(object sender, EventArgs e)
        {
            _teCode.IncreaseIndent();
        }

        private void DecreaseIndentToolStripMenuItemClick(object sender, EventArgs e)
        {
            _teCode.DecreaseIndent();
        }
    }
}