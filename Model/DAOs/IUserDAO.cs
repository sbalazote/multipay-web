using System.Collections.Generic;
using Model.Entities;

namespace Model.DAOs
{
    interface IUserDAO
    {
         void Save(User user);

        User Get(int id);

        User GetByEmail(string email);

        bool Exists(string email);

        List<User> GetAll();

        void Delete(int userId);
    }
}
