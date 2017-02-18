using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Queries
{
    public interface IQueryFor<out TResult>
    {
        TResult With<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion;
    }
}
