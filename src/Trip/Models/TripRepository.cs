using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

namespace WorldTrip.Models
{
    public class TripRepository : ITripRepository
    {
        private TripContext _context;
        private ILogger<TripRepository> _logger;

        public TripRepository(TripContext context, ILogger<TripRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<WorldTrip.Models.Trip> GetTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Couldn't fetch trips from database:- {0}", ex);
                return null;
            }
        }

        public IEnumerable<WorldTrip.Models.Trip> GeTripsWithStops()
        {
            try
            {
                return _context.Trips
                    .Include(t => t.Stops)
                    .OrderBy(t => t.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Couldn't fetch trips from database:- {0}", ex);
                return null;
            }
        }

        public void AddTrip(Trip vm)
        {
            _context.Add(vm);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .FirstOrDefault(t => t.Name == tripName);
        }

        public void AddStop(string tripName,Stop newStop)
        {
            var theTrip = GetTripByName(tripName);
            newStop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            theTrip.Stops.Add(newStop);
            _context.Stops.Add(newStop);
        }
    }
}
