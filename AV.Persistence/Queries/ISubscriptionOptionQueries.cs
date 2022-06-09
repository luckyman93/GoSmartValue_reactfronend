using System;
using System.Collections.Generic;
using AV.Common.Entities;

namespace AV.Persistence.Queries
{
    public interface ISubscriptionOptionQueries : IQueries<SubscriptionOption>
    {
        IEnumerable<SubscriptionOption> FetchAllWhere(Func<SubscriptionOption, bool> predicate);
    }
}
