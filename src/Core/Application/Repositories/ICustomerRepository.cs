using Application.CQRS.Commands.CreateCustomer;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ICustomerRepository: IAsyncRepository<Customer>
    {
        Task<Customer> GetCustomerByDataToCheckDuplicate(CreateCustomerCommand customer);
    }
}
