namespace DernSupport2.Models
{
    public class SparePart
    {
        public int SparePartId { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public decimal Cost { get; set; }
        public int QuantityInStock { get; set; }
    }
}
