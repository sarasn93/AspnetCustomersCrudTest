using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; set; }
        Task<int> SaveChangesAsync();
    }
}
