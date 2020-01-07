using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Car
    {
        public Car()
        {
            Rideshares = new HashSet<Rideshare>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public int NumberOfSeats { get; set; }
        public ICollection<Rideshare> Rideshares { get; }
    }
}
