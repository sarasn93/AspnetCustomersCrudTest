using Application.Repositories;
using Domain.Entities;
using MediatR;
using Ordering.Application.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
    {

        private readonly IAppDbContext _context;
        public UpdateCustomerCommandHandler(IAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = _context.Customers.Where(a => a.Id == command.Id).FirstOrDefault();
            if (customer == null)
                throw new NotFoundException(nameof(customer), command.Id);

            customer.FirstName = command.FirstName;
            customer.LastName = command.LastName;
            customer.PhoneNumber = command.PhoneNumber;
            customer.Email = command.Email;
            customer.DateOfBirth = command.DateOfBirth;
            customer.BankAccountNumber = command.BankAccountNumber;
            await _context.SaveChangesAsync();
            return customer.Id;
        }
    }
}
