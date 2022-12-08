using Microsoft.AspNetCore.Mvc;
using Assignment_Friday_.Models;
using Assignment_Friday_.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace Assignment_Friday_.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        ProductDataAccess prodata;
        eShoppingCodiContext context;

        public ProductController(ProductDataAccess prodata, eShoppingCodiContext context)
        {
            this.prodata = prodata;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> getproducts(string name) 
        {
            if (name == null)
            {
                var result1 = from pro in context.Products
                              join subcat in context.SubCategories on pro.SubCategoryId equals subcat.SubCategoryId
                              join cat in context.Categories on subcat.CategoryId equals cat.CategoryId
                              join manu in context.Manufacturers on pro.ManufacturerId equals manu.ManufacturerId
                              select new
                              {
                                  ProductId = pro.ProductId,
                                  CategoryName = cat.CategoryName,
                                  ProductName = pro.ProductName,
                                  Description = pro.Description,
                                  ManufacturerName = manu.ManufacturerName,
                                  Price = pro.Price,
                                  Seller = pro.Seller,
                              };
                List<Result> result = new List<Result>();
                foreach (var v in result1)
                {
                    result.Add(new Result()
                    {
                        ProductId = v.ProductId,
                        ProductName = v.ProductName,
                        CategoryName = v.CategoryName,
                        Description = v.Description,
                        ManufacturerName = v.ManufacturerName,
                        Price = v.Price,
                        seller = v.Seller
                    });
                }
                return View(result);
            }
            else
            {
                var result1 = from pro in context.Products
                              join subcat in context.SubCategories on pro.SubCategoryId equals subcat.SubCategoryId
                              join cat in context.Categories on subcat.CategoryId equals cat.CategoryId
                              join manu in context.Manufacturers on pro.ManufacturerId equals manu.ManufacturerId
                              select new
                              {
                                  ProductId = pro.ProductId,
                                  CategoryName = cat.CategoryName,
                                  ProductName = pro.ProductName,
                                  Description = pro.Description,
                                  ManufacturerName = manu.ManufacturerName,
                                  Price = pro.Price,
                                  Seller = pro.Seller,
                              };
                List<Result> result = new List<Result>();
                foreach (var v in result1)
                {
                    result.Add(new Result()
                    {
                        ProductId = v.ProductId,
                        ProductName = v.ProductName,
                        CategoryName = v.CategoryName,
                        Description = v.Description,
                        ManufacturerName = v.ManufacturerName,
                        Price = v.Price,
                        seller = v.Seller
                    });
                }


                List<Tuple<int, Result>> li = new List<Tuple<int, Result>>();
                List<Result> products = new List<Result>();
                List<string> listr = new List<string>();

                string[] ristr = name.Split(' ');
                foreach (var v in result)
                {
                    var str1 = JsonSerializer.Serialize(v).ToLower();
                    int count = 0;
                    foreach (var i in ristr)
                    {
                        if (str1.Contains(i.ToLower()))
                        {
                            count++;
                            if (count > 1)
                            {
                                li.RemoveAt(li.Count - 1);
                                li.Add(Tuple.Create(count, v));
                            }
                            else
                            {
                                li.Add(Tuple.Create(count, v));
                            }
                        }

                    }
                }
                var prod = li.OrderByDescending(x => x.Item1).ToList();
                foreach (var item in prod)
                {
                    products.Add(item.Item2);
                }
                return View(products);
            }

        }
    }
}
