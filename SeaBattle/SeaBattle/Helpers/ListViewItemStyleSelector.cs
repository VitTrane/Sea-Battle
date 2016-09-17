using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SeaBattle.Helpers
{
    public class ListViewItemStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            Style st = new Style();
            st.TargetType = typeof(ListViewItem);
            Setter backGroundSetter = new Setter();
            backGroundSetter.Property = ListViewItem.BackgroundProperty;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(container) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(container);

            if (index % 2 == 0)
            {
                backGroundSetter.Value = (SolidColorBrush)new BrushConverter().ConvertFromString("#eeeeee");
            }
            else
            {
                backGroundSetter.Value = Brushes.White;
            }
            st.Setters.Add(backGroundSetter);
            return st;
        }
    }
}
