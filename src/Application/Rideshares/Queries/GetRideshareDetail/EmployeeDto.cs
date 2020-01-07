using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Rideshares.Queries.GetRideshareDetail
{
    public class EmployeeDto : IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool IsDriver { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}
