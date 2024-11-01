using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specification;

namespace Talabat.Services
{
	public class OrderService : IOrderService
	{
		private IBasketReposatory _basketReposatory;
		private readonly IUnitOfWork _unitOfWork;
		

		public OrderService(IBasketReposatory basketReposatory,IUnitOfWork unitOfWork) {
			_basketReposatory = basketReposatory;
			_unitOfWork = unitOfWork;
			
		}
		public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
		{
			var basket=await _basketReposatory.GetBasketAsync(basketId);
			var orderItems=new List<OrderItem>();
			if (basket?.Items.Count > 0)
			{
				foreach(var item in basket.Items)
				{
					var product=await _unitOfWork.Repository<Product>().GetAsync(item.Id);
					var productItemOrder = new ProductItemOrdered(item.Id, product.ProductName, product.PictureUrl);
					var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);
					orderItems.Add(orderItem);
				}

			}
			var subTotal=orderItems.Sum(orderItem=>orderItem.Price*orderItem.Quantity);
			var deliveryMethod =await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
			var order=new Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subTotal);
			await _unitOfWork.Repository<Order>().AddAsync(order);

	     	var rusult=    await	_unitOfWork.CompleteAsyns();
			if (rusult <= 0)
			{
				return null;
			}
			return order;

		}

		public Task<Order> GetOrderByIdForUserAsync(int orderIs, string buyerEmail)
		{
			var orderRepo= _unitOfWork.Repository<Order>();
			var spec=new OrderSpecification(orderIs, buyerEmail);
			var order = orderRepo.GetWiheSpecAsync(spec);
			return order;
		}

		public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
		{
			var orderRepo= _unitOfWork.Repository<Order>();
			var spec=new OrderSpecification(buyerEmail);
			var orders = await orderRepo.GetAllWithSpecAsync(spec);
			return (IReadOnlyList<Order>)orders;
		}
	}
}
