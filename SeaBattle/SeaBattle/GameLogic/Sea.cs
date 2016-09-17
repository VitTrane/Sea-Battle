using SeaBattle.GameService;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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
        public List<DTOShip> Ships { get; private set; }

        /// <summary>
        /// Инициализация игрового поля
        /// </summary>
        /// <param name="bigSquare">Контрол игрового поля</param>
        public Sea(Grid bigSquare)
        {
            Map = new Field[MAP_WIDTH, MAP_HEIGHT];
            Ships = new List<DTOShip>();
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
            DTOShip ship = new DTOShip();
            ship.DeckCount = countDeck;
            ship.Orientation = orientation;
            ship.Coordinates = new XYCoordinate() { X = x, Y = y };
            Ships.Add(ship);
            int startPositionX = x;
            int startPositionY = y;
            for (int i = 0; i < countDeck; i++)
            {
                if (orientation == ShipOrientation.Horisontal)
                {
                    var field = Map[startPositionY, startPositionX];
                    field.State = FieldState.Ship;
                    field.Ship = ship;
                    startPositionX++;
                }
                else 
                {
                    var field = Map[startPositionY, startPositionX];
                    field.State = FieldState.Ship;
                    field.Ship = ship;
                    startPositionY++;
                }
            }
        }

        /// <summary>
        /// Удаляет корабль с поля
        /// </summary>
        /// <param name="ship">Корабль, который нужно удалить</param>
        public void DeleteShip(DTOShip ship)
        {
            int startPositionX = ship.Coordinates.X;
            int startPositionY = ship.Coordinates.Y;

            for (int i = 0; i < ship.DeckCount; i++)
            {
                if (ship.Orientation == ShipOrientation.Horisontal)
                {
                    var field = Map[startPositionY, startPositionX];
                    field.State = FieldState.Sea;
                    field.Ship = null;
                    startPositionX++;
                }
                else
                {
                    var field = Map[startPositionY, startPositionX];
                    field.State = FieldState.Sea;
                    field.Ship = null;
                    startPositionY++;
                }
            }

            Ships.Remove(ship);
        }

        /// <summary>
        /// Получает выстрел сделанный по карте
        /// </summary>
        /// <param name="shot">Выстрел, который получили</param>
        public void SetShot(Shot shot, DTOShip killedShip = null) 
        {
            var field = Map[shot.XyCoordinate.Y, shot.XyCoordinate.X];
            if (shot.Status == ShotStatus.Missed)
            {
                field.State = FieldState.Miss;
            }

            if (shot.Status == ShotStatus.Hit)
            {
                field.State = FieldState.Hit;
                SetAroundShotArea(shot.XyCoordinate.X, shot.XyCoordinate.Y, shot.Status);
            }

            if (shot.Status == ShotStatus.Killed)
            {
                field.State = FieldState.Hit;
                SetAroundShotArea(shot.XyCoordinate.X, shot.XyCoordinate.Y, shot.Status, killedShip);
            }
        }

        /// <summary>
        /// Раставляет промохи вокруг попадания
        /// </summary>
        /// <param name="x">Колонка</param>
        /// <param name="y">Строка</param>
        /// <param name="status">Статус выстрела</param>
        private void SetAroundShotArea(int x, int y, ShotStatus status, DTOShip killedShip = null) 
        {
            List<XYCoordinate> diagonalPoints = new List<XYCoordinate>();
            diagonalPoints.Add(new XYCoordinate() { X = x + 1, Y = y + 1 });
            diagonalPoints.Add(new XYCoordinate() { X = x - 1, Y = y - 1 });
            diagonalPoints.Add(new XYCoordinate() { X = x - 1 , Y = y + 1 });
            diagonalPoints.Add(new XYCoordinate() { X = x + 1, Y = y - 1 });

            //Растановка промахов по диагональным клеткам от выстрела
            foreach (var point in diagonalPoints)
            {
                if (point.X >= 0 && point.X < MAP_WIDTH && point.Y >= 0 && point.Y < MAP_HEIGHT) 
                {
                    Map[point.Y, point.X].State = FieldState.Miss;
                }
            }

            //Растановка промахов по по горизонтали и вертикали вокруг убитого корабля
            if (killedShip != null)
            {
                int deckCount = killedShip.DeckCount;
                List<XYCoordinate> horizontalPoints = new List<XYCoordinate>();
                List<XYCoordinate> verticalPoints = new List<XYCoordinate>();
                verticalPoints.Add(new XYCoordinate() { X = killedShip.Coordinates.X, Y = killedShip.Coordinates.Y + deckCount });
                verticalPoints.Add(new XYCoordinate() { X = killedShip.Coordinates.X, Y = killedShip.Coordinates.Y - 1 });
                horizontalPoints.Add(new XYCoordinate() { X = killedShip.Coordinates.X - 1, Y = killedShip.Coordinates.Y });
                horizontalPoints.Add(new XYCoordinate() { X = killedShip.Coordinates.X + deckCount, Y = killedShip.Coordinates.Y });

                if (status == ShotStatus.Killed)
                {
                    //Если у корабля больше чем 1 палуба, то закрашиваем либо по вертикали либо по горизонтали
                    if (killedShip.DeckCount > 1)
                    {
                        if (killedShip.Orientation == ShipOrientation.Horisontal)
                        {
                            foreach (var point in horizontalPoints)
                            {
                                if (point.X >= 0 && point.X < MAP_WIDTH)
                                {
                                    Map[point.Y, point.X].State = FieldState.Miss;
                                }
                            }
                        }
                        else
                        {
                            foreach (var point in verticalPoints)
                            {
                                if (point.Y >= 0 && point.Y < MAP_HEIGHT)
                                {
                                    Map[point.Y, point.X].State = FieldState.Miss;
                                }
                            }
                        }
                    }
                }
                else // Если у коробля 1 палуба, то закрашиваем и по вертикали и по горизонтали
                {
                    List<XYCoordinate> resultPoints = new List<XYCoordinate>();
                    //Находим точки которые подходят по горизонтали
                    foreach (var point in horizontalPoints)
                    {
                        if (point.X >= 0 && point.X < MAP_WIDTH)
                        {
                            resultPoints.Add(point);
                        }
                    }
                    //из найденых точек выбераем только те, которые подходят по вертикали и закрашиваем поля
                    foreach (var point in verticalPoints)
                    {
                        if (point.Y >= 0 && point.Y < MAP_HEIGHT)
                        {
                            Map[point.Y, point.X].State = FieldState.Miss;
                        }
                    }
                }

            }
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
                if (j + deckCount - 1 > MAP_WIDTH)
                    return false;
            }
            else
            {
                if (i + deckCount - 1 > MAP_HEIGHT)
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
