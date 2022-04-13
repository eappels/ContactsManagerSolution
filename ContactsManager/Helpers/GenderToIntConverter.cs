using ContactsManager.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ContactsManager.Helpers
{
    public class GenderToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GenderType genderValue = (GenderType)value;
            int intValue = 0;
            switch (genderValue)
            {
                case GenderType.Male:
                    intValue = 0;
                    break;
                case GenderType.Female:
                    intValue = 1;
                    break;
            }
            return intValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            GenderType genderValue = GenderType.Male;
            switch (intValue)
            {
                case 0:
                    genderValue = GenderType.Male;
                    break;
                case 1:
                    genderValue = GenderType.Female;
                    break;
            }
            return genderValue;
        }
    }
}