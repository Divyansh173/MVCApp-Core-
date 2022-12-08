using Microsoft.AspNetCore.Mvc;
using Coditas.ECommerce.Entities;
using Coditas.ECommerce.DataAccess;
using Coditas.ECommerce.Repositories;
using MVCAPPS.CustomSessionExtension;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCAPPS.Controllers
{
    public class ProductController : Controller
    {
        IDbAccessService<Product, int> ProductRepo;
        IDbAccessService<SubCategory, int> SubcatRepo;

        public ProductController(IDbAccessService<Product, int> ProductRepo, IDbAccessService<SubCategory, int> subcatRepo)
        {
            this.ProductRepo = ProductRepo;
            SubcatRepo = subcatRepo;
        }

        public async Task<IActionResult> Index()
        {

            int SubCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));

            var Subcat = HttpContext.Session.GetObject<Category>("SubCat");

            if (SubCategoryId == 0)
            {
                var records = await ProductRepo.GetAsync();
                return View(records);
            }
            else
            {
                var records = (await ProductRepo.GetAsync()).Where(p => p.SubCategoryId == SubCategoryId).ToList();
                return View(records);
            }

        }

        public async Task<IActionResult> Create()
        {
            var Product = new Product();
            if (HttpContext.Session.GetObject<Product>("pro") != null) 
            {
                Product = HttpContext.Session.GetObject<Product>("pro");
                ViewBag.ErrorMessage = "Price can not be less than 0";
            }
            HttpContext.Session.Clear();
            return View(Product);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {

            if (product.Price <= 0)
            {
                HttpContext.Session.SetObject<Product>("pro", product);
                //HttpContext.Session.SetString("ErrorMessage","Price can not be less than 0");
                throw new Exception("Price can not be less than 0");
            }
            else
            {
                var response = await ProductRepo.CreateAsync(product);
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
                var record = await ProductRepo.GetAsync(id);
                return View(record);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            var result = await ProductRepo.UpdateAsync(id, product);
            return RedirectToAction("Index");
   
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await ProductRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
