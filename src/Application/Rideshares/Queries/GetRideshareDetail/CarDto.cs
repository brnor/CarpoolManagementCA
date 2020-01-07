using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Rideshares.Queries.GetRideshareDetail
{
    public class CarDto : IMapFrom<Car>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int NumberOfSeats { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Car, CarDto>();
        }
    }
}
