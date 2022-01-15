using Application.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
    {
        private readonly IAppDbContext _context;
        public DeleteCustomerCommandHandler(IAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
            if (customer == null)
                throw new NotFoundException(nameof(customer), command.Id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }
    }
}
