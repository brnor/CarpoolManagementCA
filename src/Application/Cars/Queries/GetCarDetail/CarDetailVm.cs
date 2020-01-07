using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Cars.Queries.GetCarDetail
{
    public class CarDetailVm : IMapFrom<Car>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public int NumberOfSeats { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Car, CarDetailVm>();
        }
    }
}
