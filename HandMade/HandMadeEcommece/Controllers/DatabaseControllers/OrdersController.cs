using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HandMadeEcommece.Models.Dto;
using System.Linq.Expressions;
using HandMadeEcommece.Models.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace HandMadeEcommece.Controllers.ModelsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : GenersController<Order,OrderDto>
    {
        private readonly IMapper _Mapper;
        public OrdersController(AppDbContext Context,IMapper mapper):base(Context)
        { 
            _Mapper = mapper;
        }

        protected override Order SetData(OrderDto dto)
        {
            var order = _Mapper.Map<Order>(dto);
            return order;
        }

        protected override Order Update(Order entity, OrderDto dto)
        {
           var order =  _Mapper.Map(dto, entity);
            return order;
        }

    }
}
