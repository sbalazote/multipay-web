using System;
using System.Collections.Generic;
using Model.Entities;

namespace Services
{
    interface IUserService
    {
        void Save(User user);
        User Get(int id);
        User GetByEmail(string email);
        Boolean Exists(string email);
        void Delete(int userId);
        List<User> GetAll();
    }
}
