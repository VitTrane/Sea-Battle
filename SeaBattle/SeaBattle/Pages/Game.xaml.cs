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
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : UserControl
    {
        Grid[][] map = new Grid[10][];

        public Game()
        {
            InitializeComponent();
            setMapNames(seaPlayer);
            setBattleMap(seaPlayer);

            setMapNames(seaOpponent);
            setBattleMap(seaOpponent);   
        }

        private void setMapNames(Grid sea)
        {
            sea.Children.Add(textGrid("А", 0, 1));
            sea.Children.Add(textGrid("Б", 0, 2));
            sea.Children.Add(textGrid("В", 0, 3));
            sea.Children.Add(textGrid("Г", 0, 4));
            sea.Children.Add(textGrid("Д", 0, 5));
            sea.Children.Add(textGrid("Е", 0, 6));
            sea.Children.Add(textGrid("Ж", 0, 7));
            sea.Children.Add(textGrid("З", 0, 8));
            sea.Children.Add(textGrid("И", 0, 9));
            sea.Children.Add(textGrid("К", 0, 10));

            sea.Children.Add(textGrid("1", 1, 0));
            sea.Children.Add(textGrid("2", 2, 0));
            sea.Children.Add(textGrid("3", 3, 0));
            sea.Children.Add(textGrid("4", 4, 0));
            sea.Children.Add(textGrid("5", 5, 0));
            sea.Children.Add(textGrid("6", 6, 0));
            sea.Children.Add(textGrid("7", 7, 0));
            sea.Children.Add(textGrid("8", 8, 0));
            sea.Children.Add(textGrid("9", 9, 0));
            sea.Children.Add(textGrid("10", 10, 0));
        }

        private void setBattleMap(Grid sea)
        {
            for (int i = 0; i < 10; i++)
            {
                map[i] = new Grid[10];
            }
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    map[i][j] = new Grid();
                    map[i][j].Background = Brushes.Azure;
                    map[i][j].ShowGridLines = true;
                    Border b = new Border();
                    b.BorderBrush = Brushes.Black;
                    b.BorderThickness = new Thickness(0.5);
                    map[i][j].Children.Add(b);
                    Grid.SetRow(map[i][j], i + 1);
                    Grid.SetColumn(map[i][j], j + 1);
                    sea.Children.Add(map[i][j]);                   
                }
        }
        /// <summary>
        /// Создает текст блок с присвоением строки и столбца, для добавления в Grid
        /// </summary>
        /// <param name="t">Текст</param>
        /// <param name="i">Строка</param>
        /// <param name="j">Колонка</param>
        /// <returns>Текстовый блок (TextBlock)</returns>
        private TextBlock textGrid(string t, int i, int j)
        {
            TextBlock text = new TextBlock();
            text.TextAlignment = TextAlignment.Center;
            text.Text = t;
            Grid.SetRow(text, i);
            Grid.SetColumn(text, j);
            return text;
        }

        private void labelBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Switcher.SwitchPage(new MainMenu());
        }
        
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
