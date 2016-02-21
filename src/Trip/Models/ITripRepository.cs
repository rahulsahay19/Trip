using System.Collections.Generic;
using WorldTrip.ViewModels;

namespace WorldTrip.Models
{
    public interface ITripRepository
    {
        IEnumerable<WorldTrip.Models.Trip> GetTrips();
        IEnumerable<WorldTrip.Models.Trip> GeTripsWithStops();
        void AddTrip(Trip vm);
        bool SaveAll();
        Trip GetTripByName(string tripName);
        void AddStop(string tripName,Stop newStop);
    }
}