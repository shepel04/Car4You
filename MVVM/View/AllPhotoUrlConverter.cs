using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Car4You.MVVM.View
{
    public class AllPhotoUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string photoUrlsString = value as string;
            if (!string.IsNullOrEmpty(photoUrlsString))
            {
                string[] photo = photoUrlsString.Trim().Split(',');
                if (photo.Length > 0)
                {
                    return photo[0];
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
