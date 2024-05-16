using ExtendedNumerics;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CSVTrello.DTOs
{
    public class BigDecimalJsonConverter : JsonConverter<BigDecimal>
    {
        public override BigDecimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return BigDecimal.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, BigDecimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
