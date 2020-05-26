using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ICoronaCountryService
    {
        Task<Result> AddCoronaCountryRange(List<CoronaCountry> CoronaCountry);
    }
}
