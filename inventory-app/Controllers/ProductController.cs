using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using System.Web;

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using inv_service;
using inventory_app.Models;
using inventory_data;

namespace inventory_app.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly InventoryService _inventoryService;

        public ProductController(ILogger<ProductController> logger, InventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            try
            {
                var products = _inventoryService.GetProducts();
                return Json(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading items");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Delete(int id)
        {
            try
            {
                _inventoryService.DeleteProduct(id);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item {id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Update(ProductViewModel model)
        {
            try
            {
                _inventoryService.UpdateProduct(model);
                return Json(model.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating item {model.Id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Create(ProductViewModel model)
        {
            try
            {
                int id = _inventoryService.CreateProduct(model);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating item {model.Description}");
                throw;
            }
        }

        public JsonResult GetDepartments()
        {
            try
            {
                var departments = _inventoryService.GetDepartments()
                    .Select(d => new { Value = d.Id, Text = d.Code });
                return Json(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading items");
                throw;
            }
        }

        public JsonResult GetManufacturers()
        {
            try
            {
                var manufacturers = _inventoryService.GetManufacturers()
                    .Select(d => new { Value = d.Id, Text = d.Code });
                return Json(manufacturers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading items");
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}