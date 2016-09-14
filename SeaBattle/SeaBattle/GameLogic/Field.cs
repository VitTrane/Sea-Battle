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
    public enum FieldState
    {
        Ship,
        Miss,
        Hit,
        Sea
    }

    public class Field : Grid
    {
        private FieldState _fieldState;        

        public int Row { get; private set; }
        
        public int Column { get; private set; }        

        public Ship Ship { get; set; }

        public FieldState State
        {
            get { return _fieldState; }
            set 
            {
                if (value == FieldState.Ship)
                    Background = Brushes.Green;                    

                if (value == FieldState.Sea)
                    Background = Brushes.Azure;

                if (value == FieldState.Miss)
                    Background = Brushes.Gray;

                if (value == FieldState.Hit)
                    Background = Brushes.Red;

                _fieldState = value;
            }
        }

        public Field(int row, int column)
        {
            State = FieldState.Sea;
            Row = row;
            Column = column;
            Background = Brushes.Azure;
            ShowGridLines = true;
            Border b = new Border();
            b.BorderBrush = Brushes.Black;
            b.BorderThickness = new Thickness(0.5);
            Children.Add(b);
        }       
    }    
}
