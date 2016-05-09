using System;
using System.Linq;
using Model.DBInitializer;
using Model.Entities;

namespace Model.DAOs
{
    public class UserDAO : IUserDAO
    {
        private MultipayContext context = new MultipayContext();

        public void Save(User user)
        {
            context.SaveChanges();
        }

        public void Insert(User user)
        {
            context.Users.Add(user);
        }

        public User Get(int id)
        {
           return context.Users.Find(id);
        }

        public User GetByEmail(string email)
        {
            return context.Users.SingleOrDefault(x => (Equals(x.Email, email)));
        }

        public User GetByEmail(string email, bool isSeller)
        {
            return context.Users.SingleOrDefault(x => (Equals(x.Email, email)) && x is Seller);
        }

        public bool Exists(string email)
        {
            return context.Users.Any(o => Equals(o.Email, email));
        }

        public bool Exists(string email, bool isSeller)
        {
            return context.Users.Any(o => Equals(o.Email, email) && o is Seller);
        }

        public IQueryable<User> GetAll()
        {
           return context.Users;
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }
    }
}