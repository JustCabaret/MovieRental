using System.ComponentModel.DataAnnotations;
using MovieRental.Rental;

namespace MovieRental.Customer
{
	public class Customer
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		// One customer can have many rentals
		public ICollection<Rental.Rental> Rentals { get; set; }    
	}
}