

namespace Basket.API.Exception;

public class BasketNotFoundexception : NotFoundException
{
    public BasketNotFoundexception(string userName) : base("Basket", userName)
    {
    }
}
