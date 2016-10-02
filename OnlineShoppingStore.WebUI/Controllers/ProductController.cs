using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Concrete;
using OnlineShoppingStore.Domain.Entities;
using OnlineShoppingStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: Product
       

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                            .Where(p=> category== null || p.Category== category)
                            .OrderBy(p => p.ProductId)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                                repository.Products.Count() :
                                repository.Products.Where(p => p.Category == category).Count()
                
                },

                CurrentCategory = category


            };

            return View(model);
        }

       
        [HttpPost]
        public ActionResult List(string searchTerm)
        {
            EFDbContext db = new EFDbContext();
            ProductsListViewModel model = new ProductsListViewModel();
            
            if (String.IsNullOrEmpty(searchTerm))
            {
                model.Products = db.Products;
            }
            else
            {
                model.Products = db.Products.Where(x => x.Name.StartsWith(searchTerm));
            }

            
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.CurrentPage = 1; model.PagingInfo.ItemsPerPage = 1; model.PagingInfo.TotalItems = 1;
            
            model.CurrentCategory = db.Products.Where(p => p.Category == model.CurrentCategory).ToString();
            

            return View(model);
        }

        public JsonResult GetProducts(string term)
        {
            EFDbContext db = new EFDbContext();
            

            List<string> products = db.Products.Where(s => s.Name.StartsWith(term)).Select(x => x.Name).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);

        }
    }
}