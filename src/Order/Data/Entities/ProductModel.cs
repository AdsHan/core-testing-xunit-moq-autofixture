using Order.Data.DomainObjects;
using Order.Data.Enum;

namespace Order.Data.Entities
{
    public class ProductModel : BaseEntity, IAggregateRoot
    {
        public ProductModel(string barCode, string description, decimal price, ProductGroupType productGroup)
        {
            BarCode = barCode;
            Description = description;
            Price = price;
            ProductGroup = productGroup;
        }

        public string BarCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductGroupType ProductGroup { get; set; }
    }
}