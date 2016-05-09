using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Core.Domain
{
    public class ShortestRoute
    {
        private int trips;
        private int totalCost;

        public ShortestRoute(int totalCost, int trips)
        {
            this.totalCost = totalCost;
            this.trips = trips;
        }

        public int Total => totalCost;
        public int Trips => trips;
    }
}
