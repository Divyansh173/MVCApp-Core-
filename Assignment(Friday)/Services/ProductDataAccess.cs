using Assignment_Friday_.Models;
using Microsoft.EntityFrameworkCore;
namespace Assignment_Friday_.Services
{
    public class ProductDataAccess
    {
        eShoppingCodiContext context;

        public ProductDataAccess(eShoppingCodiContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Product>> GetAsync() 
        {
            var result = await context.Products.ToListAsync();
            return result;
        }
    }
}
