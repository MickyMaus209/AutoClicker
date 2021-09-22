using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoClicker.scripts
{
    public class Utils
    {
        private MainWindow mainWindow;
        public bool canChangeHotKey { get; set; }

        public Utils(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.canChangeHotKey = true;
        }

        public void HotKeyChange(bool b)
        {
            foreach (Control c in mainWindow.MainGrid.Children)
            {
                if (c != mainWindow.HotKeyButton)
                {
                    c.IsEnabled = b;
                    this.canChangeHotKey = b;
                }
            }
        }
    }
}
