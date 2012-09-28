using System;
using System.Windows.Forms;
using Model;
using Presenter;

namespace CodeBase
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var view = new MainForm())
            {
                new SnippetPresenter(view, new SQLiteCommunicator()).LoadView();
                Application.Run(view);
            }
        }
    }
}