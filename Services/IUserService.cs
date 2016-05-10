using System;
using System.Collections.Generic;
using Model.Entities;

namespace Services
{
    interface IUserService
    {
        void Update(User user);
        void Save(User user);
        User Get(int id);
        User GetByEmail(string email, bool isSeller);
        void Delete(int userId);
        List<User> GetAll();
    }
}
