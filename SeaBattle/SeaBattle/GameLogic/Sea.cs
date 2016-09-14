using SeaBattle.GameService;
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
        public const int MAP_WIDTH = 10;
        public const int MAP_HEIGHT = 10;

        /// <summary>
        /// Контейнер игрового поля
        /// </summary>
        public Grid SeaGrid { get; set; }
        
        /// <summary>
        /// Двумерный массив ячеек поля
        /// </summary>
        public Field[,] Map { get; set; }

        /// <summary>
        /// Список кораблей
        /// </summary>
        public List<Ship> Ships { get; private set; }

        /// <summary>
        /// Инициализация игрового поля
        /// </summary>
        /// <param name="bigSquare">Контрол игрового поля</param>
        public Sea(Grid bigSquare)
        {
            Map = new Field[MAP_WIDTH, MAP_HEIGHT];
            Ships = new List<Ship>();
            SeaGrid = bigSquare;
            setBattleMap();
            setMapNames();
        }

        /// <summary>
        /// Добавляет корабль на карту
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="countDeck">Количество палуб</param>
        /// <param name="orientation">Ориентация корабля</param>
        public void CreateShip(int x, int y, int countDeck, ShipOrientation orientation) 
        {
            Ship ship = new Ship();
            ship.Orientation = orientation;
            ship.StartPoint = new XYCoordinate() { X = x, Y = y };
            ship.Decks = new Deck[countDeck];
            for (int i = 0; i < countDeck; i++)
            {
                ship.Decks[i] = new Deck();
            }
            Ships.Add(ship);
            int startPositionX = x;
            int startPositionY = y;
            for (int i = 0; i < countDeck; i++)
            {
                if (orientation == ShipOrientation.Horisontal)
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var field = Map[startPositionY, startPositionX]; 
                    field.Background = (Brush)converter.ConvertFromString("#b5e61d");
                    field.State = FieldState.Ship;
                    field.Ship = ship;
                    startPositionX++;
                }
                else 
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var field = Map[startPositionY, startPositionX];
                    field.Background = (Brush)converter.ConvertFromString("#b5e61d");
                    field.State = FieldState.Ship;
                    field.Ship = ship;
                    startPositionY++;
                }
            }
        }

        public void DeleteShip(Ship ship)
        {
            int startPositionX = ship.StartPoint.X;
            int startPositionY = ship.StartPoint.Y;

            for (int i = 0; i < ship.Decks.Length; i++)
            {
                if (ship.Orientation == ShipOrientation.Horisontal)
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var field = Map[startPositionY, startPositionX];
                    field.Background = Brushes.Azure;
                    field.State = FieldState.Sea;
                    field.Ship = null;
                    startPositionX++;
                }
                else
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var field = Map[startPositionY, startPositionX];
                    field.Background = Brushes.Azure;
                    field.State = FieldState.Sea;
                    field.Ship = null;
                    startPositionY++;
                }
            }

            Ships.Remove(ship);
        }
        /// <summary>
        /// Создает на поле клетки с именами столбцов и строк
        /// </summary>
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

        /// <summary>
        /// Создает клетки для постановки кораблей
        /// </summary>
        private void setBattleMap()
        {
            for (int i = 0; i < MAP_WIDTH; i++)
            {
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    Map[i, j] = new Field(i, j);
                    Grid.SetRow(Map[i, j], i + 1);
                    Grid.SetColumn(Map[i, j], j + 1);
                    SeaGrid.Children.Add(Map[i, j]);
                }
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
            TextBlock textBlock = new TextBlock();
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Text = t;
            Grid.SetRow(textBlock, i);
            Grid.SetColumn(textBlock, j);
            return textBlock;
        }

        /// <summary>
        /// Провеярем можно ли здесь  установить корабль
        /// </summary>
        /// <param name="deckCount">Количество палуб</param>
        /// <param name="i">Координаты по строке</param>
        /// <param name="j">Координаты по столбцу</param>
        /// <param name="orientation">Ориентация корабля</param>
        /// <returns></returns>
        public bool CanShipStayThere(byte deckCount, int i, int j, ShipOrientation orientation)
        {
            if (i < 1 || j < 1)
                return false;

            if (orientation == ShipOrientation.Horisontal)
            {
                if (j + deckCount - 1 > 10) // можно ли приватные константы из класса Sea сделать публичными и проверять на них?
                    return false;
            }
            else
            {
                if (i + deckCount - 1 > 10) // аналогичный вопрос
                    return false;
            }

            if (orientation == ShipOrientation.Horisontal)
            {
                //Начало проверки правой части корабля
                if (j + deckCount < 11)
                {
                    //Проверяем правый край корабля
                    if (IsShipThere(i, j + deckCount))
                        return false;
                    //Проверяем правый верхний угол корабля
                    if (i - 1 > 0)
                        if (IsShipThere(i - 1, j + deckCount))
                            return false;
                    //Проверяем правый нижний угол корабля
                    if (i + 1 < 11)
                        if (IsShipThere(i + 1, j + deckCount))
                            return false;
                }
                //Начало проверки левой части корабля
                if (j - 1 > 0)
                {
                    //Проверяем левую сторону корабля
                    if (IsShipThere(i, j - 1))
                        return false;
                    //Проверяем левый верхний угол корабля
                    if (i - 1 > 0)
                    {
                        if (IsShipThere(i - 1, j - 1))
                            return false;
                    }
                    //Проверяем левый нижний угол корабля
                    if (i + 1 < 11)
                    {
                        if (IsShipThere(i + 1, j - 1))
                            return false;
                    }
                }

                //Проверяем верхнюю грань корабля
                if (i - 1 > 0)
                {
                    for (int k = j; k < j + deckCount; k++)
                    {
                        if (IsShipThere(i - 1, k))
                            return false;
                    }
                }
                //Проверяем нижнюю грань корабля
                if (i + 1 < 11)
                {
                    for (int k = j; k < j + deckCount; k++)
                    {
                        if (IsShipThere(i + 1, k))
                            return false;
                    }
                }

                //Проверяем не ложится ли корабль поверх другого корабля
                for (int k = j; k < j + deckCount; k++)
                {
                    if (IsShipThere(i, k))
                        return false;
                }
            }
            else
            {
                //Проверяем верхнюю часть корабля
                if (i - 1 > 0)
                {
                    //Проверяем верхний правый угол
                    if (j - 1 > 0)
                    {
                        if (IsShipThere(i - 1, j - 1))
                            return false;
                    }
                    //Проверяем верх корабля
                    if (IsShipThere(i - 1, j))
                        return false;
                    //Проверяем верхний левый угол
                    if (j + 1 < 11)
                    {
                        if (IsShipThere(i - 1, j + 1))
                            return false;
                    }
                }

                //Проверяем нижнюю часть корабля
                if (i + deckCount < 11)
                {
                    //Прверяем нижний левый угол
                    if (j - 1 > 0)
                    {
                        if (IsShipThere(i + deckCount, j - 1))
                            return false;
                    }
                    //Проверяем низ корабля
                    if (IsShipThere(i + deckCount, j))
                        return false;
                    //Проверяем правый нижний угол
                    if (j + 1 < 11)
                    {
                        if (IsShipThere(i + deckCount, j + 1))
                            return false;
                    }
                }

                //Проверяем левую грань
                if (j - 1 > 0)
                {
                    for (int k = i; k < i + deckCount; k++)
                    {
                        if (IsShipThere(k, j - 1))
                            return false;
                    }
                }

                //Проверяем не ложится ли корабль поверх других кораблей
                for (int k = i; k < i + deckCount; k++)
                {
                    if (IsShipThere(k, j))
                        return false;
                }

                //Проверяем правую грань корабля
                if (j + 1 < 11)
                {
                    for (int k = i; k < i + deckCount; k++)
                    {
                        if (IsShipThere(k, j + 1))
                            return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Проверяет наличие корабля в клетке
        /// </summary>
        /// <param name="j">Координата колонки (от 1 до 10)</param>
        /// <param name="i">Координата строки (от 1 до 10)</param>
        /// <returns></returns>
        private bool IsShipThere(int i, int j)
        {
            if (Map[i - 1, j - 1].State == FieldState.Ship)
                return true;
            else
                return false;
        }
    }
}
