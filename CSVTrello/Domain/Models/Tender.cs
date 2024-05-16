using static ExtendedNumerics.BigDecimal;
using System.Numerics;
using ExtendedNumerics;

namespace CSVTrello.Domain.Models
{
    public class Tender
    {
        public string Id { get; set; }
        public string TenderId { get; set; }
        public string LotNumber { get; set; }
        public DateTime Deadline { get; set; }
        public string Name { get; set; }
        public string TenderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool? HasDocuments { get; set; }
        public string Location { get; set; }
        public DateTime PublicationDate { get; set; }
        public int? Status { get; set; }
        public string Currency { get; set; }
        public BigDecimal? Value { get; set; }  
    }
}
