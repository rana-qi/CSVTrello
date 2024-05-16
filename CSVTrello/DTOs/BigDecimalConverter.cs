using CsvHelper.Configuration;
using CsvHelper;
using ExtendedNumerics;
using CsvHelper.TypeConversion;

namespace CSVTrello.DTOs
{
    public class BigDecimalConverter: DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return BigDecimal.Parse(text);
        }
    }
}
