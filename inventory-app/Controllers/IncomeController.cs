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
    public class IncomeController : Controller
    {
        private readonly ILogger<IncomeController> _logger;
        private readonly InventoryService _inventoryService;

        public IncomeController(ILogger<IncomeController> logger, InventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var income = _inventoryService.IncomeReport(startDate, endDate);
                return Json(income);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading items");
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}