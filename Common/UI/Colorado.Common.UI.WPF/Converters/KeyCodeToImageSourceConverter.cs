using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;

namespace Colorado.Common.UI.WPF.Converters
{
    [ValueConversion(typeof(Keys), typeof(ImageSource))]
    public class KeyCodeToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
