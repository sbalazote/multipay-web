using System.Collections.Generic;
using System.Linq;
using Model.DAOs;
using Model.Entities;

namespace Services
{
    public class UserService : IUserService
    {
        private UserDAO userDAO = new UserDAO();

        public void Update(User user)
        {
            userDAO.Save(user);
        }

        public void Save(User user)
        {
            userDAO.Insert(user);
        }

        public User Get(int id)
        {
            return userDAO.Get(id);
        }

        public User GetByEmail(string email)
        {
            return userDAO.GetByEmail(email);
        }

        public User GetByEmail(string email, bool isSeller)
        {
            return userDAO.GetByEmail(email, isSeller);
        }

        public bool Exists(string email)
        {
            return userDAO.Exists(email);
        }

        public bool Exists(string email, bool isSeller)
        {
            return userDAO.Exists(email, isSeller);
        }

        public void Delete(int userId)
        {
            userDAO.Delete(userId);
        }

        public List<User> GetAll()
        {
            return userDAO.GetAll().ToList();
        }
    }
}