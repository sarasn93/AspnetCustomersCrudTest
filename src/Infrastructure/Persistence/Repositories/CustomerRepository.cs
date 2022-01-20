using Application;
using Application.CQRS.Commands.CreateCustomer;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository 
    {
        public CustomerRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Customer> GetCustomerByDataToCheckDuplicate(CreateCustomerCommand command)
        {
            var dulicate = await _dbContext.Customers.Where(c => c.FirstName == command.FirstName
                         || c.LastName == command.LastName || c.Email == command.Email
                         || c.DateOfBirth == command.DateOfBirth).FirstOrDefaultAsync();
            return dulicate;
        }
    }
}
