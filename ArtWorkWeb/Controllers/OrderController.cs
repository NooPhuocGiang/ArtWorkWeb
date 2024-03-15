using DataTier.Models;
using Eatera.Helpers;
using Eatera.Models;
using Login_Signup.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Login_Signup.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [DisableCors]
    public class OrderController : Controller
    {
        private MainDataContext _mainDataContext;
        public OrderController(MainDataContext dataContext)
        {
            _mainDataContext = dataContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _mainDataContext.Orders.ToListAsync();

                return Ok(new { data = orders });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestModel request)
        {
            try
            {

                List<Order> orderList = new List<Order>();

                foreach (var artId in request.ArtIDList)
                {
                    var order = new Order
                    {
                        IdArtwork = artId,
                        CustomerName = request.CustomerName,
                        CustomerNumber = request.CustomerNumber,
                        CustomerEmail = request.CustomerEmail,
                        CustomerAddress = request.CustomerAddress
                    };

                    _mainDataContext.Orders.Add(order);
                    await _mainDataContext.SaveChangesAsync();

                    orderList.Add(order);
                }

                return Ok(new { data = orderList });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromQuery] int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("No id specified");
                }

                Console.WriteLine($"{DateTime.Now} DELETE /api/orders... id={id}");

                var order = await _mainDataContext.Orders.FirstOrDefaultAsync(o => o.IdArtwork == id);

                if (order == null)
                {
                    return NotFound("Order not found");
                }

                _mainDataContext.Orders.Remove(order);
                await _mainDataContext.SaveChangesAsync();

                return Ok(new { data = order });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

}

