using Multipay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multipay.DAOs
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
