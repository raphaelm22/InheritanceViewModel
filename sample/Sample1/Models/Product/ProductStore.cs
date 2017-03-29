using System.Linq;
using Sample1.Models.Common;
using System.Collections.Generic;

namespace Sample1.Models.Product
{
    public class ProductStore
    {
        readonly IEnumerable<ProductModel> All = new List<ProductModel>
        {
            {new ProductModel {Id= 1, Name = "Sony Playstation 4", Cost=300, CostType = ECostType.Dollar, BackgroundColor = "#68217A" } },
            {new ProductModel {Id= 2, Name = "Microsoft XBOX One", Cost=800, CostType = ECostType.Golds, BackgroundColor = "#7FBA00" } }
        };

        public IEnumerable<ProductModel> List() => All;

        public ProductModel FindById(int id)
        {
            return All.FirstOrDefault(p => p.Id == id);
        }
    }
}
