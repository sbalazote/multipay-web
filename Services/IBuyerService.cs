using Model.Entities;

namespace Services
{
    interface IBuyerService
    {
        Buyer GetByPhone(int areaCode, string number);
    }
}
