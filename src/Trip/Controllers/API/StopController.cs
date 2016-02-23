using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using WorldTrip.Models;
using WorldTrip.Services;
using WorldTrip.ViewModels;

namespace WorldTrip.Controllers.API
{
    [Authorize]
    [Route("api/trip/{tripName}/stop")]
    public class StopController : Controller
    {
        private ILogger<StopController> _logger;
        private ITripRepository _tripRepository;
        private GeoService _geoService;

        public StopController(ITripRepository tripRepository, ILogger<StopController> logger, GeoService geoService)
        {
            _tripRepository = tripRepository;
            _logger = logger;
            _geoService = geoService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _tripRepository.GetTripByName(tripName, User.Identity.Name);

                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(s=>s.Order)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Stops for Trip {tripName}"+ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json("Error occured Finding the Trip Name");
            }
        }

        [HttpPost("")]
        public async Task<JsonResult> Post(string tripName, [FromBody] StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Mapping the Entity
                    var newStop = Mapper.Map<Stop>(vm);

                    //Looking for coordinates

                    var geoResult = await _geoService.Lookup(newStop.Name);

                    if (!geoResult.Success)
                    {
                        Response.StatusCode = (int) HttpStatusCode.BadRequest;
                       return Json(geoResult.Message);
                    }

                    newStop.Longitude = geoResult.Longitude;
                    newStop.Latitude = geoResult.Latitude;
                    //Saving to the database
                    _tripRepository.AddStop(tripName, User.Identity.Name, newStop);

                    if (_tripRepository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new Stop", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json("Failed to save new Stop");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new stop");
        }
    }
}
