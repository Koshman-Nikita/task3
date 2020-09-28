using System;

namespace task3
{

	class Converter
	{
		public Converter(double usd, double eur)
		{
			exchangeRateUSD = usd;
			exchangeRateEUR = eur;
		}

		public double GetExchangeRateToEUR(double uah)
		{
			return uah / exchangeRateEUR;
		}

		public double GetExchangeRateFromEUR(double eur)
		{
			return eur * exchangeRateEUR;
		}

		public double GetExchangeRateToUSD(double uah)
		{
			return uah / exchangeRateUSD;
		}

		public double GetExchangeRateFromUSD(double usd)
		{
			return usd * exchangeRateUSD;
		}

		private double exchangeRateEUR;
		private double exchangeRateUSD;
	}

	class Program
	{

		static double ReadCurrencyValue()
		{
			string input = Console.ReadLine();

			if (input.Length == 0)
			{
				throw new Exception("Error. Currency value can't be 0.");
			}

			try
			{
				double value = Convert.ToDouble(input);

				if (value > 0)
					return value;

				throw new Exception("Error. Currency value must be positive.");

			}
			catch (Exception ex)
			{
				throw new Exception("Error. Can't extract currency value");
			}

			throw new Exception("Unknown error");
		}

		private delegate double ExchangeFunction(double x);

		static void InteractiveExchange(string from, string to, ExchangeFunction exchange)
		{

			Console.Write(String.Format("Enter amount of {0}: ", from.ToUpper()));
			double value = ReadCurrencyValue();

			Console.WriteLine(String.Format("{0} {1} = {2} {3}", value.ToString("F2"), from, exchange(value).ToString("F2"), to));
			Console.ReadKey();
		}

		static void Main(string[] args)
		{

			Converter currencyConverter = new Converter(0, 0);

			bool hasToUpdateRates = true;

			do
			{
				Console.Clear();

				if (hasToUpdateRates)
				{
					Console.Write("Enter exchange rate for USD: ");
					double exchangeRateUSD = ReadCurrencyValue();

					Console.Write("Enter exchange rate for EUR: ");
					double exchangeRateEUR = ReadCurrencyValue();

					currencyConverter = new Converter(exchangeRateUSD, exchangeRateEUR);


					hasToUpdateRates = false;
					continue;
				}

				Console.WriteLine("Current exchange rates:");
				Console.WriteLine(String.Format("USD to UAH: {0}", currencyConverter.GetExchangeRateFromUSD(1).ToString("F2")));
				Console.WriteLine(String.Format("EUR to UAH: {0}", currencyConverter.GetExchangeRateFromEUR(1).ToString("F2")));

				Console.WriteLine();

				Console.WriteLine("Select option: ");
				Console.WriteLine("Update currency rates (Press 0)");
				Console.WriteLine("Convert UAH to USD (Press 1)");
				Console.WriteLine("Convert UAH to EUR (Press 2)");
				Console.WriteLine("Convert USD to UAH (Press 3)");
				Console.WriteLine("Convert EUR to UAH (Press 4)");
				Console.WriteLine("Exit (Press 5)");

				Console.Write("Enter: ");

				ConsoleKey option = Console.ReadKey().Key;

				Console.WriteLine();

				try
				{
					switch (option)
					{
						case ConsoleKey.D0:
							{
								hasToUpdateRates = true;
								break;
							}
						case ConsoleKey.D1:
							{
								InteractiveExchange("UAH", "USD", currencyConverter.GetExchangeRateToUSD);
								break;
							}
						case ConsoleKey.D2:
							{
								InteractiveExchange("UAH", "EUR", currencyConverter.GetExchangeRateToEUR);
								break;
							}
						case ConsoleKey.D3:
							{
								InteractiveExchange("USD", "UAH", currencyConverter.GetExchangeRateFromUSD);
								break;
							}
						case ConsoleKey.D4:
							{
								InteractiveExchange("EUR", "UAH", currencyConverter.GetExchangeRateFromEUR);
								break;
							}
						case ConsoleKey.D5:
							{
								return;
							}

					}
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception.Message);
					Console.WriteLine("Try again.");
					Console.ReadKey();
				}

			} while (true);
		}
	}
}

