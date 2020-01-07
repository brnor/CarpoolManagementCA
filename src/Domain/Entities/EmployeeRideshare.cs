using System;

namespace Domain.Entities
{
    public class EmployeeRideshare
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int RideshareId { get; set; }
        public Rideshare Rideshare { get; set; }
    }
}
