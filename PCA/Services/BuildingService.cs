using PCA.Entities;
using PermitChecker.Exceptions;
using PermitChecker.Models;
using System.Security;

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
            if (building.Name == null) throw new BadRequestException("Podaj nazwę");

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

            if (building == null || !building.Any()) throw new NotFoundException("Nie znaleziono budynku!");
            return building;


        }

        public Building FindBuilding(int id)
        {
            var building = _dbContext.Building.FirstOrDefault(x => x.Id == id);
            if (building == null || id == null) throw new NotFoundException("Nie znaleziono budynku!");


            return building;
        }

        public void RemoveBuilding(int id)
        {
            var building = _dbContext.Building.FirstOrDefault(x => x.Id == id);
            var permission = _dbContext.Permission.Where(x => x.BuildingID == id);

            if (building == null || id == null) throw new NotFoundException("Nie znaleziono budynku");
            _dbContext.Permission.RemoveRange(permission);
            _dbContext.Building.Remove(building);

            _dbContext.SaveChanges();
        }

        public void EditBuilding(int id, Building editBuilding)
        {
            if (editBuilding.Name == null) throw new BadRequestException("Podaj nazwę");
            var building = _dbContext.Building.FirstOrDefault(x => x.Id == id);
            if (building == null || id == null) throw new NotFoundException("Nie znaleziono budynku!");
            building.Name = editBuilding.Name;

            _dbContext.SaveChanges();

        }







    }
}
