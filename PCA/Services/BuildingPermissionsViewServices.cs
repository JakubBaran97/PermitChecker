using PermitChecker.Models;

namespace PermitChecker.Services
{
    public interface IBuildingPermissionViewServices
    {
        public List<BuildingPermissionViewModel> TodayPerm(List<Permission> permissions, List<Building> buildings);
    }
    public class BuildingPermissionsViewServices : IBuildingPermissionViewServices
    {

        public List<BuildingPermissionViewModel> TodayPerm(List<Permission> permissions, List<Building> buildings)
        {
            List<BuildingPermissionViewModel> buildingPermissionViewModels = new List<BuildingPermissionViewModel>();

            foreach (var building in buildings)
            {
                var viewModel = new BuildingPermissionViewModel
                {
                    Buildings = building,
                    Permissions = permissions.Where(p => p.BuildingID == building.Id).ToList()
                };

                buildingPermissionViewModels.Add(viewModel);
            }

            return buildingPermissionViewModels;
        }
    }
}
