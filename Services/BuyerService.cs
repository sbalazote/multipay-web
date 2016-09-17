using Model.DAOs;
using Model.Entities;

namespace Services
{
    public class BuyerService : IBuyerService
    {
        private readonly UserDAO userDAO = new UserDAO();
        public Buyer GetByPhone(int areaCode, string number)
        {
            return userDAO.GetByPhone(areaCode, number);
        }
    }
}