using Registration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface IAddressRepository
    {
        Task<Address> Add(Address address);
        Task<Address> Update(Address oldAddress, Address newAddress);
        Task<Address> GetById(int id);
        Task<IEnumerable<Address>> GetAll();
        Task<IEnumerable<Address>> GetByStreet(string name);
        bool Exists(Expression<Func<Address, bool>> expression);
    }
}
