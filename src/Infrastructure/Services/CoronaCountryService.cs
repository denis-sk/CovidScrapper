using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class CoronaCountryService : ICoronaCountryService
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CoronaCountryService> _logger;

        public CoronaCountryService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> AddCoronaCountryRange(List<CoronaCountry> coronaCountryList)
        {
            //_context
            try
            {
                var existedCountries = _context.CoronaCountries.Select(m => m);
                foreach(var c in existedCountries)
                {

                }
                _context.CoronaCountries.AddRange(coronaCountryList);
                await _context.SaveChangesAsync(new CancellationToken());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Result.Success();
        }
    }
}
