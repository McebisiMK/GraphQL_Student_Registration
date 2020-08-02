using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Repository.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IGenericRepository<Address> _genericRepository;

        public AddressRepository(IGenericRepository<Address> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> Add(Address address)
        {
            await _genericRepository.Add(address);
            await _genericRepository.SaveAsync();

            return address.Id;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _genericRepository
                            .GetAll()
                            .ToListAsync();
        }

        public async Task<Address> GetById(int id)
        {
            return await _genericRepository
                            .GetBy(address => address.Id.Equals(id))
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetByStreet(string name)
        {
            return await _genericRepository
                            .GetBy(address => address.Street.Equals(name))
                            .ToListAsync();
        }
    }
}
