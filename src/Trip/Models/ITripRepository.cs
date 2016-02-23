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
        Trip GetTripByName(string tripName, string UserName);
        void AddStop(string tripName, string UserName,Stop newStop);
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}