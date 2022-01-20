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
        private readonly ICustomerRepository _repository;
        public DeleteCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(command.Id);
            if (customer == null)
                throw new NotFoundException(nameof(customer), command.Id);
            await _repository.DeleteAsync(customer);
            return customer.Id;
        }
    }
}
