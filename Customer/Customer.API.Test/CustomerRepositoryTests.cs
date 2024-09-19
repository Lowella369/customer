using AutoMapper;
using Customer.API.Controllers;
using Customer.API.Data;
using Customer.API.Models.Domain;
using Customer.API.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.API.Test
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        private CustomerDB _addCustomer;
        private IMapper _mapper;

        private static DbContextOptions<CustomerDBContext> dbContextOptions = new DbContextOptionsBuilder<CustomerDBContext>()
       .UseInMemoryDatabase(databaseName: "CustomerDBTest")
       .Options;

        CustomerDBContext _dBContext;
        SQLCustomerRepository _sqlCustomerRepository;
        CustomersController _customersController;

        [OneTimeSetUp]
        public void Setup()
        {
            

            _dBContext = new CustomerDBContext(dbContextOptions);
            _dBContext.Database.EnsureCreated();
            SeedDatabase();

            _sqlCustomerRepository = new SQLCustomerRepository(_dBContext);
            _customersController = new CustomersController(_dBContext, _sqlCustomerRepository, _mapper);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _dBContext.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var customer = new List<CustomerDB>()
            {
                new CustomerDB()
                {
                    Id = Guid.Parse("2c9ca0c8-0f7a-4cab-b578-8a1a64f45e46"),
                    FirstName = "Robert",
                    LastName = "De Niro",
                    PhoneNumber = "1234567890",
                },
                new CustomerDB()
                {
                    Id = Guid.Parse("a3f3a90f-8e5a-44c0-b970-068a2e25dba9"),
                    FirstName = "Charles",
                    LastName = "Darwin",
                    PhoneNumber = "1234567891",
                }
            };

            _dBContext.Customers.AddRange(customer);
            _dBContext.SaveChanges();

        }

        public void CustomerInfo()
        {
            _addCustomer = new CustomerDB()
            {
                Id = Guid.Parse("fdc7f7ac-6efc-42c0-b62f-1bf8b19d4d06"),
                FirstName = "Leonardo",
                LastName = "Di Carpio",
                PhoneNumber = "1234567890",
            };
        }

        [Test, Order(1)]
        public void GetAllCustomers_Test()
        {
            var result = _sqlCustomerRepository.GetAllCustomersAsync();
        }

        [Test, Order(2)]
        public void SaveCustomerInfo_CheckTheValuesFromDatabase()
        {
            CustomerInfo();

            using (var context = new CustomerDBContext(dbContextOptions))
            {
                var repository = new SQLCustomerRepository(context);
                repository.AddCustomerAsync(_addCustomer);
                
            }

            using (var context = new CustomerDBContext(dbContextOptions))
            {
                var addCustomerFromDB = context.Customers.FirstOrDefault(x => x.Id == Guid.Parse("fdc7f7ac-6efc-42c0-b62f-1bf8b19d4d06"));
                Assert.That(addCustomerFromDB, Is.Not.Null);
                Assert.That(_addCustomer.Id, Is.EqualTo(addCustomerFromDB.Id));
                Assert.That(_addCustomer.FirstName, Is.EqualTo(addCustomerFromDB.FirstName));
                Assert.That(_addCustomer.LastName, Is.EqualTo("Di Carpio"));
                Assert.That(_addCustomer.PhoneNumber, Is.EqualTo(addCustomerFromDB.PhoneNumber));
            }
        }
    }
}
