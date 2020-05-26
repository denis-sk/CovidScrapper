using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Entities
{
    public class CoronaCountry : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal TotalCases { get; set; }
        public decimal NewCases { get; set; }
        public decimal TotalDeaths { get; set; }
        public decimal NewDeaths {get;set;}
        public decimal TotalRecovered { get;set;}
    }
}
