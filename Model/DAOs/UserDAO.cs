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
            context.SaveChanges();
        }

        public User Get(int id)
        {
           return context.Users.Find(id);
        }

        public User GetByEmail(string email)
        {
            return context.Users.SingleOrDefault(x => x.Email == email);
        }

        public User GetByEmail(string email, bool isSeller)
        {
            var user = context.Users.SingleOrDefault(x => x.Email == email);
            if (user is Seller && isSeller)
                return user;
            if (user is Buyer && !isSeller)
                return user;
            return null;
        }

        public bool Exists(string email)
        {
            return context.Users.Any(o => o.Email == email);
        }

        public bool Exists(string email, bool isSeller)
        {
            var user = context.Users.SingleOrDefault(o => o.Email == email);
            if (user is Seller && isSeller)
                return true;
            if (user is Buyer && !isSeller)
                return true;
            return false;
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