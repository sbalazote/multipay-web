using Model.Entities;

namespace Services
{
    interface ISellerService
    {
        Seller GetByMPSellerUserId(int mpSellerUserId);
    }
}