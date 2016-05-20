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

        public User GetByEmail(string email, bool isSeller)
        {
            return context.Users.SingleOrDefault(o => o.Email == email && (isSeller ? o is Seller : o is Buyer));
        }

        public IQueryable<User> GetAll()
        {
           return context.Users;
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public Seller GetByMPSellerUserId(int mpSellerUserId)
        {
            return context.Users.OfType<Seller>().SingleOrDefault(o => o.MPSellerUserId == mpSellerUserId);
        }
    }
}