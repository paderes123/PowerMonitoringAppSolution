using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PowerMonitoringApp.Converters
{
    public class EmailFormatValidator : IValueConverter
    {
        private static readonly Regex _emailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string email && !string.IsNullOrWhiteSpace(email))
            {
                return _emailRegex.IsMatch(email);
            }

            return false;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Returning Binding.DoNothing is safer when ConvertBack isn't used
            return Binding.DoNothing;
        }
    }
}
