namespace MovieRental.PaymentProviders
{
	public class PaymentProviderFactory
	{
		// Factory method to instantiate payment providers based on method name
		public IPaymentProvider GetProvider(string paymentMethod)
		{
			return paymentMethod switch
			{
				"MBWay" => new MbWayProvider(),
				"PayPal" => new PayPalProvider(),
				_ => throw new ArgumentException($"Unknown payment method: {paymentMethod}")
			};
		}
	}
}
