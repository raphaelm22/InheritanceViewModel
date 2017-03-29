using Sample1.Models.Common;

namespace Sample1.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public ECostType CostType { get; set; }
        public string BackgroundColor { get; set; }
    }
}
