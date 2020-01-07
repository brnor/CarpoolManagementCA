using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Employee
    {
        public Employee()
        {
            EmployeeRideshares = new HashSet<EmployeeRideshare>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDriver { get; set; }
        public ICollection<EmployeeRideshare> EmployeeRideshares { get; }
    }
}
