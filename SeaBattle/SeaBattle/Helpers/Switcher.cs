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
        private static PageSwitcher pageSwitcher;

        /// <summary>
        /// Переходит на заданную страницу
        /// </summary>
        /// <param name="newPage">Страница на которую нужно перейти</param>
        public static void SwitchPage(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }
    }
}
