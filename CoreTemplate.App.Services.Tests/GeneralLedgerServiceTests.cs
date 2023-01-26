using CoreTemplate.App.Db.Repositories;
using CoreTemplate.App.Db.Tables;
using CoreTemplate.App.Entites.DTO;
using CoreTemplate.App.Services.AutoMapper;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using System.Collections.Generic;
using CoreTemplate.App.Services.GeneralLeadger;

namespace CoreTemplate.App.Services.Tests
{
    [TestClass]
    public class GeneralLedgerServiceTests
    {
        private static IMapper _mapper;
        private Mock<IGeneralLedgerRepository> _generalLedgerMock { get; set; }
        private GeneralLedger _generalLedger;
        private GeneralLedgerDTO _generalLedgerDTO;

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

            _generalLedger = new GeneralLedger() { TransectionId = 1, AgainstAccountId = 1 };
            _generalLedgerDTO = new GeneralLedgerDTO() { TransectionId = 1, AgainstAccountId = 1 };
            //Mock GeneralLedgerRepositor 
            _generalLedgerMock = new Mock<IGeneralLedgerRepository>();
            _generalLedgerMock.Setup(generalLedger => generalLedger.CreateAsync(It.IsAny<GeneralLedger>()))
                .Returns(Task.FromResult(_generalLedger));
            _generalLedgerMock.Setup(generalLedger => generalLedger.UpdateAsync(It.IsAny<GeneralLedger>()))
                .Returns(Task.FromResult(_generalLedger));
            _generalLedgerMock.Setup(generalLedger => generalLedger.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_generalLedgerDTO.TransectionId) ? true : throw new KeyNotFoundException());
            _generalLedgerMock.Setup(generalLedger => generalLedger.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_generalLedgerDTO.TransectionId) ? _generalLedger : throw new KeyNotFoundException());
            _generalLedgerMock.Setup(generalLedger => generalLedger.GetAllAsync())
                .Returns(Task.FromResult(new List<GeneralLedger>() { _generalLedger }));


        }

        [TestMethod]
        public async Task GeneralLedgerCreateAsync()
        {
            GeneralLedgerService generalLedgerService = new GeneralLedgerService(_generalLedgerMock.Object, _mapper);
            //Act
            var result = await generalLedgerService.CreateGeneralLedgerAsync(_generalLedgerDTO);
            // Assert
            result.TransectionId.Should().Be(_generalLedgerDTO.TransectionId);
        }

        [TestMethod]
        public async Task GeneralLedgerUpdateAsync()
        {
            GeneralLedgerService generalLedgerService = new GeneralLedgerService(_generalLedgerMock.Object, _mapper);
            //Act
            var result = await generalLedgerService.UpdateGeneralLedgerAsync(_generalLedgerDTO);
            // Assert
            result.Should().Be(_generalLedgerDTO);
        }
        [TestMethod]
        public async Task GeneralLedgerDeleteAsync()
        {
            GeneralLedgerService generalLedgerService = new GeneralLedgerService(_generalLedgerMock.Object, _mapper);
            //Act
            var result = await generalLedgerService.DeleteGeneralLedgerAsync(1);
            // Assert
            result.Should().Be(true);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task GeneralLedgerDeleteNotFoundAsync()
        {
            GeneralLedgerService generalLedgerService = new GeneralLedgerService(_generalLedgerMock.Object, _mapper);
            //Act
            var result = await generalLedgerService.DeleteGeneralLedgerAsync(2);
            // Assert
            result.Should().Be(false);
        }

        [TestMethod]
        public async Task GeneralLedgerGetAsync()
        {
            GeneralLedgerService generalLedgerService = new GeneralLedgerService(_generalLedgerMock.Object, _mapper);
            //Act
            var result = await generalLedgerService.GetGeneralLedgerAsync(1);
            // Assert
            result.TransectionId.Should().Be(_generalLedgerDTO.TransectionId);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task GeneralLedgerGetNotFoundAsync()
        {
            GeneralLedgerService generalLedgerService = new GeneralLedgerService(_generalLedgerMock.Object, _mapper);
            //Act
            var result = await generalLedgerService.GetGeneralLedgerAsync(2);
            // Assert
            result.Should().Be(false);
        }
        [TestMethod]
        public async Task GeneralLedgerGetAllAsync()
        {
            GeneralLedgerService generalLedgerService = new GeneralLedgerService(_generalLedgerMock.Object, _mapper);
            //Act
            var result = await generalLedgerService.GetAllGeneralLedgersAsync();
            // Assert
            result.Count.Should().Be(1);
        }

    }
       
}
