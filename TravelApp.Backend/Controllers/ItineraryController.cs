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
        private ICrudRepository<Location, LocationFilter> _locationsRepo;
        public ItineraryController(ICrudRepository<Itinerary, ItineraryFilter> repository, IHttpContextAccessor httpContextAccessor, ICrudRepository<Location, LocationFilter> repo) : base(repository, httpContextAccessor){ _locationsRepo = repo; }
        [HttpPost("AddLocation")]
        public ActionResult AddLocation(int position, LocationDto location)
        {
            var itinerary = _repository.GetAllEager(new ItineraryFilter() { Id = location.ItineraryId }, _userId).FirstOrDefault();
            if (itinerary == null) return NotFound();
            itinerary.AddLocation(_mapper.Map<Location>(location), position);
            _repository.SaveChanges();
            return Ok();
        }
        [HttpPost("LocationChangeOrder")]
        public ActionResult<IEnumerable<LocationDto>> ChangeLocationPosition(LocationDto dto)
        {
            var itinerary = _repository.GetAllEager(new ItineraryFilter() { Id = dto.ItineraryId }, _userId).FirstOrDefault();
            var position = (int) dto.Order;
            if (itinerary == null) return NotFound();
            var location = itinerary.Locations.Where(l => l.Id == dto.Id).FirstOrDefault();
            var locations = itinerary.Locations.OrderBy(l => l.Order).ToList();
            if (locations.Count == 0)
                location.Order = 10;
            else if (position <= 0)
                location.Order = locations[0].Order / 2;
            else if (position >= locations.Count )
                location.Order = locations.Max(l => l.Order) + 10;
            else
                location.Order = (locations[position - 1].Order + locations[position].Order) / 2;
            locations = locations.OrderBy(l => l.Order).ToList();
            for (int i = 0; i < locations.Count; i++)
            {
                locations[i].Order = 10 * (i + 1);
                _locationsRepo.Update(location, _userId);
            }
            
            return Ok(_mapper.Map<IEnumerable<LocationDto>>(_locationsRepo.GetAll(new LocationFilter() { ItineraryId = dto.ItineraryId }, _userId).OrderBy(l => l.Order)));
        }
    }
}
