using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Shopfloor.Models.Persons;

namespace Shopfloor.Converters
{
    internal class ComparisonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isConfirmedByCoach = (bool)values[0];
            bool isConfirmedByTrainee = (bool)values[1];
            Person coach = (Person)values[2];
            Person trainee = (Person)values[3];

            if (values[4] is not Person)
            {
                return Visibility.Collapsed;
            }

            Person? currentPerson = (Person?)values[4];

            if (!isConfirmedByCoach && currentPerson == coach)
            {
                return Visibility.Visible;
            }

            if (!isConfirmedByTrainee && currentPerson == trainee)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}