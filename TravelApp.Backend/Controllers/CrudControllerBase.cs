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
using Microsoft.AspNetCore.Authorization;

namespace TravelApp.Backend.Controllers
{
    public class CrudControllerBase<T, I, D> : CrudControllerBaseBase<T,I> where T : TravelAppClass where I : IFilter<T> where D : BaseDto
    {
        public CrudControllerBase(ICrudRepository<T, I> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }

        [HttpGet("GetAll")]
        public virtual ActionResult<IEnumerable<D>> GetAll([FromQuery] I filter)
        {
            return Ok(_repository.GetAll(filter, _userId));
        }

        [HttpGet("GetAllEager")]
        public virtual ActionResult<IEnumerable<D>> GetAllEager([FromQuery] I filter)
        {
            return Ok(_repository.GetAllEager(filter, _userId));
        }
        [HttpPut("Create")]
        public virtual ActionResult<D> Create(D input)
        {
            return Ok(_repository.Create(_mapper.Map<T>(input), _userId));
        }
        [HttpPut("Update")]

        public virtual ActionResult<D> Update(D input)
        {
            return Ok(_repository.Update(_mapper.Map<T>(input), _userId));
        }
        [HttpPost("Delete")]

        public virtual ActionResult Delete(long id)
        {
            if (_repository.Delete(id, _userId))
                return Ok();
            return NotFound();
        }


    }


    
    public class CrudControllerBase<T, I, GetDto, CreateOrUpdateDto> : CrudControllerBaseBase<T, I> where T : TravelAppClass where I : IFilter<T> where GetDto : BaseDto where CreateOrUpdateDto : BaseDto
    {
        public CrudControllerBase(ICrudRepository<T, I> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }

        [HttpGet("GetAll")]
        public virtual ActionResult<IEnumerable<GetDto>> GetAll([FromQuery] I filter)
        {
            return Ok(_repository.GetAll(filter, _userId));
        }

        [HttpGet("GetAllEager")]
        public virtual ActionResult<IEnumerable<GetDto>> GetAllEager([FromQuery] I filter)
        {
            return Ok(_repository.GetAllEager(filter, _userId));
        }
        [HttpPut("Create")]
        public virtual ActionResult<CreateOrUpdateDto> Create(CreateOrUpdateDto input)
        {
            return Ok(_repository.Create(_mapper.Map<T>(input), _userId));
        }
        [HttpPut("Update")]

        public virtual ActionResult<CreateOrUpdateDto> Update(CreateOrUpdateDto input)
        {
            return Ok(_repository.Update(_mapper.Map<T>(input), _userId));
        }
        [HttpPost("Delete")]

        public virtual ActionResult Delete(long id)
        {
            if (_repository.Delete(id, _userId))
                return Ok();
            return NotFound();
        }


    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CrudControllerBaseBase<T,I> : ControllerBase where T : TravelAppClass where I : IFilter<T>
    {
        protected readonly ICrudRepository<T, I> _repository;
        protected readonly string _userId;
        protected readonly IMapper _mapper;

        public CrudControllerBaseBase(ICrudRepository<T, I> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()).CreateMapper();

        }
    }
}
