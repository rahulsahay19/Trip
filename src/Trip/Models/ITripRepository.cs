using System.Collections.Generic;

namespace WorldTrip.Models
{
    public interface ITripRepository
    {
        IEnumerable<WorldTrip.Models.Trip> GetTrips();
        IEnumerable<WorldTrip.Models.Trip> GeTripsWithStops();
    }
}