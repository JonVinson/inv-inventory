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
    public class ManufacturerController : Controller
    {
        private readonly ILogger<ManufacturerController> _logger;
        private readonly InventoryService _inventoryService;

        public ManufacturerController(ILogger<ManufacturerController> logger, InventoryService inventoryService)
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
                var manufacturers = _inventoryService.GetManufacturers();
                return Json(manufacturers);
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
                _inventoryService.DeleteManufacturer(id);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item {id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Update(Manufacturer model)
        {
            try
            {
                _inventoryService.UpdateManufacturer(model);
                return Json(model.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating item {model.Id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Create(Manufacturer model)
        {
            try
            {
                int id = _inventoryService.CreateManufacturer(model);
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