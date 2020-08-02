using Registration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface IAddressRepository
    {
        Task<int> Add(Address address);
        Task<Address> GetById(int id);
        Task<IEnumerable<Address>> GetAll();
        Task<IEnumerable<Address>> GetByStreet(string name);
    }
}
