using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.Rideshares.Queries.GetRidesharesList
{
    public class RideshareDto : IMapFrom<Rideshare>
    {
        public int Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Rideshare, RideshareDto>();
        }
    }
}
