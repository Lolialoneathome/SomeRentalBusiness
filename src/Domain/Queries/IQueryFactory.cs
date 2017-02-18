using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Queries
{
    public interface IQueryFactory
    {
        IQuery<TCriterion, TResult> Create<TCriterion, TResult>()
            where TCriterion : ICriterion;
    }
}
