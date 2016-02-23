using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WorldTrip.Models
{
    public class TripUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}
