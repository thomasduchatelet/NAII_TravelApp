using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Backend.Data.Repositories.Implementations.Filters;
using TravelApp.Backend.Data.Repositories.Interfaces;
using TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Controllers
{
    public class CategoryController : CrudControllerBase<Category, CategoryFilter>
    {

        public CategoryController(ICrudRepository<Category, CategoryFilter> repository, IHttpContextAccessor httpContextAccessor) : base(repository,httpContextAccessor)
        {
        }
    }
}
