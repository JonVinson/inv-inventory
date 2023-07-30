using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using System.Web;

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using inventory_service;
using inventory_app.Models;
using inventory_data;
using KendoCoreService.Models.Request;
using KendoCoreService.Extensions;

namespace inventory_app.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly InventoryService _inventoryService;

        public TransactionController(ILogger<InventoryController> logger, InventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Get(Request request, DateTime? startDate, DateTime? endDate, TransactionType transType, string product, string company)
        {
            try
            {
                var items = _inventoryService.GetTransactions(startDate, endDate, transType, product, company);
                //if (skipItems > 0)
                //{
                //    request.Skip = skipItems;
                //}
                var result = items.ToDataSourceResult(request);
                return Ok(result);
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
                _inventoryService.DeleteTransaction(id);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item {id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Update(TransactionViewModel model)
        {
            try
            {
                _inventoryService.UpdateTransaction(model);
                return Json(model.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating item {model.Id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Create(TransactionViewModel model)
        {
            try
            {
                int id = _inventoryService.CreateTransaction(model);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating item {model.Date} {model.ProductName}");
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

        public JsonResult GetCompanies()
        {
            try
            {
                var products = _inventoryService.GetCompanies()
                    .Select(p => new
                    {
                        Value = p.Id,
                        Text = p.Code + " - " + p.Name
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