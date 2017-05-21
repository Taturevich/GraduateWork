using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Services;

namespace AimlBotWeb.Features.Products
{
    public class ProductController : Controller
    {
        private readonly IProductService _productSevice;

        public ProductController(IProductService productService)
        {
            _productSevice = productService;
        }

        // GET: products
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult All()
        {
            var products = _productSevice.GetAll();

            var productModels = products.Select(Mapper.Map<ProductModel>);

            return View(productModels);
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult Update(int id)
        {
            var product = _productSevice.GetById(id) ?? new Product();
            var model = Mapper.Map<ProductModel>(product);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(ProductModel model)
        {
            var product = Mapper.Map<Product>(model);

            _productSevice.Update(product);

            return RedirectToAction(nameof(Update));
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult Details(int id)
        {
            var message = _productSevice.GetById(id) ?? new Product();
            var model = Mapper.Map<ProductModel>(message);
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult Add()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductModel model)
        {
            var product = Mapper.Map<Product>(model);

            _productSevice.Add(product);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult Delete(int id)
        {
            var product = _productSevice.GetById(id);
            if (product != null)
            {
                _productSevice.Delete(product);
            }

            return RedirectToAction(nameof(All));
        }
    }
}