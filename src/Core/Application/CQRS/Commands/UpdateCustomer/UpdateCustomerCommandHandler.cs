using Application.Repositories;
using AutoMapper;
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

        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Customer> _repository;

        public UpdateCustomerCommandHandler(IAsyncRepository<Customer> repository, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = _repository.GetByIdAsync(command.Id).Result;
            if (customer == null)
                throw new NotFoundException(nameof(customer), command.Id);
            customer.FirstName = command.FirstName;
            customer.LastName = command.LastName;
            customer.PhoneNumber = command.PhoneNumber;
            customer.Email = command.Email;
            customer.DateOfBirth = command.DateOfBirth;
            customer.BankAccountNumber = command.BankAccountNumber;
            await _repository.UpdateAsync(customer); 
            return customer.Id;
        }
    }
}
