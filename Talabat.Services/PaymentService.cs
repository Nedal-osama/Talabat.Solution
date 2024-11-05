using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.V2;
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
using Product = Talabat.Core.Entites.Product;

namespace Talabat.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketReposatory _basketReposatory;
		private readonly IUnitOfWork _unitOfWork;

		public PaymentService(IConfiguration configuration,IBasketReposatory basketReposatory,IUnitOfWork unitOfWork)
        {
			_configuration = configuration;
			_basketReposatory = basketReposatory;
			_unitOfWork = unitOfWork;
		}

		public object Currency { get; private set; }

		public async Task<customerBasket?> CreateOrUpdatePaymentIntent(string basketId)
		{
			StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];
			var Basket=await _basketReposatory.GetBasketAsync(basketId);
			if(Basket == null)
			{
				return null;
			}
			var shippingPrice = 0M;
			if (Basket.DeliveryMethodId.HasValue)
			{
				var Deliverymethod =await _unitOfWork.Repository<DeliveryMethod>().GetAsync(Basket.DeliveryMethodId.Value);
				shippingPrice = Deliverymethod.Cost;

			}
			if(Basket.Items.Count > 0)
			{
				foreach(var item in Basket.Items)
				{
					var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
					if (item.Price != product.Price)
						item.Price = product.Price;


				}

			}
			var subTotal=Basket.Items.Sum(item=>item.Price*item.Quantity);
			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrEmpty(Basket.PaymentIntentId))
			{
				var options =new PaymentIntentCreateOptions()
				{
					Amount=(long) subTotal*100+(long) shippingPrice*100,
					Currency="usd",
					PaymentMethodTypes=new List<string>() { "card"}
				};
				paymentIntent = await service.CreateAsync(options);
				Basket.PaymentIntentId = paymentIntent.Id;
				Basket.ClintSecrit=paymentIntent.ClientSecret;
			}
			else
			{
				var Options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)subTotal * 100 + (long)shippingPrice * 100,
				};
				paymentIntent = await service.UpdateAsync(Basket.PaymentIntentId, Options);
				Basket.PaymentIntentId=paymentIntent.Id;
				Basket.ClintSecrit = paymentIntent.ClientSecret;
			}
			await _basketReposatory.UpdateBasketAsync(Basket);
			return Basket;
		}
	}
}
