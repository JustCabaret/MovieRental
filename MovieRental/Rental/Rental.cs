using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieRental.Customer;

namespace MovieRental.Rental
{
	public class Rental
	{
		[Key]
		public int Id { get; set; }
		public int DaysRented { get; set; }
		public Movie.Movie? Movie { get; set; }

		[ForeignKey("Movie")]
		public int MovieId { get; set; }

		public string PaymentMethod { get; set; }

		//FK to customer
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }

		//One rental belongs to one customer
		public Customer.Customer? Customer { get; set; }
	}
}
