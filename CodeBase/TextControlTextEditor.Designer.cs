using CodeBase.Properties;

namespace CodeBase
{
    partial class TextControlTextEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._teCode = new ICSharpCode.TextEditor.TextEditorControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this._tbCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this._tbDescription = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this._tbName = new System.Windows.Forms.TextBox();
            this._cbLanguage = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _teCode
            // 
            this._teCode.CausesValidation = false;
            this._teCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._teCode.EnableFolding = false;
            this._teCode.IndentStyle = ICSharpCode.TextEditor.Document.IndentStyle.Auto;
            this._teCode.IsReadOnly = false;
            this._teCode.Location = new System.Drawing.Point(0, 112);
            this._teCode.Name = "_teCode";
            this._teCode.ShowHRuler = true;
            this._teCode.Size = new System.Drawing.Size(749, 432);
            this._teCode.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this._tbCategory);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this._tbDescription);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(749, 69);
            this.panel1.TabIndex = 7;
            // 
            // _tbCategory
            // 
            this._tbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbCategory.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._tbCategory.Location = new System.Drawing.Point(86, 42);
            this._tbCategory.Name = "_tbCategory";
            this._tbCategory.Size = new System.Drawing.Size(660, 21);
            this._tbCategory.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(3, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Category:";
            // 
            // _tbDescription
            // 
            this._tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbDescription.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._tbDescription.Location = new System.Drawing.Point(3, 3);
            this._tbDescription.Multiline = true;
            this._tbDescription.Name = "_tbDescription";
            this._tbDescription.Size = new System.Drawing.Size(743, 33);
            this._tbDescription.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this._tbName);
            this.panel2.Controls.Add(this._cbLanguage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(749, 112);
            this.panel2.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackgroundImage = global::CodeBase.Properties.Resources.ImageExpand;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Location = new System.Drawing.Point(721, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 25);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = global::CodeBase.Properties.Resources.ImageCollapse;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Location = new System.Drawing.Point(696, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // _tbName
            // 
            this._tbName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbName.BackColor = System.Drawing.SystemColors.Control;
            this._tbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._tbName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._tbName.Location = new System.Drawing.Point(12, 10);
            this._tbName.Name = "_tbName";
            this._tbName.Size = new System.Drawing.Size(593, 22);
            this._tbName.TabIndex = 1;
            // 
            // _cbLanguage
            // 
            this._cbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbLanguage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._cbLanguage.Items.AddRange(new object[] {
            "C#",
            "VBNET",
            "ASP/XHTML",
            "HTML",
            "C++.NET",
            "Java",
            "JavaScript",
            "PHP",
            "XML",
            "BAT",
            "Other"});
            this._cbLanguage.Location = new System.Drawing.Point(611, 10);
            this._cbLanguage.Name = "_cbLanguage";
            this._cbLanguage.Size = new System.Drawing.Size(79, 21);
            this._cbLanguage.TabIndex = 2;
            // 
            // TextControlTextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._teCode);
            this.Controls.Add(this.panel2);
            this.Name = "TextControlTextEditor";
            this.Size = new System.Drawing.Size(749, 544);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        public void ApplyResources()
        {
            this.label5.Text = Resources.Category;
        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl _teCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox _tbDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox _tbCategory;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox _tbName;
        private System.Windows.Forms.ComboBox _cbLanguage;
        private System.Windows.Forms.Button button2;
    }
}
