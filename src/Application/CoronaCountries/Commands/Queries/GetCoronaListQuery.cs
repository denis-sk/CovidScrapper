using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CoronaCountries.Commands.Queries
{
    public class GetCoronaListQuery : IRequest<CoronaItemVm>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetCoronaListQuery, CoronaItemVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoronaItemVm> Handle(GetCoronaListQuery request, CancellationToken cancellationToken)
        {
            return new CoronaItemVm
            {
                List = await _context.CoronaCountries
                    .ProjectTo<CoronaItemDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken)
        };
        }
    }
}
