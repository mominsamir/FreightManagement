using FreightManagement.Application.Orders.Commands.CreateOrder;
using FreightManagement.Application.Orders.Commands.CreateOrderItem;
using FreightManagement.Application.Orders.Commands.RemoveOrderItem;
using FreightManagement.Application.Orders.Commands.UpdateOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Orders
{

    [Authorize]
    public class OrderController : ApiControllerBase
    {
/*        [HttpGet("{id}")]
        public async Task<ActionResult<FuelProductDto>> GetRack(long id)
        {
            return await Mediator.Send(new GetFuelProductById { Id = id });
        }
*/
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateOrderCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
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
        public async Task<ActionResult> AddOrderItem(CreateOrderItemCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{OrderId}/{OrderItemId}")]
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
