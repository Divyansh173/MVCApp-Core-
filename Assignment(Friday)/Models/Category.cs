using System;
using System.Collections.Generic;

namespace Assignment_Friday_.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public decimal BasePrice { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
