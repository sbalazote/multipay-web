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

        public void Save(User User)
        {
            db.Entry(User).State = EntityState.Modified; 
            db.SaveChanges();
        }

        public User Get(int Id)
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string Email)
        {
            return db.Users.SingleOrDefault(x => (x.Email == Email));
        }

        public bool Exists(string Email)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}