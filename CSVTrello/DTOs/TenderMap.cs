using CsvHelper.Configuration;
using CSVTrello.Domain.Models;

namespace CSVTrello.DTOs
{
    public class TenderMap: ClassMap<Tender>
    {
        public TenderMap()
        {
            Map(m => m.Deadline).TypeConverter<CustomDateTimeConverter>();
            Map(m => m.ExpirationDate).TypeConverter<CustomDateTimeConverter>();
            Map(m => m.PublicationDate).TypeConverter<CustomDateTimeConverter>();
            Map(m => m.Value).TypeConverter<BigDecimalConverter>().Name("Value");
            Map(m => m.Id).Name("Id");
            Map(m => m.Currency).Name("Currency"); // replace "Currency" with the actual column name in the CSV file
            Map(m => m.Location).Name("Location"); // replace "Location" with the actual column name in the CSV file
            Map(m => m.LotNumber).Name("LotNumber"); // replace "LotNumber" with the actual column name in the CSV file
            Map(m => m.Name).Name("Name"); // replace "Name" with the actual column name in the CSV file
            Map(m => m.TenderId).Name("TenderId"); // replace "TenderId" with the actual column name in the CSV file
            Map(m => m.TenderName).Name("TenderName"); // replace "TenderName" with the actual column name in the CSV file
            Map(m => m.HasDocuments).Name("HasDocuments"); // replace "HasDocuments" with the actual column name in the CSV file
            Map(m => m.Status).Name("Status"); // replace "Status" with the actual column name in the CSV file
            


            // Map other properties...
        }
    }
}
