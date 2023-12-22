using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PCA.Entities;
using PermitChecker.Exceptions;
using PermitChecker.Models;
using System.Reflection.Metadata.Ecma335;
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
        public List<Permission> IsPermissionValidToday();
        public void Expiration();

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
            if (permission.StartDate == null
                || permission.Place == null
                || permission.Company == null
                || permission.EndDate == null
                || permission.Company == null
                || permission.RangeOfHours == null
                || permission.Asist == null
                || permission.Description == null) throw new BadRequestException("Uzupełnij wszystkie pola");

            if (permission.StartDate > permission.EndDate) throw new BadRequestException("data rozpoczęcia nie może być mniejsza niż data końca");

            var newPermission = new Permission()
            {
                Place = permission.Place,

                Company = permission.Company,

                StartDate = permission.StartDate,

                EndDate = permission.EndDate,

                RangeOfHours = permission.RangeOfHours,

                Description = permission.Description,

                Asist = permission.Asist,

                BuildingID = permission.BuildingID
            };










            _dbContext.Permission.Add(newPermission);

            _dbContext.SaveChanges();
        }

        public List<Permission> GetPermission()
        {

            var permission = _dbContext.Permission.ToList();
            if (permission == null || !permission.Any()) throw new NotFoundException("Nie znaleziono Awizacji!");




            return permission;
        }

        public Permission FindPermission(int id)
        {
            var permission = _dbContext.Permission.FirstOrDefault(x => x.Id == id);
            if (permission == null || id == null) throw new NotFoundException("Nie znaleziono Awizacji!");
            return permission;
        }

        public void RemovePermission(int id)
        {
            var permission = _dbContext.Permission.FirstOrDefault(x => x.Id == id);
            if (permission == null || id == null) throw new NotFoundException("Nie znaleziono Awizacji!");
            _dbContext.Permission.Remove(permission);
            _dbContext.SaveChanges();
        }

        public void EditPermission(int id, Permission editPermission)
        {

            if (editPermission.StartDate == null
                || editPermission.Place == null
                || editPermission.Company == null
                || editPermission.EndDate == null
                || editPermission.Company == null
                || editPermission.RangeOfHours == null
                || editPermission.Asist == null
                || editPermission.Description == null) throw new BadRequestException("Uzupełnij wszystkie pola");

            if (id == null) throw new NotFoundException("Nie znaleziono Awizacji!");


            var permission = _dbContext.Permission.FirstOrDefault(x => x.Id == id);

            permission.Place = editPermission.Place;

            permission.Company = editPermission.Company;

            permission.StartDate = editPermission.StartDate;

            permission.EndDate = editPermission.EndDate;

            permission.RangeOfHours = editPermission.RangeOfHours;

            permission.Description = editPermission.Description;

            permission.Asist = editPermission.Asist;



            _dbContext.SaveChanges();

        }

        public List<Permission> IsPermissionValidToday()
        {
            DateTime today = DateTime.Today;

            List<Permission> validPermissions = _dbContext.Permission
                .Where(p => p.StartDate <= today && today <= p.EndDate)
                .ToList();
            if (validPermissions == null || !validPermissions.Any()) throw new NotFoundException("Nie ma żadnych awizacji an dziś");

            return validPermissions;
        }

        public void Expiration()
        {
            DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
            var expiredPermission = _dbContext.Permission.Where(x => x.EndDate < oneMonthAgo).ToList();


            _dbContext.Permission.RemoveRange(expiredPermission);
            _dbContext.SaveChanges();
        }
    }
}
