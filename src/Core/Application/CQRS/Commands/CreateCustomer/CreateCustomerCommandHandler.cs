using Application.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IAppDbContext _context;
        public CreateCustomerCommandHandler(IAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<int> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = new Customer();
            var duplicate = _context.Customers.Where(c => c.DateOfBirth == command.DateOfBirth
            || c.FirstName == command.FirstName || c.LastName == command.LastName).FirstOrDefault();
            if (duplicate != null)
                return 0;
            customer.FirstName = command.FirstName;
            customer.LastName = command.LastName;
            customer.PhoneNumber = command.PhoneNumber;
            customer.Email = command.Email;
            customer.DateOfBirth = command.DateOfBirth;
            customer.BankAccountNumber = command.BankAccountNumber;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }

    }
}
