using SenseHat.Portable.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HelloWorld
{
    public class HumidityToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = Constants.UnavailableValue;

            try
            {
                float humidity = System.Convert.ToSingle(value);

                result = string.Format("{0,4:F2} %", humidity);
            }
            catch (Exception) { }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
