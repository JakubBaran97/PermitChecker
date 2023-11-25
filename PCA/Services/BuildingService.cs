using PCA.Entities;
using PermitChecker.Models;

namespace PermitChecker.Services
{
    public interface IBuildingService
    {
        public void AddBuilding(Building building);
        public List<Building> GetBuildings();
        public void RemoveBuilding(int id);
        public void EditBuilding(int id, Building editBuilding);

        public Building FindBuilding(int id);
    }
    public class BuildingService : IBuildingService
    {
        private readonly PermitCheckerDbContext _dbContext;
        public BuildingService(PermitCheckerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddBuilding(Building building)
        {
            var newBuilding = new Building()
            {
                Name = building.Name
            };

            _dbContext.Building.Add(building);
            _dbContext.SaveChanges();

        }

        public List<Building> GetBuildings()
        {
            var building = _dbContext.Building.ToList();
            return building;


        }

        public Building FindBuilding(int id) 
        {
            var building = _dbContext.Building.FirstOrDefault(x => x.Id == id);

            return building;
        }

        public void RemoveBuilding(int id)
        {
            var building = _dbContext.Building.FirstOrDefault(x => x.Id == id);

            _dbContext.Building.Remove(building);
            _dbContext.SaveChanges();
        }

        public void EditBuilding(int id, Building editBuilding)
        {
            var building = _dbContext.Building.FirstOrDefault(x => x.Id == id);

            building.Name = editBuilding.Name;

            _dbContext.SaveChanges();

        }

    }
}
