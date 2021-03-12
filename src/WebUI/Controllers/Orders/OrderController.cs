using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Orders.Commands.CreateOrder;
using FreightManagement.Application.Orders.Commands.CreateOrderItem;
using FreightManagement.Application.Orders.Commands.RemoveOrderItem;
using FreightManagement.Application.Orders.Commands.UpdateOrder;
using FreightManagement.Application.Orders.Queries;
using FreightManagement.Application.Orders.Queries.OrderSearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Orders
{

    public class OrderController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<ModelView<OrderDto>>> GetOrder(long id)
        {
            return await Mediator.Send(new GetOrderById { Id = id });
        }

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<PaginatedList<OrderListDto>>> Search(QueryOrderSearch query)
        {
            return await Mediator.Send(query);
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<long>> Create(CreateOrderCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> Update(int id, UpdateOrderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("addOrderItem")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> AddOrderItem(CreateOrderItemCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{OrderId}/{OrderItemId}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> RemoveOrderItem(int OrderId, long OrderItemId)
        {
            await Mediator.Send(new RemoveOrderItemCommand
            {
                OrderId = OrderId,
                OrderItemId = OrderItemId
            });

            return NoContent();
        }
    }

}
