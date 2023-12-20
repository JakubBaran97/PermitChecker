using Microsoft.AspNetCore.Mvc;
using PermitChecker.Exceptions;
using PermitChecker.Models;
using PermitChecker.Services;

namespace PermitChecker.Controllers
{
    public class PermitCheckerController : Controller
    {
        private readonly IBuildingService _buildingService;
        private readonly IPermissionService _permissionService;
        private readonly IBuildingPermissionViewServices _buildingPermissionViewServices;
        public PermitCheckerController(IBuildingService buildingService, IPermissionService permissionService, IBuildingPermissionViewServices buildingPermissionViewServices)
        {
            _buildingService = buildingService;
            _permissionService = permissionService;
            _buildingPermissionViewServices = buildingPermissionViewServices;
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
            try
            {


                _permissionService.AddPermission(permission);
                return RedirectToAction("Index");
            }

            catch (BadRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
         
        }


        public IActionResult PermissionList()
        {
            _permissionService.Expiration();



            var permissions = _permissionService.GetPermission();

            var buildings = _buildingService.GetBuildings();

            var buildingPermissionViewModels = _buildingPermissionViewServices.TodayPerm(permissions, buildings);

            return View(buildingPermissionViewModels);


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

        public IActionResult TodayPermission()
        {
            _permissionService.Expiration();



            var permissions = _permissionService.IsPermissionValidToday();

            var buildings = _buildingService.GetBuildings();

            var buildingPermissionViewModels = _buildingPermissionViewServices.TodayPerm(permissions, buildings);

            return View(buildingPermissionViewModels);
        }

        public IActionResult Print()
        {
            var permissions = _permissionService.IsPermissionValidToday();

            var buildings = _buildingService.GetBuildings();

            var buildingPermissionViewModels = _buildingPermissionViewServices.TodayPerm(permissions, buildings);

            return View(buildingPermissionViewModels);
        }
    }
}
