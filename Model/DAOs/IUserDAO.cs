using System.Collections.Generic;
using System.Linq;
using Model.Entities;

namespace Model.DAOs
{
    interface IUserDAO
    {
         void Save(User user);

        User Get(int id);

        User GetByEmail(string email);

        bool Exists(string email);

        IQueryable<User> GetAll();

        void Delete(int userId);
    }
}
