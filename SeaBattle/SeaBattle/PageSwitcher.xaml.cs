using SeaBattle.Managers;
using SeaBattle.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeaBattle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        public PageSwitcher()
        {
            InitializeComponent();
            Switcher.PageSwitcher = this;
            Switcher.SwitchPage(new Login()); 
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (ClientManager.Instance.Client != null) 
            {
                try
                {
                    ClientManager.Instance.Client.LeaveGame();
                }
                catch (Exception ex)
                {
                    string message = string.Format("{0} \n {1},\n {2}",ex.Message,
                    ex.ToString(), ex.StackTrace);
                    ClientManager.Instance.Logger.WriteLineError(message);
                }

                ClientManager.Instance.Dispose();
            }

            base.OnClosing(e);
        }
    }
}
