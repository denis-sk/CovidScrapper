using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }
        DbSet<TodoItem> TodoItems { get; set; }
        DbSet<CoronaCountry> CoronaCountries { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
