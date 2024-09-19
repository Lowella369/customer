using Customer.API.Data;
using Customer.API.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class SQLCustomerRepository: ICustomerRepository
    {
        private readonly CustomerDBContext _dbContext;
        public SQLCustomerRepository(CustomerDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<CustomerDB?> AddCustomerAsync(CustomerDB customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<CustomerDB?> DeleteCustomerByIdAsync(Guid id)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer == null) 
            {
                return null;
            };

            _dbContext.Customers.Remove(existingCustomer);
            await _dbContext.SaveChangesAsync();

            return existingCustomer;
        }

        public async Task<List<CustomerDB>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<CustomerDB?> GetCustomerByIdAsync(Guid id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CustomerDB?> UpdateCustomerAsync(Guid id, CustomerDB customer)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer == null)
            {
                return null;
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            await _dbContext.SaveChangesAsync();
            return existingCustomer;
        }
    }
}
