﻿namespace Customer.API.Models.Domain
{
    public class CustomerDB
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
