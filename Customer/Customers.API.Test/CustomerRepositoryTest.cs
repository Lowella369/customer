using AutoMapper;
using Customer.API.Controllers;
using Customer.API.Data;
using Customer.API.Models.Domain;
using Customer.API.Models.DTO;
using Customer.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Customers.API.Test
{
    public class CustomerRepositoryTest
    {
        private static DbContextOptions<CustomerDBContext> dbContextOptions = new DbContextOptionsBuilder<CustomerDBContext>()
            .UseInMemoryDatabase(databaseName: "CustomerDBTest")
            .Options;

        CustomerDBContext context;
        SQLCustomerRepository sqlCustomerRepository;
        CustomersController customersController;
        IMapper mapper;

        [OneTimeSetUp]
        public void Setup()
        {

            context = new CustomerDBContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();

            sqlCustomerRepository = new SQLCustomerRepository(context);
            customersController = new CustomersController(context, sqlCustomerRepository, mapper);
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var customer = new List<CustomerDB>()
            {
                new CustomerDB()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Robert",
                    LastName = "De Niro",
                    PhoneNumber = "1234567890",
                },
                new CustomerDB()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Charles",
                    LastName = "Darwin",
                    PhoneNumber = "1234567891",
                }
            };
            context.Customers.AddRange(customer);

            context.SaveChanges();

        }
    }
}