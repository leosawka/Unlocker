using System;
using System.Windows.Forms;
using Unlocker;

namespace Unlocker
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                MessageBox.Show("Please specify a file to unlock.", "Unlocker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UnlockerForm(args[0]));
        }
    }
}
