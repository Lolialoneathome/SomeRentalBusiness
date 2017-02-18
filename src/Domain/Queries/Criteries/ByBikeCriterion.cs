using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Queries.Criteries
{
    public class ByBikeCriterion : ICriterion
    {
        public Bike Bike { get; set; }
    }
}
