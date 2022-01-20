using Application.CQRS.Commands.CreateCustomer;
using Application.CQRS.Commands.DeleteCustomer;
using Application.CQRS.Queries.GetAllSCustomer;
using Application.CQRS.Queries.GetCustomerById;
using Application.Mappings;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Moq;
using Ordering.Application.Exceptions;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.CQRS.Queries.GetAllSCustomer.GetAllCustomerQuery;

namespace CustomerApi.Test
{
    public class CustomerApiTestController 
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _mockRepo;
        private readonly Customer _customer;

        public CustomerApiTestController()
        {
            _mockRepo = CustomerFakeData.GetCustomerRepository();
            var mapperConfig = new MapperConfiguration(m =>
             {
                 m.AddProfile<MappingProfile>();
             });
            _mapper = mapperConfig.CreateMapper();
            _customer = new Customer()
            {
                FirstName = "sara1",
                LastName = "ngp1",
                Email = "s1@g.com",
                PhoneNumber = "09383240412",
                BankAccountNumber = "1569870",
                DateOfBirth = "137215040"
            };

        }
        [Fact]
        public async Task GetAllCustomer()
        {
            var handler = new GetAllCustomerQueryHandler(_mockRepo.Object);
            var result = await handler.Handle(new GetAllCustomerQuery(), CancellationToken.None);
            result.ShouldBeOfType<List<Customer>>();
            result.Count().ShouldBe(2);
        }

        [Fact]
        public async Task DeleteCustomer()
        {
            var handler = new DeleteCustomerCommandHandler(_mockRepo.Object);
            var result = await handler.Handle(new DeleteCustomerCommand() { Id = 8}, CancellationToken.None);
            var custoemrs = await _mockRepo.Object.GetAllAsync();
            custoemrs.Count().ShouldBe(1);
        }

        [Fact]
        public async Task GetCustomerById()
        {
            var handler = new GetCustomerByIdQueryHandler(_mockRepo.Object);
            var result = await handler.Handle(new GetCustomerByIdQuery() { Id = 8 }, CancellationToken.None);
            result.ShouldBeOfType<Customer>();
        }

        [Fact]
        public async Task NotFoundCustomerById()
        {
            var handler = new GetCustomerByIdQueryHandler(_mockRepo.Object);
            var result = await handler.Handle(new GetCustomerByIdQuery() {Id=8}, CancellationToken.None);
            result.ShouldNotBeOfType<NotFoundException>();
        }
        [Fact]
        public async Task Valid_CreateCustomer()
        {
            var handler = new CreateCustomerCommandHandler(_mapper, _mockRepo.Object);
            await handler.Handle(new CreateCustomerCommand()
            {
                BankAccountNumber = _customer.BankAccountNumber,
                DateOfBirth = _customer.DateOfBirth,
                Email = _customer.Email,
                FirstName = _customer.FirstName,
                LastName = _customer.LastName,
                PhoneNumber = _customer.PhoneNumber
            }
            , CancellationToken.None);
            var custoemrs = await _mockRepo.Object.GetAllAsync();
            custoemrs.Count().ShouldBe(3);
        }

        [Fact]
        public async Task InValid_CreateCustomer()
        {
            var handler = new CreateCustomerCommandHandler(_mapper, _mockRepo.Object);
            _customer.BankAccountNumber = "dddddd";
            await handler.Handle(new CreateCustomerCommand()
            {
                BankAccountNumber = _customer.BankAccountNumber,
                DateOfBirth = _customer.DateOfBirth,
                Email = _customer.Email,
                FirstName = _customer.FirstName,
                LastName = _customer.LastName,
                PhoneNumber = _customer.PhoneNumber
            }
            , CancellationToken.None);
            var custoemrs = await _mockRepo.Object.GetAllAsync();
            custoemrs.Count().ShouldBe(3);
        }
    }
}
