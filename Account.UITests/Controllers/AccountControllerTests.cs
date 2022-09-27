using Microsoft.VisualStudio.TestTools.UnitTesting;
using Account.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Account.Backend;
using Xunit;
using Account.UI.Models;
using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Account.UI.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [Fact]
        [TestMethod()]
        public void DepositTest_ShoudAddBalanceToDataBase_WhenUserDepositAmountToAccount()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var amount = new Amount
                {
                    Id = 1,
                    Balance = 100,
                    WithdrawalAmount = 0,
                    DepositAmount = 100,
                    Date = DateTime.Now
                };
                 

                mock.Mock<IAccountService>()
                    .Setup(x => x.Deposit(amount));
                var mockController = mock.Create<AccountController>();
                mockController.Deposit(amount);
                mock.Mock<IAccountService>()
                    .Verify(x => x.Deposit(amount), Times.Exactly(1));

            }
        }
        [Fact]
        [TestMethod()]
        public void DepositTest_ShouldReturnError_WhenDepositeAmountIsZero()
        {
            try
            {
                using (var mock = AutoMock.GetLoose())
                {
                    var amount = new Amount
                    {
                        Id = 1,
                        Balance = 100,
                        WithdrawalAmount = 0,
                        DepositAmount = 0,
                        Date = DateTime.Now
                    };


                    mock.Mock<IAccountService>()
                        .Setup(x => x.Deposit(amount));
                    var mockController = mock.Create<AccountController>();
                    mockController.Deposit(amount);
                    mock.Mock<IAccountService>()
                        .Verify(x => x.Deposit(amount), Times.Exactly(1));

                }
            }
            catch (ArgumentOutOfRangeException e)
            {

                StringAssert.Contains(e.Message, "Deposite amount can not be Zero(0) or less then Zero(0)");
            }
            
        }
        [Fact]
        [TestMethod()]
        public void WithdrawTest_ShoudDeductBalanceFromDataBase_WhenUserWithdrawAmountFromAccount()
        {
            
            using (var mock = AutoMock.GetLoose())
            {
                var amount = new Amount
                {
                    Id = 1,
                    Balance = 100,
                    WithdrawalAmount = 50,
                    DepositAmount = 0,
                    Date = DateTime.Now
                };

                mock.Mock<IAccountService>()
                    .Setup(x => x.PrintStatement())
                    .Returns(GetSampleAmountDetailsList());
                mock.Mock<IAccountService>()
                    .Setup(x => x.Withdraw(amount));
                var mockController = mock.Create<AccountController>();
                mockController.Withdraw(amount);
                mock.Mock<IAccountService>()
                    .Verify(x => x.Withdraw(amount), Times.Exactly(1));

            }
        }
        [Fact]
        [TestMethod()]
        public void WithdrawTest_ShouldReturnError_WhenWithdrawalAmountIsZero()
        {
            try
            {
                using (var mock = AutoMock.GetLoose())
                {
                    var amount = new Amount
                    {
                        Id = 1,
                        Balance = 100,
                        WithdrawalAmount = 0,
                        DepositAmount = 0,
                        Date = DateTime.Now
                    };

                    mock.Mock<IAccountService>()
                        .Setup(x => x.PrintStatement())
                        .Returns(GetSampleAmountDetailsList());
                    mock.Mock<IAccountService>()
                        .Setup(x => x.Withdraw(amount));
                    var mockController = mock.Create<AccountController>();
                    mockController.Withdraw(amount);
                    mock.Mock<IAccountService>()
                        .Verify(x => x.Withdraw(amount), Times.Exactly(1));

                }
            }
            catch (ArgumentOutOfRangeException e)
            {

                StringAssert.Contains(e.Message, "Withdrawal amount can not be Zero(0) or less then Zero(0)");
            }
            
        }
        [Fact]
        [TestMethod()]
        public void WithdrawTest_ShouldReturnError_WhenWithdrawalAmountIsGreaterThenBalance()
        {
            try
            {
                using (var mock = AutoMock.GetLoose())
                {
                    var amount = new Amount
                    {
                        Id = 1,
                        Balance = 100,
                        WithdrawalAmount = 500,
                        DepositAmount = 0,
                        Date = DateTime.Now
                    };

                    mock.Mock<IAccountService>()
                        .Setup(x => x.PrintStatement())
                        .Returns(GetSampleAmountDetailsList());
                    mock.Mock<IAccountService>()
                        .Setup(x => x.Withdraw(amount));
                    var mockController = mock.Create<AccountController>();
                    mockController.Withdraw(amount);
                    mock.Mock<IAccountService>()
                        .Verify(x => x.Withdraw(amount), Times.Exactly(1));

                }
            }
            catch (ArgumentOutOfRangeException e)
            {

                StringAssert.Contains(e.Message, "Withdrawal amount can not greater then balance amount");
            }

        }
        [Fact]
        [TestMethod()]
        public void PrintStatementTest_SouldReturnValidDataList_WhenFetchDataFromDataBase()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccountService>()
                    .Setup(x => x.PrintStatement())
                    .Returns(GetSampleAmountDetailsList());


                var mockController = mock.Create<AccountController>();
                var expectedResult = GetSampleAmountDetailsList();
                var actualResult = mockController.PrintStatement() as ViewResult;
                var result = (List<Amount>)actualResult.Model;
               
                Assert.IsTrue(actualResult != null);
                Assert.AreEqual(expectedResult.Count, result.Count);
                for (int i = 0; i < expectedResult.Count; i++)
                {
                    Assert.AreEqual(expectedResult[i].Balance, result[i].Balance);
                    Assert.AreEqual(expectedResult[i].WithdrawalAmount, result[i].WithdrawalAmount);
                }
            }
        }
        private List<Amount> GetSampleAmountDetailsList()
        {
            List<Amount> output = new List<Amount>
            {
                new Amount
                {
                    Id = 1,
                    Balance = 100,
                    WithdrawalAmount=0,
                    DepositAmount=100,
                    Date=DateTime.Now
                },
                new Amount
                {
                    Id = 2,
                    Balance = 50,
                    WithdrawalAmount=50,
                    DepositAmount=0,
                    Date=DateTime.Now
                }

            };
            return output;
        }
 
    }
}