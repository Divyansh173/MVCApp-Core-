using System;
using System.Collections.Generic;

namespace Coditas.ECommerce.Entities
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Products = new HashSet<Product>();
        }

        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; } = null;
        public virtual ICollection<Product> Products { get; set; }
    }
}
