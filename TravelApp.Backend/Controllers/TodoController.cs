﻿using Microsoft.AspNetCore.Http;
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

    public class TodoController : CrudControllerBase<Todo, ItemTodoFilter<Todo>, TodoDto>
    {
        public TodoController(ICrudRepository<Todo, ItemTodoFilter<Todo>> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }
    }
}
