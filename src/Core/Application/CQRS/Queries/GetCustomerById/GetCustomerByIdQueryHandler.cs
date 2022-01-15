using Application.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly IAppDbContext _context;
        public GetCustomerByIdQueryHandler(IAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
            if (customer == null)
                throw new NotFoundException(nameof(customer), query.Id);

            return customer;
        }
    }
}
