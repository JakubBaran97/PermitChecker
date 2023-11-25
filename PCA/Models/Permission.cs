namespace PermitChecker.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RangeOfHours { get; set; }
        public string Description { get; set; }

        public string Asist { get; set; }

        public string? BuildingName { get; set; }

        
    }
}
