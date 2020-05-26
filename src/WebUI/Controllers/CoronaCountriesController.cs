using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Application.CoronaCountries.Commands.CreateCountry;
using MediatR;
using CleanArchitecture.Application.CoronaCountries.Commands.Queries;

namespace CleanArchitecture.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoronaCountriesController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateCountryCommand command) => await Mediator.Send(command);
       
        [HttpGet]
        public async Task<ActionResult<CoronaItemVm>> Get() => await Mediator.Send(new GetCoronaListQuery());
    }
}
