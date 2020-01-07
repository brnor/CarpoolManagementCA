using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Rideshares.Queries.GetRideshareDetail
{
    public class RideshareDetailVm : IMapFrom<Rideshare>
    {
        public int Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CarDto Car { get; set; }
        public IList<EmployeeDto> Employees { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Rideshare, RideshareDetailVm>()
                .ForMember(d => d.Car, opt => opt.MapFrom(s => s.Car));
        }
    }
}
