using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TransactionStore.Business.Services;
using TransactionStore.DAL.Repositories;

namespace TransactionStore.Business.Tests
{
    public class TransactionServiceTests
    {
        private TransactionService _sut;
        private Mock<ITransactionRepository> _transactionRepoMock;
        private Mock<ICurrencyRatesService> _currencyRatesServiceMock;
        private Mock<IConverterService> _converterRatesServiceMock;


        [SetUp]
        public void Setup()
        {
            _transactionRepoMock = new Mock<ITransactionRepository>();
            _currencyRatesServiceMock = new Mock<ICurrencyRatesService>();
            _converterRatesServiceMock = new Mock<IConverterService>();
            _sut = new TransactionService(_transactionRepoMock.Object, _converterRatesServiceMock.Object);
        }

        [Test]
        public void AddDeposit_TransactionDto_DepositCreated()
        {
            //Given
            var dto = TransactionStoreData.GetDeposit();

            _transactionRepoMock.Setup(x => x.AddDepositAsync(dto)).ReturnsAsync(dto.Id);

            //When
            var actual = _sut.AddDepositAsync(dto).Result;

            //Than
            dto.Id.Should().Be(actual);
            _transactionRepoMock.Verify(x => x.AddDepositAsync(dto), Times.Once);
        }

        [Test]
        public void AddWithdraw_TransactionDto_WithdrawCreated()
        {
            //Given
            var dto = TransactionStoreData.GetWithdraw();
            

            _transactionRepoMock.Setup(x => x.AddWithdrawAsync(dto)).ReturnsAsync(dto.Id);

            //When
            var actual = _sut.AddWithdrawAsync(dto).Result;

            //Than
            dto.Id.Should().Be(actual);
            _transactionRepoMock.Verify(x => x.AddWithdrawAsync(dto), Times.Once);
        }

        [Test]
        public void AddTransfer_TransferDto_TransferCreated()
        {
            //Given
            var dto = TransactionStoreData.GetTransfer();
            var ids = (dto.AccountId, dto.RecipientAccountId);
            var expectedList = new List<long>();
            expectedList.Add(dto.AccountId);
            expectedList.Add(dto.RecipientAccountId);

            _transactionRepoMock.Setup(x => x.AddTransferAsync(dto)).ReturnsAsync(ids);

            //When
            var actual = _sut.AddTransferAsync(dto).Result;

            //Than
            expectedList.Should().BeEquivalentTo(actual);
            _transactionRepoMock.Verify(x => x.AddTransferAsync(dto), Times.Once);
        }

        [Test]
        public void GetTransactionsByAccountId_AccountId_ReturnedTransactionDtos()
        {
            //Given
            var accountId = 1;
            var dtos = TransactionStoreData.GetListOfTransactions();
            var resultDtos = TransactionStoreData.GetSameListOfTransactionsWithTransfersByAccountIdEqualOne();
            var jsonResultDtos = JsonConvert.SerializeObject(resultDtos);

            _transactionRepoMock.Setup(x => x.GetTransactionsByAccountIdAsync(accountId)).ReturnsAsync(dtos);

            //When
            var actual = _sut.GetTransactionsByAccountIdAsync(accountId).Result;

            //Than
            jsonResultDtos.Should().BeEquivalentTo(actual);
            _transactionRepoMock.Verify(x => x.GetTransactionsByAccountIdAsync(accountId), Times.Once);
        }

        [Test]
        public void GetTransactionsByPeriod_AccountId_ReturnedTransactionDtos()
        {
            //Given
            var dtos = TransactionStoreData.GetListOfTransactions();
            var resultDtos = TransactionStoreData.GetSameListOfTransactionsWithTransfersByAccountIdEqualOne();
            var jsonResultDtos = JsonConvert.SerializeObject(resultDtos);
            var getByPeriodDto = TransactionStoreData.GetByPeriodDtoWithAccountIdEqualOne();
            var leadId = "1";

            _transactionRepoMock.Setup(x => x.GetTransactionsByPeriodAsync(getByPeriodDto.From, getByPeriodDto.To, getByPeriodDto.AccountId)).ReturnsAsync(dtos);

            //When
            var actual = _sut.GetTransactionsByPeriodAsync(getByPeriodDto, leadId).Result;

            //Than
            jsonResultDtos.Should().BeEquivalentTo(actual);
            _transactionRepoMock.Verify(x => x.GetTransactionsByPeriodAsync(getByPeriodDto.From, getByPeriodDto.To, getByPeriodDto.AccountId), Times.Once);
        }

        [Test]
        public void GetTransactionsByPeriod_AccountIdIsNull_ReturnedTransactionDtos()
        {
            //Given
            var dtos = TransactionStoreData.GetListOfTransactions();
            var resultDtos = TransactionStoreData.GetSameListOfTransactionsWithTransfersByAccountIdIsNull();
            var jsonResultDtos = JsonConvert.SerializeObject(resultDtos);
            var getByPeriodDto = TransactionStoreData.GetByPeriodDtoWithAccountIdEqualNull();
            var leadId = "1";

            _transactionRepoMock.Setup(x => x.GetTransactionsByPeriodAsync(getByPeriodDto.From, getByPeriodDto.To, getByPeriodDto.AccountId)).ReturnsAsync(dtos);

            //When
            var actual = _sut.GetTransactionsByPeriodAsync(getByPeriodDto, leadId).Result;

            //Than
            jsonResultDtos.Should().BeEquivalentTo(actual);
            _transactionRepoMock.Verify(x => x.GetTransactionsByPeriodAsync(getByPeriodDto.From, getByPeriodDto.To, getByPeriodDto.AccountId), Times.Once);
        }
    }
}