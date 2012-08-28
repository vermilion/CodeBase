namespace sliver.SnippetCompiler.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static Bitmap Build
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Build", resourceCulture);
            }
        }

        internal static Bitmap BuildAll
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("BuildAll", resourceCulture);
            }
        }

        internal static Bitmap CSharpFile
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("CSharpFile", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string DefaultTemplate_CSharp
        {
            get
            {
                return ResourceManager.GetString("DefaultTemplate_CSharp", resourceCulture);
            }
        }

        internal static string DefaultTemplate_JScript
        {
            get
            {
                return ResourceManager.GetString("DefaultTemplate_JScript", resourceCulture);
            }
        }

        internal static string DefaultTemplate_VisualBasic
        {
            get
            {
                return ResourceManager.GetString("DefaultTemplate_VisualBasic", resourceCulture);
            }
        }

        internal static Bitmap Find
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Find", resourceCulture);
            }
        }

        internal static Bitmap FindNext
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("FindNext", resourceCulture);
            }
        }

        internal static Bitmap FolderClosed
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("FolderClosed", resourceCulture);
            }
        }

        internal static Bitmap Help
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Help", resourceCulture);
            }
        }

        internal static Bitmap KillProcess
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("KillProcess", resourceCulture);
            }
        }

        internal static string LexerXml_JScript
        {
            get
            {
                return ResourceManager.GetString("LexerXml_JScript", resourceCulture);
            }
        }

        internal static Bitmap NewFile
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("NewFile", resourceCulture);
            }
        }

        internal static Bitmap Open
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Open", resourceCulture);
            }
        }

        internal static Bitmap Output
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Output", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("sliver.SnippetCompiler.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap Start
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Start", resourceCulture);
            }
        }

        internal static Bitmap StartAll
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("StartAll", resourceCulture);
            }
        }

        internal static Bitmap VBFile
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("VBFile", resourceCulture);
            }
        }

        internal static Bitmap Website
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Website", resourceCulture);
            }
        }

        internal static Bitmap YourFeedback
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("YourFeedback", resourceCulture);
            }
        }
    }
}

