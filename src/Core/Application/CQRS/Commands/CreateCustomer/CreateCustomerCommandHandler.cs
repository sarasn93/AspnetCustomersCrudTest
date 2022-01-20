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
        //private readonly IAppDbContext _context;
        private readonly ICustomerRepository _repository;

        public CreateCustomerCommandHandler(IMapper mapper, ICustomerRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<int> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = new Customer();
            var duplicateValue = await CheckDuplicateValue(command);
            if (!string.IsNullOrWhiteSpace(duplicateValue))
                throw new DuplicateException(duplicateValue);
            var newcustomer = _mapper.Map<Customer>(command);

            await _repository.AddAsync(newcustomer);
            return newcustomer.Id;
        }

        private async Task<string> CheckDuplicateValue(CreateCustomerCommand command)
        {
            var dulicate = await _repository.GetCustomerByDataToCheckDuplicate(command);
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
