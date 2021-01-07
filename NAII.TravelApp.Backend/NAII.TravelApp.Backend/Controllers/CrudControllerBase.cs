using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NAII.TravelApp.Backend.Data.Repositories.Interfaces;
using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudControllerBase<T, I> : ControllerBase where T : TravelAppClass where I : IFilter<T>
    {
        protected readonly ICrudRepository<T, I> _repository;
        protected readonly string _userId;

        public CrudControllerBase(ICrudRepository<T, I> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        [HttpGet("GetAll")]
        public virtual IEnumerable<T> GetAll([FromQuery] I filter)
        {
            return _repository.GetAll(filter, _userId);
        }
        [HttpPut("Create")]
        public virtual T Create(T input)
        {
            return _repository.Create(input, _userId);
        }
        [HttpPut("Update")]

        public virtual T Update(T input)
        {
            return _repository.Update(input, _userId);
        }
        [HttpPost("Delete")]

        public virtual void Delete(long id)
        {
            _repository.Delete(id, _userId);
        }


    }
}
