using Multipay.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multipay.Services
{
    public class UserService : IUserService
    {
        private UserDAO userDAO = new UserDAO();

        public void Save(Models.User User)
        {
            userDAO.Save(User);
        }

        public Models.User Get(int Id)
        {
            return userDAO.Get(Id);
        }

        public Models.User GetByEmail(string Email)
        {
            return userDAO.GetByEmail(Email);
        }

        public bool Exists(string Email)
        {
            return userDAO.Exists(Email);
        }

        public void Delete(int UserId)
        {
            userDAO.Delete(UserId);
        }

        public List<Models.User> GetAll()
        {
            return userDAO.GetAll();
        }
    }
}