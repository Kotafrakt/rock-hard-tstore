using Exchange;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TransactionStore.Business.Services;
using TStore.Business.Exceptions;

namespace TransactionStore.Business.Tests
{
    public class ConverterServiceTests
    {
        private ConverterService _sut;
        private Mock<ICurrencyRatesService> _currencyRatesServiceMock;

        [SetUp]
        public void Setup()
        {
            _currencyRatesServiceMock = new Mock<ICurrencyRatesService>();

            _currencyRatesServiceMock.SetupGet(a => a.RatesModel).Returns(new Exchange.RatesExchangeModel()
            {
                Updated = "11.09.2021 20:00:09",
                BaseCurrency = "USD",
                Rates = new Dictionary<string, decimal> {
                { "USDRUB", 73.1519m },
                { "USDEUR", 0.84645m },
                { "USDJPY", 109.885m }  }
            });
            _currencyRatesServiceMock.Setup(a => a.LoadCurrencyRates()).Returns(new Exchange.RatesExchangeModel()
            {
                Updated = "11.09.2021 20:00:09",
                BaseCurrency = "USD",
                Rates = new Dictionary<string, decimal> {
                { "USDRUB", 73.1519m },
                { "USDEUR", 0.84645m },
                { "USDJPY", 109.885m }  }
            });

            _sut = new ConverterService(_currencyRatesServiceMock.Object);
        }

        [TestCase("USD")]
        [TestCase("RUB")]
        [TestCase("EUR")]
        [TestCase("JPY")]
        public void ConvertAmount_Currencies_RecipientAmount(string currency)
        {
            //Given
            var baseCurrency = "USD";
            var amount = 10m;
            var currencies = new List<string>() { "USD", "RUB", "EUR", "JPY" };
            currencies.Remove(currency);
            var expectedAmounts = ConverterData.GetValidListOfRecipientAmount(currency, amount);

            //When
            List<decimal> actualAmounts = new();
            foreach (var recipientCurrency in currencies)
            {
                actualAmounts.Add(_sut.ConvertAmount(currency, recipientCurrency, amount));
            }

            //Than
            actualAmounts.Should().BeEquivalentTo(expectedAmounts);
        }

        [Test]
        public void ConvertAmount_NotValidSenderCurrency_CurrencyNotValidException()
        {
            //Given
            var amount = 10m;
            var senderCurrency = "ZZZ";
            var recipientCurrency = "USD";

            Assert.Throws(Is.TypeOf<CurrencyNotValidException>()
               .And.Message.EqualTo("Sender currency is not valid"),
               () => _sut.ConvertAmount(senderCurrency, recipientCurrency, amount));
        }

        [Test]
        public void ConvertAmount_NotValidRecipientCurrency_CurrencyNotValidException()
        {
            //Given
            var amount = 10m;
            var senderCurrency = "USD";
            var recipientCurrency = "ZZZ";

            Assert.Throws(Is.TypeOf<CurrencyNotValidException>()
               .And.Message.EqualTo("Recipient currency is not valid"),
               () => _sut.ConvertAmount(senderCurrency, recipientCurrency, amount));
        }

        [Test]
        public void ConvertAmount_NotCurrencyRates_CurrencyNotValidException()
        {
            //Given
            var amount = 10m;
            var senderCurrency = "USD";
            var recipientCurrency = "RUB";

            _currencyRatesServiceMock.Setup(a => a.LoadCurrencyRates()).Returns<RatesExchangeModel>(default);

            Assert.Throws(Is.TypeOf<CurrencyRatesNotFoundException>()
               .And.Message.EqualTo("There are no current Currency Rates"),
               () => _sut.ConvertAmount(senderCurrency, recipientCurrency, amount));
        }
    }
}