using Microsoft.AspNetCore.Mvc;
using Coditas.ECommerce.Entities;
using Coditas.ECommerce.Repositories;

namespace MVCAPPS.Controllers
{
    public class DetailsController : Controller
    {
        IDbAccessService<Product, int> productrepo;
        IDbAccessService<SubCategory, int> subcaterepo;
        IDbAccessService<Category, int> categoryrepo;
        public DetailsController(IDbAccessService<Product, int> productrepo, IDbAccessService<SubCategory, int> subcaterepo, IDbAccessService<Category, int> categoryrepo)
        {
            this.productrepo = productrepo;
            this.subcaterepo = subcaterepo;
            this.categoryrepo = categoryrepo;
        }

        public async Task<IActionResult> Index(int? id)
        {
            List<SubCategory> subcategories = null;
            List<Product> products = null;
            List<Category> categories = null;
            Tuple<List<Category>, List<Product>> tuple = null;

            categories = (await categoryrepo.GetAsync()).ToList();

            if (id == null || id == 0)
            {
                products = (await productrepo.GetAsync()).ToList();
            }
            else 
            {
                var product = (await productrepo.GetAsync()).ToList();
                var category = (await categoryrepo.GetAsync()).ToList();
                var subcategory = (await subcaterepo.GetAsync()).ToList();
                products = (from pro in product
                           join subcat in subcategory on pro.SubCategoryId equals subcat.SubCategoryId
                           where subcat.CategoryId == id
                           select pro).ToList();
            }

            tuple = new Tuple<List<Category>, List<Product>>(categories, products);
            return View(tuple);
        }

        public IActionResult ShowDetails(int? id) 
        {
            return RedirectToAction("Index", new { id = id});
        }
    }
}
