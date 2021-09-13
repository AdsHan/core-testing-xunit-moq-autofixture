using MediatR;
using MOP.Order.API.Application.DTO;

namespace Order.DTO
{
    public class GetByIdOrderQuery : IRequest<OrderDTO>
    {
        public GetByIdOrderQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
