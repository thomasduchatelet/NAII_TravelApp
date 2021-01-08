using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Backend.Data.Repositories.Implementations.Filters;
using TravelApp.Backend.Data.Repositories.Interfaces;
using TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Shared.Dto;

namespace TravelApp.Backend.Controllers
{

    public class ItineraryController : CrudControllerBase<Itinerary, ItineraryFilter, ItineraryDto>
    {
        public ItineraryController(ICrudRepository<Itinerary, ItineraryFilter> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor){}

        public ActionResult AddLocation(int position, LocationDto location)
        {
            var itinerary = _repository.GetAllEager(new ItineraryFilter() { Id = location.ItineraryId }, _userId).FirstOrDefault();
            if (itinerary == null) return NotFound();
            itinerary.AddLocation(_mapper.Map<Location>(location), position);
            _repository.SaveChanges();
            return Ok();
        }
    }
}
