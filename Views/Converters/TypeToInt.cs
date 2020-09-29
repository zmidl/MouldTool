using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MouldTool.Views.Converters
{
   public class TypeToInt : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return (int)((Models.Type)value);
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         switch (System.Convert.ToInt32(value))
         {
            //case 0:return Models.Type.Rectangle;
            case 1: return Models.Type.Ellipse;
            case 2: return Models.Type.Triangle;
            default: return Models.Type.Rectangle;
         }
      }
   }
}
