using Domain.Entities;
using MediatR;

namespace Application.CQRS.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery :IRequest<Customer>
    {
        public int Id { get; set; }
    }
}
