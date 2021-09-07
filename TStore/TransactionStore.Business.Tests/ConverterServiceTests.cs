using Moq;
using NUnit.Framework;
using TransactionStore.Business.Services;
using TransactionStore.DAL.Repositories;
using FluentAssertions;
using TransactionStore.Core.Enums;
using System;
using System.Collections.Generic;

namespace TransactionStore.Business.Tests
{
    public class ConverterServiceTests
    {
        private ConverterService _sut;


        [SetUp]
        public void Setup()
        {
            _sut = new ConverterService(new CurrencyRatesService());
        }

        [TestCase("USD")]
        [TestCase("RUB")]
        [TestCase("EUR")]
        [TestCase("JPY")]
        public void ConvertAmount_Currencies_RecipientAmount(string currency)
        {
            //Given
            var amount = 10m;
            var currencies = new List<string>() { "USD", "RUB", "EUR", "JPY" };
            currencies.Remove(currency);
            var expectedAmounts = new List<decimal>();
            expectedAmounts = ConverterData.GetValidListOfRecipientAmount(currency, amount);

            //When
            List<decimal> actualAmounts = new ();
            foreach (var recipientCurrency in currencies)
            {
                actualAmounts.Add(_sut.ConvertAmount(currency, recipientCurrency, amount));
            }
               
            //Than
            actualAmounts.Should().BeEquivalentTo(expectedAmounts);
        }
    }
}