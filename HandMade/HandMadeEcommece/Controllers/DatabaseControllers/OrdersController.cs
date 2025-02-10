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
using System.Collections.Generic;

namespace HandMadeEcommece.Controllers.ModelsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _Context;
        public OrdersController(AppDbContext Context)
        {
            _Context = Context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAll()
        {
            var orders = await _Context.Orders.ToListAsync();
            if (orders == null) return NotFound();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm] OrderDto orderDto)
        {
            if (!ModelState.IsValid || orderDto == null) return BadRequest();
            var carts = await _Context.Carts.Where(e => e.Order.CartId == orderDto.CartId).ToListAsync();
            // admins
            // vendors
            var cartItem = await _Context.CartItems.Where(e => e.CartId == orderDto.CartId).ToListAsync();

            var Qty = 0;
            foreach (var cartitem in cartItem)
            {
                Qty += cartitem.Quantity;
            }



            var products_id = await _Context.CartItems
                .Include(e => e.productVariantItem) 
                .ThenInclude(pvi => pvi.ProductVariant)
                .Where(e => e.CartId == orderDto.CartId)
                .Select(e => new
                {
                    ProductId = e.productVariantItem.ProductVariant.ProductId
                })
                .ToListAsync();


            var list = new List<Product>();
            var vendors = new List<Vendor>();

            foreach (var product_id in products_id)
            {
                var product = await _Context.Products.FindAsync(product_id);
                if (product == null) continue;
                var cart = await _Context.CartItems.Include(e=>e.productVariantItem).ThenInclude(e=>e.ProductVariant)
                    .Where(e=>e.productVariantItem.ProductVariant.ProductId == product.Id).SingleOrDefaultAsync();
                product.Qty -= cart.Quantity;// in cartitem
                var add_product = new Product
                {
                    Id = product.Id,
                    BrandId = product.BrandId,
                    Price = product.Price,
                    Qty = product.Qty,
                    OfferEndDate = product.OfferEndDate,
                    OfferStartDate = product.OfferStartDate,
                    Name = product.Name,
                    Slug = product.Slug,
                    IsApproved = product.IsApproved,
                };
                var vendor = await _Context.Vendors.FindAsync(product.VendorId);
                list.Add(product);
                vendors.Add(vendor);
                _Context.Products.Update(product);
            }
            await _Context.SaveChangesAsync();


            var amount = 0.0m;
            foreach (var cart in carts)
            {
                amount += cart.TotalPrice;
            }


            var order = new Order
            {
                CompanyDeliveryId = orderDto.CompanyDeliveryId,
                Amount = amount,//calucalate coupon and discount
                ProductQty = Qty,
                OrderStatus = orderDto.OrderStatus.ToString(),
                CartId = orderDto.CartId,
                UserId = orderDto.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CurrencyName = orderDto.CurrencyName.ToString(),
                OrderAddress = orderDto.OrderAddress,
                PaymentMethod = orderDto.PaymentMethod.ToString(),
                product = list,
                vendor=vendors
            };
            await _Context.Orders.AddAsync(order);
            await _Context.SaveChangesAsync();
            return Ok(order);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, OrderDto orderDto)
        {
            if (id <= 0 || orderDto == null) return BadRequest();
            var order = await _Context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            var carts = await _Context.Carts.Where(e => e.Order.CartId == orderDto.CartId).ToListAsync();
            var amount = 0.0m;
            foreach (var cart in carts)
            {
                amount += cart.TotalPrice;
            }
            var cartItem = await _Context.CartItems.Where(e => e.CartId == orderDto.CartId).ToListAsync();
            var Qty = 0;
            foreach (var cartitem in cartItem)
            {
                Qty += cartitem.Quantity;
            }

            var products_id = await _Context.CartItems
                .Include(e => e.productVariantItem)
                .ThenInclude(pvi => pvi.ProductVariant)
                .Where(e => e.CartId == orderDto.CartId)
                .Select(e => new
                {
                    ProductId = e.productVariantItem.ProductVariant.ProductId
                })
                .ToListAsync();


            var list = new List<Product>();
            var vendors = new List<Vendor>();

            foreach (var product_id in products_id)
            {
                var product = await _Context.Products.FindAsync(product_id);
                if (product == null) continue;
                var cart = await _Context.CartItems.Include(e => e.productVariantItem).ThenInclude(e => e.ProductVariant)
                    .Where(e => e.productVariantItem.ProductVariant.ProductId == product.Id).SingleOrDefaultAsync();
                product.Qty -= cart.Quantity;// in cartitem
                var add_product = new Product
                {
                    Id = product.Id,
                    BrandId = product.BrandId,
                    Price = product.Price,
                    Qty = product.Qty,
                    OfferEndDate = product.OfferEndDate,
                    OfferStartDate = product.OfferStartDate,
                    Name = product.Name,
                    Slug = product.Slug,
                    IsApproved = product.IsApproved,
                };
                var vendor = await _Context.Vendors.FindAsync(product.VendorId);
                list.Add(product);
                vendors.Add(vendor);
                _Context.Products.Update(product);
            }
            await _Context.SaveChangesAsync();

            order.ProductQty = Qty;
            order.CartId = orderDto.CartId;
            order.UserId = orderDto.UserId;
            order.Amount = amount;
            order.CurrencyName = orderDto.CurrencyName.ToString();
            order.UpdatedAt = DateTime.UtcNow;
            order.CreatedAt = orderDto.CreatedAt;
            order.PaymentMethod = orderDto.PaymentMethod.ToString();
            order.OrderStatus = orderDto.OrderStatus.ToString();
            order.OrderAddress = orderDto.OrderAddress;
            order.CompanyDeliveryId = orderDto.CompanyDeliveryId;
            order.product = list;
            order.vendor = vendors;
            _Context.Orders.Update(order);
            await _Context.SaveChangesAsync();
            return Ok(order);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromQuery] List<int> ids)
        {
            if (ids == null || ids.Count <= 0) return BadRequest();
            var ordes = new List<Order>();
            foreach (var id in ids)
            {
                var order = await _Context.Orders.FindAsync(id);
                if (order == null) continue;
                _Context.Orders.Remove(order);
            }
            await _Context.SaveChangesAsync();
            if (ordes.Count == 0) return NotFound();
            return Ok(ordes);
        }

    }
}
