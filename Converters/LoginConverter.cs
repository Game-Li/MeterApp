using MeterApp.Models;
using System.Globalization;

namespace MeterApp.Converters;

public class UserInfoToStringArrayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is UserInfo userInfo))
        {
            return null;
        }

        // Convert UserInfo to string[] array
        return new string[] { userInfo.Username, userInfo.Password };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is string[] stringArray))
        {
            return null;
        }

        // Convert string[] array to UserInfo
        return new UserInfo
        {
            Username = stringArray[0],
            Password = stringArray[1]
        };
    }
}


