using Registration.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Contracts
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAll();
        Task<Address> GetById(int id);
        Task<IEnumerable<Address>> GetByStreet(string name);
    }
}
