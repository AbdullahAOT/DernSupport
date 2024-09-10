namespace DernSupport2.Models.DTOs
{
    public class SparePartDTO
    {
        public int SparePartId { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public decimal Cost { get; set; }
        public int QuantityInStock { get; set; }
    }
}
