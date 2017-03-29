using Sample1.Models.Common;
using Sample1.Models.Product;

namespace Sample1.ViewModels.Order
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public ECostType CostType { get; set; }

        public ProductViewModel() { }
        public ProductViewModel(ProductModel productModel)
        {
            Id = productModel.Id;
            Name = productModel.Name;
            Cost = productModel.Cost;
            CostType = productModel.CostType;
        }
    }
}
