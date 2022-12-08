using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coditas.ECommerce.Entities;
using Coditas.ECommerce.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Coditas.ECommerce.Repositories
{
    public  class SubcategoryDataAccess : IDbAccessService<SubCategory, int>
    {
        eShoppingCodiContext context;

        public SubcategoryDataAccess(eShoppingCodiContext context)
        {
            this.context = context;
        }
        public async Task<SubCategory> CreateAsync(SubCategory entity)
        {
            try
            {
                var res = await context.SubCategories.AddAsync(entity);
                await context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await context.SubCategories.FindAsync(id);
            if (record == null)
            {
                throw new Exception("Record to delete is not found");
            }
            context.SubCategories.Remove(record);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<SubCategory>> GetAsync()
        {
            var category = await context.SubCategories.ToListAsync();
            if (category == null) throw new Exception("Record not found");
            return category;

        }

        public async Task<SubCategory> GetAsync(int id)
        {
            var product = await context.SubCategories.FindAsync(id);
            if (product == null) throw new Exception("Record not found");
            return product;
        }

        public async Task<SubCategory> UpdateAsync(int id, SubCategory entity)
        {
            var product = await context.SubCategories.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Record to be updated is not found");
            }
           
            product.SubCategoryId = entity.SubCategoryId;
            product.SubCategoryName = entity.SubCategoryName;
            product.CategoryId = entity.CategoryId;
            await context.SaveChangesAsync();

            return product;

        }

    }
}
