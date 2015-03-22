using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Multipay.Models;
using System.Runtime.Remoting.Contexts;

namespace Multipay.DAOs
{
    public class UserDAO : IUserDAO
    {
        private MultipayContext db = new MultipayContext();

        public void Save(User User)
        {
            throw new NotImplementedException();
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