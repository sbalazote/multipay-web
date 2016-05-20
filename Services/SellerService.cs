using Model.DAOs;
using Model.Entities;

namespace Services
{
    public class SellerService : ISellerService
    {
        private readonly UserDAO userDAO = new UserDAO();
        public Seller GetByMPSellerUserId(int mpSellerUserId)
        {
            return userDAO.GetByMPSellerUserId(mpSellerUserId);
        }
    }
}