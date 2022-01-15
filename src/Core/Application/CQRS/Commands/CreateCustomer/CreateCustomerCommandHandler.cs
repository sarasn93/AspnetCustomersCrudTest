using Application.Repositories;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;
        private readonly IAsyncRepository<Customer> _repository;

        public CreateCustomerCommandHandler(IMapper mapper, IAsyncRepository<Customer> repository, IAppDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<int> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = new Customer();
            var duplicateValue = CheckDuplicateValue(command);
            if (!string.IsNullOrWhiteSpace(duplicateValue))
                throw new DuplicateException(duplicateValue);
            var newcustomer = _mapper.Map<Customer>(command);

            await _repository.AddAsync(newcustomer);
            return newcustomer.Id;
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
