using System;
using System.Collections.Generic;
using Model.Entities;

namespace Services
{
    interface IUserService
    {
        void Save(User User);
        User Get(int Id);
        User GetByEmail(string Email);
        Boolean Exists(string Name);
        void Delete(int UserId);
        List<User> GetAll();
    }
}
