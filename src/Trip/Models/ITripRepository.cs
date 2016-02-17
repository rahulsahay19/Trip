using System.Collections.Generic;

namespace Trip.Models
{
    public interface ITripRepository
    {
        IEnumerable<Trip> GetTrips();
        IEnumerable<Trip> GeTripsWithStops();
    }
}