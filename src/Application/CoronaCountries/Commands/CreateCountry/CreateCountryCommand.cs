using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CoronaCountries.Commands.CreateCountry
{
    public class CreateCountryCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public decimal TotalCases { get; set; }
        public decimal NewCases { get; set; }
        public decimal TotalDeaths { get; set; }
        public decimal NewDeaths { get; set; }
        public decimal TotalRecovered { get; set; }
    }
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateCountryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
          
            var exists = _context.CoronaCountries.FirstOrDefault(item => item.Title == request.Title);

            if (string.IsNullOrEmpty(request.Title)) throw new Exception("Empty Title");

            if (exists != null)
            {
                exists.NewCases = request.NewCases;
                exists.Title = request.Title;
                exists.NewDeaths = request.NewDeaths;
                exists.TotalCases = request.TotalCases;
                exists.TotalDeaths = request.TotalDeaths;
                exists.TotalRecovered = request.TotalRecovered;

                _context.CoronaCountries.Update(exists);

                await _context.SaveChangesAsync(cancellationToken);
                return exists.Id;
            }
            else
            {
                var entity = new CoronaCountry
                {
                    Title = request.Title,
                    NewCases = request.NewCases,
                    NewDeaths = request.NewDeaths,
                    TotalDeaths = request.TotalDeaths,
                    TotalCases = request.TotalCases,
                    TotalRecovered = request.TotalRecovered
                };

                _context.CoronaCountries.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}