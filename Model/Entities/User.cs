using System;
using System.Net.Sockets;
using System.Security.Principal;

namespace Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; } 
        public string Password { get; set; }
        public bool Active { get; set; }
    }

    public class Buyer : User
    {
        public string LastName { get; set; }
        public Identification Identification { get; set; }
        public Address Address { get; set; }
        public Phone Phone { get; set; }
    }

    public class Seller : User
    {
        public string AuthCode { get; set; }
        public Token Token { get; set; }
    }
}