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

namespace SeaBattle.GameLogic
{
    class Sea
    {
        public Grid SeaGrid { get; set; }

        public Field[][] Map = new Field[10][];

        public Sea (Grid bigSquare)
        {
            SeaGrid = bigSquare;
            setBattleMap();
            setMapNames();
        }
        private void setMapNames()
        {
            SeaGrid.Children.Add(textGrid("А", 0, 1));
            SeaGrid.Children.Add(textGrid("Б", 0, 2));
            SeaGrid.Children.Add(textGrid("В", 0, 3));
            SeaGrid.Children.Add(textGrid("Г", 0, 4));
            SeaGrid.Children.Add(textGrid("Д", 0, 5));
            SeaGrid.Children.Add(textGrid("Е", 0, 6));
            SeaGrid.Children.Add(textGrid("Ж", 0, 7));
            SeaGrid.Children.Add(textGrid("З", 0, 8));
            SeaGrid.Children.Add(textGrid("И", 0, 9));
            SeaGrid.Children.Add(textGrid("К", 0, 10));

            SeaGrid.Children.Add(textGrid("1", 1, 0));
            SeaGrid.Children.Add(textGrid("2", 2, 0));
            SeaGrid.Children.Add(textGrid("3", 3, 0));
            SeaGrid.Children.Add(textGrid("4", 4, 0));
            SeaGrid.Children.Add(textGrid("5", 5, 0));
            SeaGrid.Children.Add(textGrid("6", 6, 0));
            SeaGrid.Children.Add(textGrid("7", 7, 0));
            SeaGrid.Children.Add(textGrid("8", 8, 0));
            SeaGrid.Children.Add(textGrid("9", 9, 0));
            SeaGrid.Children.Add(textGrid("10", 10, 0));
        }

        private void setBattleMap()
        {
            for (int i = 0; i < 10; i++)
            {
                Map[i] = new Field[10];
            }
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Map[i][j] = new Field();

                    Grid.SetRow(Map[i][j].Square, i + 1);
                    Grid.SetColumn(Map[i][j].Square, j + 1);
                    SeaGrid.Children.Add(Map[i][j].Square);
                    Map[i][j].Square.MouseMove += Map[i][j].Game_MouseMove;
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
    }
}
