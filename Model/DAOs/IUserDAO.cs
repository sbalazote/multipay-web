using System.Linq;
using Model.Entities;

namespace Model.DAOs
{
    interface IUserDAO
    {
        void Save(User user);

        void Insert(User user);

        User Get(int id);

        User GetByEmail(string email);

        User GetByEmail(string email, bool isSeller);

        bool Exists(string email);

        bool Exists(string email, bool isSeller);

        IQueryable<User> GetAll();

        void Delete(int userId);
    }
}
