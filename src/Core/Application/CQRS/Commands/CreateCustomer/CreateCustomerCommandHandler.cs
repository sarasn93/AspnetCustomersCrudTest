using Application.Repositories;
using Domain.Entities;
using MediatR;
using Ordering.Application.Exceptions;
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
            var duplicateValue = CheckDuplicateValue(command);
            if (!string.IsNullOrWhiteSpace(duplicateValue))
                throw new DuplicateException(duplicateValue);
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

        private string CheckDuplicateValue(CreateCustomerCommand command)
        {
            var dulicate = _context.Customers.Where(c => c.FirstName == command.FirstName
             || c.LastName == command.LastName || c.Email == command.Email
             || c.DateOfBirth == command.DateOfBirth).FirstOrDefault();
            if (dulicate != null)
            {
                if (dulicate.FirstName == command.FirstName)
                    return "FirstName";
                if (dulicate.LastName == command.LastName)
                    return "LastName";
                if (dulicate.Email == command.Email)
                    return "Email";
                if (dulicate.DateOfBirth == command.DateOfBirth)
                    return "Date of birth";
            }
            return null;
        }
    }
}
