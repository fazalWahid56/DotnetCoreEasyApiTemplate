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
using App.Services.GeneralLeadger;

namespace App.Services.Tests
{
    [TestClass]
    public class VoucherServiceTests
    {
        private static IMapper _mapper;
        private Mock<IVoucherRepository> _vouchertMock { get; set; }
        private Mock<IVoucherTypeRepository> _voucherTypeMock { get; set; }
        private Voucher _vouchert;
        private VoucherDTO _vouchertDTO;
        private VoucherType _voucherType;
        private VoucherTypeDTO _voucherTypeDTO;

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

            _voucherType = new VoucherType() { VoucherTypeId = 1, Desciption = "Cash" };
            _voucherTypeDTO= new VoucherTypeDTO() { VoucherTypeId = 1, Desciption = "Cash" };
            //Mock VoucherRepositor 
            _voucherTypeMock = new Mock<IVoucherTypeRepository>();
            _voucherTypeMock.Setup(voucher => voucher.CreateAsync(It.IsAny<VoucherType>()))
                .Returns(Task.FromResult(_voucherType));
            _voucherTypeMock.Setup(voucher => voucher.UpdateAsync(It.IsAny<VoucherType>()))
                .Returns(Task.FromResult(_voucherType));
            _voucherTypeMock.Setup(voucher => voucher.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_vouchertDTO.VoucherId) ? true : throw new KeyNotFoundException());
            _voucherTypeMock.Setup(voucher => voucher.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_voucherTypeDTO.VoucherTypeId) ? _voucherType : throw new KeyNotFoundException());
            _voucherTypeMock.Setup(voucher => voucher.GetAllAsync())
                .Returns(Task.FromResult(new List<VoucherType>() { _voucherType }));

            _vouchert = new Voucher() { VoucherId = 1, VoucherNo = "BBV/Feb/2020", VoucherTypeId=1 };
            _vouchertDTO = new VoucherDTO() { VoucherId = 1, VoucherNo = "BBV/Feb/2020", VoucherTypeId = 1 };
            //Mock VoucherRepositor 
            _vouchertMock = new Mock<IVoucherRepository>();
            _vouchertMock.Setup(voucher => voucher.CreateAsync(It.IsAny<Voucher>()))
                .Returns(Task.FromResult(_vouchert));
            _vouchertMock.Setup(voucher => voucher.UpdateAsync(It.IsAny<Voucher>()))
                .Returns(Task.FromResult(_vouchert));
            _vouchertMock.Setup(voucher => voucher.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_vouchertDTO.VoucherId) ? true : throw new KeyNotFoundException());
            _vouchertMock.Setup(voucher => voucher.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id.Equals(_vouchertDTO.VoucherId) ? _vouchert : throw new KeyNotFoundException());
            _vouchertMock.Setup(voucher => voucher.GetAllAsync())
                .Returns(Task.FromResult(new List<Voucher>() { _vouchert }));

        }

        [TestMethod]
        public async Task VoucherTypeCreateAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object,_mapper);
            //Act
            var result = await voucherService.CreateVoucherTypeAsync(_voucherTypeDTO);
            // Assert
            result.VoucherTypeId.Should().Be(_voucherTypeDTO.VoucherTypeId);
        }

        [TestMethod]
        public async Task VoucherTypeUpdateAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.UpdateVoucherTypeAsync(_voucherTypeDTO);
            // Assert
            result.Should().Be(_voucherTypeDTO);
        }
        [TestMethod]
        public async Task VoucherTypeDeleteAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.DeleteVoucherTypeAsync(1);
            // Assert
            result.Should().Be(true);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task VoucherTypeDeleteNotFoundAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.DeleteVoucherTypeAsync(2);
            // Assert
            result.Should().Be(false);
        }

        [TestMethod]
        public async Task VoucherTypeGetAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.GetVoucherTypeAsync(1);
            // Assert
            result.VoucherTypeId.Should().Be(_voucherTypeDTO.VoucherTypeId);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task VoucherTypeGetNotFoundAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.GetVoucherTypeAsync(2);
            // Assert
            result.Should().Be(false);
        }
        [TestMethod]
        public async Task VoucherTypeGetAllAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.GetAllVouchersAsync();
            // Assert
            result.Count.Should().Be(1);
        }


        #region Voucher

        [TestMethod]
        public async Task VoucherCreateAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.CreateVoucherAsync(_vouchertDTO);
            // Assert
            result.VoucherId.Should().Be(_vouchertDTO.VoucherId);
        }

        [TestMethod]
        public async Task VoucherUpdateAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.UpdateVoucherAsync(_vouchertDTO);
            // Assert
            result.Should().Be(_vouchertDTO);
        }
        [TestMethod]
        public async Task VoucherDeleteAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.DeleteVoucherAsync(1);
            // Assert
            result.Should().Be(true);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task VoucherDeleteNotFoundAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.DeleteVoucherAsync(2);
            // Assert
            result.Should().Be(false);
        }

        [TestMethod]
        public async Task VoucherGetAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.GetVoucherAsync(1);
            // Assert
            result.VoucherId.Should().Be(_vouchert.VoucherId);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "key not found")]
        public async Task VoucherGetNotFoundAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.GetVoucherAsync(2);
            // Assert
            result.Should().Be(false);
        }
        [TestMethod]
        public async Task VoucherGetAllAsync()
        {
            VoucherService voucherService = new VoucherService(_vouchertMock.Object, _voucherTypeMock.Object, _mapper);
            //Act
            var result = await voucherService.GetAllVouchersAsync();
            // Assert
            result.Count.Should().Be(1);
        }
    }
    #endregion
}
