using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Orders.Commands.CreateOrder;
using FreightManagement.Application.Orders.Commands.CreateOrderItem;
using FreightManagement.Application.Orders.Commands.RemoveOrderItem;
using FreightManagement.Application.Orders.Commands.UpdateOrder;
using FreightManagement.Application.Orders.Commands.UpdateOrderStatus;
using FreightManagement.Application.Orders.Queries;
using FreightManagement.Application.Orders.Queries.OrderSearch;
using FreightManagement.Domain.Entities.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Orders
{

    public class OrderController : ApiControllerBase
    {
        private readonly ILogger _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<ModelView<OrderDto>>> GetOrder(long id)
        {
            return await Mediator.Send(new GetOrderById ( id ));
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
            _logger.LogInformation(command.ToString());
            var id =  await Mediator.Send(command);
            return Ok(new { Id = id, success = true, message = "Order Created" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> Update(int id, UpdateOrderCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return Ok(new { Id = id, success = true, message = "Order Updated" });
        }

        [HttpPut]
        [Route("{id}/shipped")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> UpdateOrderStatusToShipped(long id)
        {
            var updateId = await Mediator.Send(new UpdateOrderStatusCommand(id, OrderStatus.Shipped));

            return Ok(new { Id = updateId, success = true, message = "Order marked Shipped" });
        }

        [HttpPut]
        [Route("{id}/cancel")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> UpdateOrderStatusToCancell(long id)
        {
            var updateId = await Mediator.Send(new UpdateOrderStatusCommand(id, OrderStatus.Cancelled));

            return Ok(new { Id = updateId, success = true, message = "Order marked Cancelled" });
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
