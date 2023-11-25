using PCA.Entities;
using PermitChecker.Models;
using System.Security;

namespace PermitChecker.Services
{
    public interface IPermissionService
    {
        public void AddPermission(Permission permission);
        public List<Permission> GetPermission();
        public void RemovePermission(int id);
        public void EditPermission(int id, Permission editPermission);
        public Permission FindPermission(int Id);

    }
    public class PermissionService : IPermissionService
    {

        private readonly PermitCheckerDbContext _dbContext;
        public PermissionService(PermitCheckerDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public void AddPermission(Permission permission)
        {
            var newPermission = new Permission();
            {
                newPermission.Place = permission.Place;

                newPermission.Company = permission.Company;

                newPermission.StartDate= permission.StartDate;

                newPermission.EndDate= permission.EndDate;

                newPermission.RangeOfHours= permission.RangeOfHours;

                newPermission.Description = permission.Description;

                newPermission.Asist= permission.Asist;

                newPermission.BuildingName = permission.BuildingName;

                    

            }

            _dbContext.Permission.Add(newPermission);
            _dbContext.SaveChanges();
        }

        public List<Permission> GetPermission() 
        {
            var permission = _dbContext.Permission.ToList();

            return permission;
        }

        public Permission FindPermission(int Id) 
        {
            var permission = _dbContext.Permission.FirstOrDefault(x => x.Id == Id);

            return permission;
        }

        public void RemovePermission(int id)
        {
            var permission = _dbContext.Permission.FirstOrDefault(x => x.Id == id);

            _dbContext.Permission.Remove(permission);
            _dbContext.SaveChanges();
        }

        public void EditPermission(int id, Permission editPermission)
        {
            var permission = _dbContext.Permission.FirstOrDefault(x => x.Id == id);

            permission.Place  = editPermission.Place;

            permission.Company = editPermission.Company;

            permission.StartDate = editPermission.StartDate;

            permission.EndDate = editPermission.EndDate;

            permission.RangeOfHours = editPermission.RangeOfHours;

            permission.Description = editPermission.Description;

            permission.Asist = editPermission.Asist;

            permission.BuildingName = editPermission.BuildingName;

            _dbContext.SaveChanges();

        }
    }
}
