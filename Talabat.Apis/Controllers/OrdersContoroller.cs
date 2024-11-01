using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Erorrs;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Services.Contract;

namespace Talabat.Apis.Controllers
{

	public class OrdersContoroller : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersContoroller(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}
		[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpGet("api/OrdersController/CreateOrder")]
		public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
		{
			var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.deliveryMethodId, address);

			if (order is null)
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(_mapper.Map<Order,OrderDto>(order));
		}
		[HttpGet("api/OrdersController/GetOrderForUser")]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetOrderForUser(string email)
		{
			var orders=_orderService.GetOrderForUserAsync(email);
			return Ok(orders);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Order>>GetOrderByIdForUserAstnc(int id, string email)
		{
			var order=_orderService.GetOrderByIdForUserAsync(id, email);
			if(order is null)return NotFound(new ApiResponse(404));
			return Ok(order);
		}
    }
}
