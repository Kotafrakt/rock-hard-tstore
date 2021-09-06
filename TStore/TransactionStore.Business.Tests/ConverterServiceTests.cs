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

        [Test]
        public void ConvertAmount_Currencies_RecipientAmount()
        {
            //Given
            var amount = 10m;
            var currencies = new List<string>() { "USD", "EUR", "JPY" };
            var recipientCurrency = "RUB";
            var expectedAmounts = ConverterData.GetValidListOfRubsAmounts;

            //When
            List<decimal> actualAmounts = new ();
            foreach (var currency in currencies)
                actualAmounts.Add(_sut.ConvertAmount(currency, recipientCurrency, amount));

            //Than
            actualAmounts.Should().BeEquivalentTo(expectedAmounts);
        }
    }
}