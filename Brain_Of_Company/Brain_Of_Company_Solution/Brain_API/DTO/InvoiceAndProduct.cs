namespace Brain_API.DTO
{
    public class InvoiceAndProduct : InvoiceDTO
    {
        public int Id {  get; set; }
        public string ProductName { get; set; }
        public DateTime DateOfSale { get; set; } // Date of Sale (primary key)

    }
}
