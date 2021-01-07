using Microsoft.AspNetCore.Http;
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

    public class CategoryController : CrudControllerBase<Category, CategoryFilter>
    {
        public CategoryController(ICrudRepository<Category, CategoryFilter> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }
    }
}
