using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Erorrs;
using Talabat.Core.Entites;
using Talabat.Core.Repository.Contract;

namespace Talabat.Apis.Controllers
{
	
	public class BasketContoroller : BaseApiController
	{
        private readonly IBasketReposatory _basketReposatory;
		private readonly IMapper _mapper;

		public BasketContoroller(IBasketReposatory basketReposatory,IMapper mapper)
        {
            _basketReposatory = basketReposatory;
			_mapper = mapper;
		}
        [HttpGet("{id}")]
        public async Task<ActionResult<customerBasket>> GetBasket(string id)
        {
            var basket=await _basketReposatory.GetBasketAsync(id);
            return Ok(basket ?? new customerBasket(id));


        }
        [HttpPost]
        public async Task<ActionResult<customerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, customerBasket>(basket);
            var createOrUpdateBasket=await _basketReposatory.UpdateBasketAsync(mappedBasket);
            if(createOrUpdateBasket is null) {
                return BadRequest(new ApiResponse(400));

            }
            return Ok(createOrUpdateBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
         await   _basketReposatory.DeleteBasketAsync(id);
        }

    }
}
