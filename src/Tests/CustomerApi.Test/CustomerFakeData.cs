using Application.Repositories;
using Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Test
{
    public static class CustomerFakeData
    {
        public static Mock<ICustomerRepository> GetCustomerRepository()
        {
            var customers = new List<Customer>()
            {
                new Customer() {Id=8, FirstName="sara", LastName="ngp", Email="s@g.com"
                , PhoneNumber="09383240442", BankAccountNumber="156987", DateOfBirth="13721504"},
                new Customer(){ Id=9, FirstName="sam", LastName="ddd", Email="d@g.com"
                , PhoneNumber="09383247789", BankAccountNumber="4244", DateOfBirth="13621504"}
            };
            var mockrepo = new Mock<ICustomerRepository>();
            mockrepo.Setup(x => x.GetAllAsync()).ReturnsAsync(customers);

            mockrepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
             .ReturnsAsync((int i) => customers.Where(u => u.Id == i).FirstOrDefault());

            mockrepo.Setup(m => m.DeleteAsync(It.IsAny<Customer>()))
             .Callback<Customer>((entity) => customers.Remove(entity)); ;

            mockrepo.Setup(x => x.AddAsync(It.IsAny<Customer>())).ReturnsAsync((Customer customer) =>
              {
                  customers.Add(customer);
                  return customer;
              });
            return mockrepo;
        }
    }
}
