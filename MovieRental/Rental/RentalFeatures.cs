using Microsoft.EntityFrameworkCore;
using MovieRental.Data;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public RentalFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}

		// Async method to avoid blocking thread during database I/O
		public async Task<Rental> SaveAsync(Rental rental)
		{
			_movieRentalDb.Rentals.Add(rental);
			await _movieRentalDb.SaveChangesAsync();
			return rental;
		}

		// Query rentals by customer name with related entities loaded
		public async Task<IEnumerable<Rental>> GetRentalsByCustomerNameAsync(string customerName)
		{
			return await _movieRentalDb.Rentals
				.Include(r => r.Customer)
				.Include(r => r.Movie)
				.Where(r => r.Customer != null && r.Customer.Name == customerName)
				.ToListAsync();
		}

	}
}
