using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Backend.Data.Repositories.Interfaces;
using TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelApp.Shared.Dto;
using AutoMapper;

namespace TravelApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudControllerBase<T, I, D> : ControllerBase where T : TravelAppClass where I : IFilter<T> where D : BaseDto
    {
        protected readonly ICrudRepository<T, I> _repository;
        protected readonly string _userId;
        protected readonly IMapper _mapper;

        public CrudControllerBase(ICrudRepository<T, I> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<T, D>()).CreateMapper();
            }
        [HttpGet("GetAll")]
        public virtual ActionResult<IEnumerable<T>> GetAll([FromQuery] I filter)
        {
            return Ok(_repository.GetAll(filter, _userId));
        }

        [HttpGet("GetAllEager")]
        public virtual ActionResult<IEnumerable<T>> GetAllEager([FromQuery] I filter)
        {
            return Ok(_repository.GetAllEager(filter, _userId));
        }
        [HttpPut("Create")]
        public virtual ActionResult<T> Create(T input)
        {
            return Ok(_repository.Create(input, _userId));
        }
        [HttpPut("Update")]

        public virtual ActionResult<T> Update(T input)
        {
            return Ok(_repository.Update(input, _userId));
        }
        [HttpPost("Delete")]

        public virtual ActionResult Delete(long id)
        {
            if (_repository.Delete(id, _userId))
                return Ok();
            return NotFound();
        }


    }
}
