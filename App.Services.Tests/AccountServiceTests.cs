using App.Db.Repositories;
using App.Db.Tables;
using App.Entites.DTO;
using App.Services.AutoMapper;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using System.Collections.Generic;

namespace App.Services.Tests
{
    [TestClass]
    public class AccountServiceTests
    {
        private static IMapper _mapper;
        private Mock<IChartOfAccountRepository> _chartOfAccountMock { get; set; }
        private Mock<IAccountNatureRepository> _accountNatureMock { get; set; }
        private ChartOfAccount _chartOfAccount;
        private AccountDTO _chartOfAccountDTO;
        private AccountNature _accountNature;
        private AccountNatureDTO _accountNatureDTO;

        [TestInitialize]
        public void SetUp()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _accountNature = new AccountNature() { AccountNatureId = 1, Description = "Cash" };
            _accountNatureDTO= new AccountNatureDTO() { AccountNatureId = 1, Description = "Cash" };
            //Mock ChartOfAccountRepositor 
            _accountNatureMock = new Mock<IAccountNatureRepository>();
            _accountNatureMock.Setup(account => account.CreateAsync(It.IsAny<AccountNature>()))
                .Returns(Task.FromResult(_accountNature));
            _accountNatureMock.Setup(account => account.UpdateAsync(It.IsAny<AccountNature>()))
                .Returns(Task.FromResult(_accountNature));
            _accountNatureMock.Setup(account => account.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_chartOfAccountDTO.AccountId) ? true : throw new KeyNotFoundException());
            _accountNatureMock.Setup(account => account.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_accountNatureDTO.AccountNatureId) ? _accountNature : throw new KeyNotFoundException());
            _accountNatureMock.Setup(account => account.GetAllAsync())
                .Returns(Task.FromResult(new List<AccountNature>() { _accountNature }));

            _chartOfAccount = new ChartOfAccount() { AccountId = 1, AccountDescription = "Cash", AccountNatureId=1 };
            _chartOfAccountDTO = new AccountDTO() { AccountId = 1, AccountDescription = "Cash" , AccountNatureId = 1 };
            //Mock ChartOfAccountRepositor 
            _chartOfAccountMock = new Mock<IChartOfAccountRepository>();
            _chartOfAccountMock.Setup(account => account.CreateAsync(It.IsAny<ChartOfAccount>()))
                .Returns(Task.FromResult(_chartOfAccount));
            _chartOfAccountMock.Setup(account => account.UpdateAsync(It.IsAny<ChartOfAccount>()))
                .Returns(Task.FromResult(_chartOfAccount));
            _chartOfAccountMock.Setup(account => account.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_chartOfAccountDTO.AccountId) ? true : throw new KeyNotFoundException());
            _chartOfAccountMock.Setup(account => account.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_chartOfAccountDTO.AccountId) ? _chartOfAccount : throw new KeyNotFoundException());
            _chartOfAccountMock.Setup(account => account.GetAllAsync())
                .Returns(Task.FromResult(new List<ChartOfAccount>() { _chartOfAccount }));

        }

        [TestMethod]
        public async Task AccountNatureCreateAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object,_mapper);
            //Act
            var result = await accountService.CreateAccountNatureAsync(_accountNatureDTO);
            // Assert
            result.AccountNatureId.Should().Be(_accountNatureDTO.AccountNatureId);
        }

        [TestMethod]
        public async Task AccountNatureUpdateAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.UpdateAccountNatureAsync(_accountNatureDTO);
            // Assert
            result.Should().Be(_accountNatureDTO);
        }
        [TestMethod]
        public async Task AccountNatureDeleteAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.DeleteAccountNatureAsync(1);
            // Assert
            result.Should().Be(true);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task AccountNatureDeleteNotFoundAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.DeleteAccountNatureAsync(2);
            // Assert
            result.Should().Be(false);
        }

        [TestMethod]
        public async Task AccountNatureGetAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.GetAccountNatureAsync(1);
            // Assert
            result.AccountNatureId.Should().Be(_accountNatureDTO.AccountNatureId);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task AccountNatureGetNotFoundAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.GetAccountNatureAsync(2);
            // Assert
            result.Should().Be(false);
        }
        [TestMethod]
        public async Task AccountNatureGetAllAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.GetAllAccountsAsync();
            // Assert
            result.Count.Should().Be(1);
        }


        #region ChartOfAccount

        [TestMethod]
        public async Task ChartOfAccountCreateAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.CreateAccountAsync(_chartOfAccountDTO);
            // Assert
            result.AccountId.Should().Be(_chartOfAccountDTO.AccountId);
        }

        [TestMethod]
        public async Task ChartOfAccountUpdateAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.UpdateAccountAsync(_chartOfAccountDTO);
            // Assert
            result.Should().Be(_chartOfAccountDTO);
        }
        [TestMethod]
        public async Task ChartOfAccountDeleteAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.DeleteAccountAsync(1);
            // Assert
            result.Should().Be(true);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task ChartOfAccountDeleteNotFoundAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.DeleteAccountAsync(2);
            // Assert
            result.Should().Be(false);
        }

        [TestMethod]
        public async Task ChartOfAccountGetAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.GetAccountAsync(1);
            // Assert
            result.AccountId.Should().Be(_chartOfAccount.AccountId);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task ChartOfAccountGetNotFoundAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.GetAccountAsync(2);
            // Assert
            result.Should().Be(false);
        }
        [TestMethod]
        public async Task ChartOfAccountGetAllAsync()
        {
            AccountService accountService = new AccountService(_chartOfAccountMock.Object, _accountNatureMock.Object, _mapper);
            //Act
            var result = await accountService.GetAllAccountsAsync();
            // Assert
            result.Count.Should().Be(1);
        }
    }
    #endregion
}
