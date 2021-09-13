using MediatR;
using MOP.Order.API.Application.DTO;
using System.Collections.Generic;

namespace Order.Queries
{
    public class GetAllOrderQuery : IRequest<List<OrderDTO>>
    {
    }
}
