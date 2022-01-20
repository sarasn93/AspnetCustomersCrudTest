using Application.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.GetAllSCustomer
{
    public class GetAllCustomerQuery : IRequest<IEnumerable<Customer>>
    {
        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<Customer>>
        {
            private readonly ICustomerRepository _repository;
            public GetAllCustomerQueryHandler(ICustomerRepository repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public async Task<IEnumerable<Customer>> Handle(GetAllCustomerQuery query, CancellationToken cancellationToken)
            {
                var customerList = await _repository.GetAllAsync();
                if (customerList == null)
                {
                    return null;
                }
                return customerList;
            }
        }
    }
}
