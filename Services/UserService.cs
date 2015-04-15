using System.Collections.Generic;
using Model.DAOs;
using Model.Entities;

namespace Services
{
    public class UserService : IUserService
    {
        private UserDAO userDAO = new UserDAO();

        public void Save(User User)
        {
            userDAO.Save(User);
        }

        public User Get(int Id)
        {
            return userDAO.Get(Id);
        }

        public User GetByEmail(string Email)
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

        public List<User> GetAll()
        {
            return userDAO.GetAll();
        }
    }
}