using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.Domain;

namespace Trains.Core.Utilities
{
    public static class RouteCostExtensions
    {
        public static RouteCost<T> GetCostByIds<T>(this IEnumerable<RouteCost<T>> routeCosts, IEnumerable<T> ids)
        {
            return routeCosts.Where(x => x.IsOnRoute(ids)).FirstOrDefault();
        }
    }
}
