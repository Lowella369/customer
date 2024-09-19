using Customer.API.Models.Domain;

namespace Customer.API.Repositories
{
    public interface ICustomerRepository
    {
        Task<List <CustomerDB>> GetAllCustomersAsync();
        Task<CustomerDB?> GetCustomerByIdAsync(Guid id);
        Task<CustomerDB> AddCustomerAsync(CustomerDB customer);
        Task<CustomerDB?> UpdateCustomerAsync(Guid id, CustomerDB customer);
        Task<CustomerDB?> DeleteCustomerByIdAsync(Guid id);

    }
}
