using Moq;
using NUnit.Framework;
using TransactionStore.Business.Services;
using TransactionStore.DAL.Repositories;
using FluentAssertions;
using System.Collections.Generic;

namespace TransactionStore.Business.Tests
{
    public class TransactionServiceTests
    {
        private TransactionService _sut;
        private Mock<ITransactionRepository> _transactionRepoMock;
        private Mock<ICurrencyRatesService> _currencyRatesServiceMock;
        private Mock<IConverterService> _converterRatesServicMock;


        [SetUp]
        public void Setup()
        {
            _transactionRepoMock = new Mock<ITransactionRepository>();
            _currencyRatesServiceMock = new Mock<ICurrencyRatesService>();
            _converterRatesServicMock = new Mock<IConverterService>();
            _sut = new TransactionService(_transactionRepoMock.Object, _converterRatesServicMock.Object);
        }

        [Test]
        public void AddDeposit_TransactionDto_DepositCreated()
        {
            //Given
            var dto = TransactionStoreData.GetDeposit();

            _transactionRepoMock.Setup(x => x.AddDepositeOrWithdraw(dto)).Returns(dto.Id);

            //When
            var actual = _sut.AddDeposit(dto);

            //Than
            dto.Id.Should().Be(actual);
            _transactionRepoMock.Verify(x => x.AddDepositeOrWithdraw(dto), Times.Once);
        }

        [Test]
        public void AddWithdraw_TransactionDto_WithdrawCreated()
        {
            //Given
            var dto = TransactionStoreData.GetWithdraw();

            _transactionRepoMock.Setup(x => x.AddDepositeOrWithdraw(dto)).Returns(dto.Id);

            //When
            var actual = _sut.AddWithdraw(dto);

            //Than
            dto.Id.Should().Be(actual);
            _transactionRepoMock.Verify(x => x.AddDepositeOrWithdraw(dto), Times.Once);
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

            _transactionRepoMock.Setup(x => x.AddTransfer(dto)).Returns(ids);

            //When
            var actual = _sut.AddTransfer(dto);

            //Than
            expectedList.Should().BeEquivalentTo(actual);
            _transactionRepoMock.Verify(x => x.AddTransfer(dto), Times.Once);
        }

        [Test]
        public void GetTransactionsByAccountId_AccountId_ReturnedTransactionDtos()
        {
            //Given
            var accountId = 1;
            var dtos = TransactionStoreData.GetListOfTransactions();
            var resultDtos = TransactionStoreData.GetSameListOfTransactionsWithTransfersByAccountIdEqualOne();

            _transactionRepoMock.Setup(x => x.GetTransactionsByAccountId(accountId)).Returns(dtos);

            //When
            var actual = _sut.GetTransactionsByAccountId(accountId);

            //Than
            resultDtos.Should().BeEquivalentTo(actual);
            _transactionRepoMock.Verify(x => x.GetTransactionsByAccountId(accountId), Times.Once);
        }

        [Test]
        public void GetTransactionsByPeriod_AccountIdIsNull_ReturnedTransactionDtos()
        {
            //Given
            var dtos = TransactionStoreData.GetListOfTransactions();
            var resultDtos = TransactionStoreData.GetSameListOfTransactionsWithTransfersByAccountIdIsNull();
            var getByPeriodDto = TransactionStoreData.GetByPeriodDtoWithAccountIdEqualNull();

            _transactionRepoMock.Setup(x => x.GetTransactionsByPeriod(getByPeriodDto.From, getByPeriodDto.To, getByPeriodDto.AccountId)).Returns(dtos);

            //When
            var actual = _sut.GetTransactionsByPeriod(getByPeriodDto);

            //Than
            resultDtos.Should().BeEquivalentTo(actual);
            _transactionRepoMock.Verify(x => x.GetTransactionsByPeriod(getByPeriodDto.From, getByPeriodDto.To, getByPeriodDto.AccountId), Times.Once);
        }

        [Test]
        public void GetTransactionsByPeriod_AccountId_ReturnedTransactionDtos()
        {
            //Given
            var dtos = TransactionStoreData.GetListOfTransactions();
            var resultDtos = TransactionStoreData.GetSameListOfTransactionsWithTransfersByAccountIdEqualOne();
            var getByPeriodDto = TransactionStoreData.GetByPeriodDtoWithAccountIdEqualOne();

            _transactionRepoMock.Setup(x => x.GetTransactionsByPeriod(getByPeriodDto.From, getByPeriodDto.To, getByPeriodDto.AccountId)).Returns(dtos);

            //When
            var actual = _sut.GetTransactionsByPeriod(getByPeriodDto);

            //Than
            resultDtos.Should().BeEquivalentTo(actual);
            _transactionRepoMock.Verify(x => x.GetTransactionsByPeriod(getByPeriodDto.From, getByPeriodDto.To, getByPeriodDto.AccountId), Times.Once);
        }
    }
}