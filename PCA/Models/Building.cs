﻿namespace PermitChecker.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Permission> Permissions { get; set; }




    }
}
