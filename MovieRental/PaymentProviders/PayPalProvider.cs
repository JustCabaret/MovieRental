namespace MovieRental.PaymentProviders
{
	public class PayPalProvider : IPaymentProvider
	{
		public Task<bool> Pay(double price)
		{
			// Simulate payment processing
			return Task.FromResult(true);
		}
	}
}
