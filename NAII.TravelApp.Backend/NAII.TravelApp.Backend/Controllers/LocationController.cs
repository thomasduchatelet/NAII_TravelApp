﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NAII.TravelApp.Backend.Data.Repositories.Implementations.Filters;
using NAII.TravelApp.Backend.Data.Repositories.Interfaces;
using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : CrudControllerBase<Location, LocationFilter>
    {
        public LocationController(ICrudRepository<Location, LocationFilter> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }
    }
}