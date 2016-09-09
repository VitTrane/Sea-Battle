using SeaBattle.Helpers;
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

namespace SeaBattle.Pages
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        private void backTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Validator.IsValidUsername(usernameTextBox.Text))
            {
                errorMessageTextBlock.Text = "Такой логин не подходит";
                return;
            }
            if (!Validator.IsValidMailAddress(emailTextBox.Text))
            {
                errorMessageTextBlock.Text = "Вы ввели email неправильного формата";
                return;
            }
            if (!Validator.IsValidPassword(passwordBox.Password, 6))
            {
                errorMessageTextBlock.Text = "Пароль должен иметь от 6 до 16 символов";
                return;
            }

            Switcher.SwitchPage(new Login());
        }
    }
}
