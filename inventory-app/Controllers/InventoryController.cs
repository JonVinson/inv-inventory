using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using System.Web;

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using inventory_service;
using inventory_app.Models;
using inventory_data;

namespace inventory_app.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly InventoryService _inventoryService;

        public InventoryController(ILogger<InventoryController> logger, InventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get(DateTime? startDate, DateTime? endDate, string product)
        {
            try
            {
                var items = _inventoryService.GetInventoryItems(startDate, endDate, product);
                return Json(items);
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
                _inventoryService.DeleteInventoryItem(id);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item {id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Update(InventoryItemViewModel model)
        {
            try
            {
                _inventoryService.UpdateInventoryItem(model);
                return Json(model.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating item {model.Id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Create(InventoryItemViewModel model)
        {
            try
            {
                int id = _inventoryService.CreateInventoryItem(model);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating item {model.Description}");
                throw;
            }
        }

        public JsonResult GetProducts()
        {
            try
            {
                var products = _inventoryService.GetProducts()
                    .Select(p => new
                    {
                        Value = p.Id,
                        Text = p.DepartmentCode + " - " + p.ManufacturerCode
                            + " - " + p.ModelNumber + " - " + p.Description
                    });
                return Json(products);
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