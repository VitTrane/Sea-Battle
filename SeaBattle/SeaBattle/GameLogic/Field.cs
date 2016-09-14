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
        Sea
    }

    public class Field : Grid
    {
        public int Row { get; private set; }
        
        public int Column { get; private set; }

        public FieldState State { get;  set; }

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
