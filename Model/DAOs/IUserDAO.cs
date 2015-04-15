using System.Collections.Generic;
using Model.Entities;

namespace Model.DAOs
{
    interface IUserDAO
    {
         void Save(User User);

        User Get(int Id);

        User GetByEmail(string Email);

        bool Exists(string Email);

        List<User> GetAll();

        void Delete(int UserId);
    }
}
