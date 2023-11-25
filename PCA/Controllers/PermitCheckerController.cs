using Microsoft.AspNetCore.Mvc;
using PermitChecker.Models;
using PermitChecker.Services;

namespace PermitChecker.Controllers
{
    public class PermitCheckerController : Controller
    {
        private readonly IBuildingService _buildingService;
        private readonly IPermissionService _permissionService;
        public PermitCheckerController(IBuildingService buildingService, IPermissionService permissionService)
        {
            _buildingService = buildingService;
            _permissionService = permissionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBuilding()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddBuilding(Building building)
        {
            _buildingService.AddBuilding(building);

            return RedirectToAction("Index");
        }

        public IActionResult BuildingList()
        {
            var buildings = _buildingService.GetBuildings();

            return View(buildings);
        }




        public IActionResult DeleteBuilding(int id)
        {
            _buildingService.RemoveBuilding(id);

            return RedirectToAction("Index");
        }

        public IActionResult EditBuilding(int id)
        {
            var building = _buildingService.FindBuilding(id);
            return View(building);
        }

        [HttpPost]
        public IActionResult EditBuilding(int id, Building editBuilding)
        {
            _buildingService.EditBuilding(id, editBuilding);

            return RedirectToAction("Index");
        }

        public IActionResult AddPermission()
        {
            var buildingList = _buildingService.GetBuildings();
            ViewBag.BuildingList = buildingList;
            return View();
        }

        [HttpPost]
        public IActionResult AddPermission(Permission permission)
        {
            _permissionService.AddPermission(permission);
            return RedirectToAction("Index");
        }


        public IActionResult PermissionList()
        {
            var permission = _permissionService.GetPermission();
            return View(permission);
        }

        public IActionResult DeletePermission(int id)
        {
            
           _permissionService.RemovePermission(id);

            return RedirectToAction("Index");
        }

        public IActionResult EditPermission(int id)
        {
            var permission = _permissionService.FindPermission(id);
            var buildingList = _buildingService.GetBuildings();
            ViewBag.BuildingList = buildingList;
            return View(permission);

        }

        [HttpPost]
        public IActionResult EditPermission(int id, Permission editPermission)
        {
            _permissionService.EditPermission(id, editPermission);

            return RedirectToAction("Index");
        }

    }
}
