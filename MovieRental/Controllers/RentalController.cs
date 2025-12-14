using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rental.Rental rental)
        {
	        return Ok(await _features.SaveAsync(rental));
        }

        // Get all rentals for a specific customer
        [HttpGet("customer/{customerName}")]
        public async Task<IActionResult> GetByCustomerName(string customerName)
        {
            var rentals = await _features.GetRentalsByCustomerNameAsync(customerName);
            return Ok(rentals);
        }

	}
}
