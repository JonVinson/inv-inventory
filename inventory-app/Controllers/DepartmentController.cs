using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using System.Web;

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using inventory_service;
using inventory_app.Models;

namespace inventory_app.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly InventoryService _inventoryService;

        public DepartmentController(ILogger<DepartmentController> logger, InventoryService inventoryService)
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
                var departments = _inventoryService.GetDepartments();
                return Json(departments);
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
                _inventoryService.DeleteDepartment(id);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item {id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Update(int id, string? name, string? code)
        {
            try
            {
                _inventoryService.UpdateDepartment(id, name, code);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating item {id}");
                throw;
            }
        }

        [AcceptVerbs("POST")]
        public JsonResult Create(string name, string code)
        {
            try
            {
                int id = _inventoryService.CreateDepartment(name, code);
                return Json(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating item {name}");
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