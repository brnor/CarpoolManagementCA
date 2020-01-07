using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Rideshare
    {
        public Rideshare()
        {
            EmployeeRideshares = new HashSet<EmployeeRideshare>();
        }

        public int Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Car Car { get; set; }
        public ICollection<EmployeeRideshare> EmployeeRideshares { get; set; }
    }
}
