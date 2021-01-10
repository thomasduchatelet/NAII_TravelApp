using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Backend.Data.Repositories.Implementations.Filters;
using TravelApp.Backend.Data.Repositories.Interfaces;
using TravelApp.Backend.Models;
using TravelApp.Shared.Dto;

namespace TravelApp.Backend.Controllers
{
    public class CountryController : CrudControllerBase<Country, CountryFilter, CountryDto>
    {
        public CountryController(ICrudRepository<Country, CountryFilter> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }
    }
}
