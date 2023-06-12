using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Car4You.MVVM.View
{
    public class IndexConverter : IMultiValueConverter
    {
        public int Count { get; set; }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int currentIndex = (int)values[0];
            Count = (int)values[1];

            string action = parameter.ToString();

            if (action == "Increase")
            {
                currentIndex = (currentIndex + 1) % Count;
            }
            else if (action == "Decrease")
            {
                currentIndex = (currentIndex - 1 + Count) % Count;
            }

            return currentIndex;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
