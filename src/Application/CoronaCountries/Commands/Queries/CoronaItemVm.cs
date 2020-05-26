using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.CoronaCountries.Commands.Queries
{
    public class CoronaItemVm
    {
        public IList<CoronaItemDto> List { get; set; }
    }
}
