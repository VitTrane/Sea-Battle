using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SeaBattle
{
    public static class Switcher
    {
        public static PageSwitcher pageSwitcher;

        public static void SwitchPage(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }
    }
}
