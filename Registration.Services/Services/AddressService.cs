using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Contracts;
using Registration.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> Add(Address address)
        {
            if (!Valid(address))
                throw new InvalidUserObject("Address");

            return await _addressRepository.Add(address);
        }

        public async Task<Address> Update(int id, Address newAddress)
        {
            var addressExists = _addressRepository.Exists(address => address.Id.Equals(id));

            if (!Valid(newAddress) && !addressExists)
                throw new InvalidUserObject("Address");

            var existingAddress = await _addressRepository.GetById(id);

            return await _addressRepository.Update(existingAddress, newAddress);
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _addressRepository.GetAll();
        }

        public async Task<Address> GetById(int id)
        {
            if (id <= 0)
                throw new InvalidUserInputException(id.ToString());

            return await _addressRepository.GetById(id);
        }

        public async Task<IEnumerable<Address>> GetByStreet(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidUserInputException(name);

            return await _addressRepository.GetByStreet(name);
        }

        private bool Valid(Address address)
        {
            return
                (
                    IsValid(address.Unit) &&
                    IsValid(address.Street) &&
                    IsValid(address.Town) &&
                    IsValid(address.Province)
                );
        }

        private bool IsValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
