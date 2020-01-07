﻿using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Rideshares.Commands.UpdateRideshare
{
    public class UpdateRideshareCommand : IRequest
    {
        public int Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SubCar Car { get; set; }
        public IList<SubEmployee> Employees { get; set; }

        public class SubCar
        {
            public int Id { get; set; }
        }

        public class SubEmployee
        {
            public int Id { get; set; }
        }
    }
}
