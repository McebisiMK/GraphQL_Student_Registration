using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Services;
using Registration.Utilities.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Tests.Queries
{
    [TestFixture]
    public class AddressServiceTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task GetByStreet_Given_Invalid_Street_Name_Should_Throw_Exception(string streetName)
        {
            //-----------------------Arrange----------------------------------
            var addressRepository = Substitute.For<IAddressRepository>();
            AddressService addressService = CreateAddressService(addressRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserInputException>(() => addressService.GetByStreet(streetName));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be($"Invalid given user input: {streetName}");
            await addressRepository.Received(0).GetByStreet(streetName);
        }

        [Test]
        public async Task GetByStreet_Given_Valid_Street_Name_Should_Return_List_Of_Address_Details()
        {
            //-----------------------Arrange----------------------------------
            var streetName = "Street";
            var addressList = new List<Address>
            {
                new Address
                {
                    Unit = "Unit 1",
                    Street = "Street",
                    Town = "Town",
                    Province = "Province"
                },
                new Address
                {
                    Unit = "Unit 2",
                    Street = "Street",
                    Town = "Town",
                    Province = "Province"
                }
            };
            var addressRepository = Substitute.For<IAddressRepository>();
            var addressService = CreateAddressService(addressRepository);

            //-----------------------Act--------------------------------------
            addressRepository.GetByStreet(streetName).Returns(addressList);
            var actual = await addressService.GetByStreet(streetName);

            //-----------------------Assert-----------------------------------
            actual.Should().BeEquivalentTo(addressList);
            await addressRepository.Received(1).GetByStreet(streetName);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-45)]
        public async Task GetById_Given_Invalid_Address_Id_Should_Throw_Exception(int addressId)
        {
            //-----------------------Arrange-----------------------------------
            var addressRepository = Substitute.For<IAddressRepository>();
            var addressService = CreateAddressService(addressRepository);

            //-----------------------Act---------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserInputException>(() => addressService.GetById(addressId));

            //-----------------------Assert------------------------------------
            exception.Message.Should().Be($"Invalid given user input: {addressId}");
            await addressRepository.Received(0).GetById(addressId);
        }

        [Test]
        public async Task GetById_Given_Valid_Address_Id_Should_Return_Address_Details()
        {
            //-----------------------Arrange-----------------------------------
            var addressId = 1;
            var address = new Address
                        {
                            Id = 1,
                            Unit = "Unit 1",
                            Street = "Street",
                            Town = "Town",
                            Province = "Province"
                        };
            var addressRepository = Substitute.For<IAddressRepository>();
            var addressService = CreateAddressService(addressRepository);

            //-----------------------Act---------------------------------------
            addressRepository.GetById(addressId).Returns(address);
            var actual = await addressService.GetById(addressId);

            //-----------------------Assert------------------------------------
            actual.Should().BeEquivalentTo(address);
            await addressRepository.Received(1).GetById(addressId);
        }

        [Test]
        public async Task GetAll_Given_Address_Table_Contains_Data_Should_Return_List_Of_Address_Details()
        {
            //-----------------------Arrange----------------------------------
            var addressList = new List<Address>
            {
                new Address
                {
                    Id = 1,
                    Unit = "Unit 1",
                    Street = "Street",
                    Town = "Town",
                    Province = "Province"
                },
                new Address
                {
                    Id = 2,
                    Unit = "Unit 2",
                    Street = "Street",
                    Town = "Town",
                    Province = "Province"
                }
            };
            var addressRepository = Substitute.For<IAddressRepository>();
            var addressService = CreateAddressService(addressRepository);

            //-----------------------Act--------------------------------------
            addressRepository.GetAll().Returns(addressList);
            var actual = await addressService.GetAll();

            //-----------------------Assert-----------------------------------
            actual.Should().BeEquivalentTo(addressList);
            await addressRepository.Received(1).GetAll();
        }

        private static AddressService CreateAddressService(IAddressRepository addressRepository)
        {
            return new AddressService(addressRepository);
        }
    }
}
