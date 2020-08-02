using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Services;
using Registration.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Tests.Mutations
{
    [TestFixture]
    public class AddressServiceTests
    {
        [TestCase("", "", "", "")]
        [TestCase(" ", " ", " ", " ")]
        [TestCase(null, null, null, null)]
        public async Task Add_Given_One_Or_More_Address_Properties_Are_Invalid_Should_Exception(string unit, string street, string town, string province)
        {
            //-----------------------Arrange----------------------------------
            var address = GetAddress();
            address.Unit = unit;
            address.Street = street;
            address.Town = town;
            address.Province = province;
            var addressRepository = Substitute.For<IAddressRepository>();
            var addressService = CreateAddressService(addressRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserObject>(() => addressService.Add(address));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Invalid given object: Address");
            await addressRepository.Received(0).Add(address);
        }

        [Test]
        public async Task Add_Given_Valid_Address_Should_Return_Newly_Created_Record()
        {
            //-----------------------Arrange----------------------------------
            var address = GetAddress();
            var addressRepository = Substitute.For<IAddressRepository>();
            var addressService = CreateAddressService(addressRepository);

            //-----------------------Act--------------------------------------
            addressRepository.Add(address).Returns(1);
            address.Id = 1;
            addressRepository.GetById(1).Returns(address);
            await addressService.Add(address);

            //-----------------------Assert-----------------------------------;
            await addressRepository.Received(1).Add(address);
            address.Id.Should().BeGreaterThan(0);
        }

        private Address GetAddress()
        {
            return new Address
            {
                Unit = "Unit 1",
                Street = "Street",
                Town = "Town",
                Province = "Province"
            };
        }

        private AddressService CreateAddressService(IAddressRepository addressRepository)
        {
            return new AddressService(addressRepository);
        }
    }
}
