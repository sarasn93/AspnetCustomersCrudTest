using Application.CQRS.Queries.GetCustomerById;
using CustomerApi.Controllers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace CustomerApi.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task HandlerReturnsCorrectCustumer()
        {
            const int Id = 1;
            var mediator = new Mock<IMediator>();
            var sut = new CustomerController(mediator.Object);

            await sut.GetById(Id);

            mediator.Verify(x => x.Send(It.Is<Customer>(y => y.Id == Id)), Times.Once);
        }

        [Fact]
        public async Task UserDetailsReturnsHttpNotFoundResultWhenUserIsNull()
        {
            var mediator = new Mock<IMediator>();

            var sut = new CustomerController(mediator.Object);

            var result = await sut.GetById(It.IsAny<int>());

            Assert.IsType<HttpNotFoundResult>(result);
        }
    }


}