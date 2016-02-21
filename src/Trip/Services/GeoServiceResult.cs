﻿namespace WorldTrip.Services
{
    public class GeoServiceResult
    {
        public bool Success { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Message { get; set; }
    }
}
