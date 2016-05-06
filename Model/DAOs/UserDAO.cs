using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using Model.DBInitializer;
using Model.Entities;

namespace Model.DAOs
{
    public class UserDAO : IUserDAO
    {
        private MultipayContext db = new MultipayContext();

        public void Save(User user)
        {
            db.Entry(user).State = EntityState.Modified; 
            db.SaveChanges();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => (Equals(x.Email, email)));
        }

        public User GetByEmail(string email, bool isSeller)
        {
            return db.Users.SingleOrDefault(x => (Equals(x.Email, email)) && x is Seller);
        }

        public bool Exists(string email)
        {
            return db.Users.Any(o => Equals(o.Email, email));
        }

        public bool Exists(string email, bool isSeller)
        {
            return db.Users.Any(o => Equals(o.Email, email) && o is Seller);
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }
    }
}