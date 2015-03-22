using Multipay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multipay.Services
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
