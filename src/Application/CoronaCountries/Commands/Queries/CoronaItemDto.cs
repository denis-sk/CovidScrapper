using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.CoronaCountries.Commands.Queries
{
    public class CoronaItemDto: IMapFrom<CoronaCountry>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal TotalCases { get; set; }
        public decimal NewCases { get; set; }
        public decimal TotalDeaths { get; set; }
        public decimal NewDeaths { get; set; }
        public decimal TotalRecovered { get; set; }
    }
}
