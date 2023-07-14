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
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly InventoryService _inventoryService;

        public CustomerController(ILogger<CustomerController> logger, InventoryService inventoryService)
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
                var suppliers = _inventoryService.GetCustomers();
                return Json(suppliers);
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
                _inventoryService.DeleteCustomer(id);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item {id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Update(Company model)
        {
            try
            {
                _inventoryService.UpdateCustomer(model);
                return Json(model.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating item {model.Id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Create(Company model)
        {
            try
            {
                int id = _inventoryService.CreateCustomer(model);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating item {model.Name}");
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