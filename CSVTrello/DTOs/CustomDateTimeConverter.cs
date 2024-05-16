using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CsvHelper.TypeConversion;

namespace CSVTrello.DTOs
{
    public class CustomDateTimeConverter : DateTimeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return DateTime.MinValue; // return a default DateTime value
            }

            var formatProvider = CultureInfo.InvariantCulture;
            var dateTimeStyles = DateTimeStyles.None;
            var formats = new[] { "yyyy-mm-dd" }; // replace with your date format

            if (DateTime.TryParseExact(text, formats, formatProvider, dateTimeStyles, out var date))
            {
                return date;
            }

            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
