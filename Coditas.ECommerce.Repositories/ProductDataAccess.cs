using Coditas.ECommerce.Entities;
using Coditas.ECommerce.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
namespace Coditas.ECommerce.Repositories
{
    public class ProductDataAccess : IDbAccessService<Product, int>
    {
        eShoppingCodiContext context;

        public ProductDataAccess(eShoppingCodiContext context)
        {
            this.context = context;
        }
        public async Task<Product> CreateAsync(Product entity)
        {
            //try
            //{
            if ((Convert.ToInt32(entity.ProductId) < 0)) {
                throw new Exception("ProductId can not be less than 0");
            }
            else {
                var res = await context.Products.AddAsync(entity);
                await context.SaveChangesAsync();
                return res.Entity;
            }
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await context.Products.FindAsync(id);
            if (record == null)
            {
                throw new Exception("Record to delete is not found");
            }
            context.Products.Remove(record);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            var category = await context.Products.ToListAsync();
            if (category == null) throw new Exception("Record not found");
            return category;

        }

        public async Task<Product> GetAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) throw new Exception("Record not found");
            return product;
        }

        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Record to be updated is not found");
            }
            product.ProductUniqueId = entity.ProductUniqueId;
            product.ProductId = entity.ProductId;
            product.ProductName = entity.ProductName;
            product.ManufacturerId = entity.ManufacturerId;
            product.Description = entity.Description;
            product.Price = entity.Price;
            product.SubCategoryId = entity.SubCategoryId;
            await context.SaveChangesAsync();

            return product;

        }

    }
}
