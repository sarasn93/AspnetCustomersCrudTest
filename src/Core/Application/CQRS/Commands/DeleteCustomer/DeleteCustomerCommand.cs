using MediatR;

namespace Application.CQRS.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
