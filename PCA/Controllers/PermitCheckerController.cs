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
            try
            {
                _buildingService.AddBuilding(building);

                return RedirectToAction("Index");
            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
        }

        public IActionResult BuildingList()
        {
            try
            {
                var buildings = _buildingService.GetBuildings();

                return View(buildings);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
        }




        public IActionResult DeleteBuilding(int id)
        {
            try
            {
                _buildingService.RemoveBuilding(id);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditBuilding(int id)
        {
            try
            {
                var building = _buildingService.FindBuilding(id);
                return View(building);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
        }

        [HttpPost]
        public IActionResult EditBuilding(int id, Building editBuilding)
        {
            try
            {
                _buildingService.EditBuilding(id, editBuilding);

                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
        }

        public IActionResult AddPermission()
        {
            try
            {
                var buildingList = _buildingService.GetBuildings();
                ViewBag.BuildingList = buildingList;
                return View();
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = "Nie można dodać awizacji jesli nie stworzono budynku!";
                return View("ErrorView");
            }



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

            try
            {

                var permissions = _permissionService.GetPermission();

                var buildings = _buildingService.GetBuildings();

                var buildingPermissionViewModels = _buildingPermissionViewServices.TodayPerm(permissions, buildings);

                return View(buildingPermissionViewModels);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }

        }

        public IActionResult DeletePermission(int id)
        {
            try
            {
                _permissionService.RemovePermission(id);

                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
        }

        public IActionResult EditPermission(int id)
        {
            try
            {
                var permission = _permissionService.FindPermission(id);
                var buildingList = _buildingService.GetBuildings();
                ViewBag.BuildingList = buildingList;
                return View(permission);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
           




        }

        [HttpPost]
        public IActionResult EditPermission(int id, Permission editPermission)
        {
            try
            {
                _permissionService.EditPermission(id, editPermission);

                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
        }

        public IActionResult TodayPermission()
        {
            try
            {
                _permissionService.Expiration();



                var permissions = _permissionService.IsPermissionValidToday();

                var buildings = _buildingService.GetBuildings();

                var buildingPermissionViewModels = _buildingPermissionViewServices.TodayPerm(permissions, buildings);

                return View(buildingPermissionViewModels);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }

        }

        public IActionResult Print()
        {
            try
            {
                var permissions = _permissionService.IsPermissionValidToday();

                var buildings = _buildingService.GetBuildings();

                var buildingPermissionViewModels = _buildingPermissionViewServices.TodayPerm(permissions, buildings);

                return View(buildingPermissionViewModels);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = "Nie ma nic do wydrukowania!";
                return View("ErrorView");
            }
            catch (BadRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return View("ErrorView");
            }
        }
    }
}
