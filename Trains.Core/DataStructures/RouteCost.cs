﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.DataStructures;

namespace Trains.Core.DataStructures
{
    public class RouteCost<T>
    {
        private List<Route<T>> applicableRoute;
        readonly GraphNode<T> routeStart;
        readonly int routeStartCost;

        public RouteCost(GraphNode<T> routeStart, List<Route<T>> applicableRoute)
        {
            this.routeStart = routeStart;
            this.applicableRoute = applicableRoute;
            TotalCost = applicableRoute.Sum(x => x.Cost)+routeStart.Costs[applicableRoute.First().From];
        }

        public int TotalCost { get; }

        private bool StartsAt(T node)
        {
            return routeStart.NodeKey.Equals(node);
        }

        private bool EndsAt(T node)
        {
            return applicableRoute.Last().To.Equals(node);
        }

        public bool IsOnRoute(IEnumerable<T> stops)
        {
            var startsAt = StartsAt(stops.First());
            var endsAt = EndsAt(stops.Last());
            var midStops = stops.Skip(1).Take(stops.Count() - 2);
            var visistsMidStops = midStops.SequenceEqual(applicableRoute.Select(x=>x.From));
            return startsAt && endsAt && visistsMidStops;
        }
    }
}
