using System;
using System.Security.AccessControl;

namespace Model.Entities
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; } 
        public string Password { get; set; }
        public bool Active { get; set; }
        public virtual Device Device { get; set; }
    }

    public class Buyer : User
    {
        public string MPCustomerId { get; set; }
        public string LastName { get; set; }
        public virtual Identification Identification { get; set; }
        public virtual Address Address { get; set; }
        public virtual Phone Phone { get; set; }
    }

    public class Seller : User
    {
        public string AuthCode { get; set; }
        public virtual Token Token { get; set; }
        public string SocialNetworkId { get; set; }
    }
}