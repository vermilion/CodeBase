using System;
using System.Windows.Forms;
using Controller;
using Model;

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

            var view = new MainForm();
            new SnippetController(view, CommunicatorSwitcher.GetCommunicator).LoadView();
            view.ShowDialog();
        }
    }
}