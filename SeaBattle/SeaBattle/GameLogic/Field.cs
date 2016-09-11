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
    class Field
    {
        int Row { get; set; }

        int Column { get; set; }

        Grid _square = new Grid();

        public Field()
        {
            _square.Background = Brushes.Azure;
            _square.ShowGridLines = true;
            Border b = new Border();
            b.BorderBrush = Brushes.Black;
            b.BorderThickness = new Thickness(0.5);
            _square.Children.Add(b);
        }
        
        public Grid Square
        {
            get { return _square; }
            set
            {
                _square = value;
            }
        }        
    }
}
