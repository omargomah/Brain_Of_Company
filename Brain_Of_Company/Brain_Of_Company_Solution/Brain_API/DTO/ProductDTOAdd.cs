namespace Brain_API.DTO
{
    public class ProductDTOAdd
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int RealQuantities { get; set; }
    }
}
