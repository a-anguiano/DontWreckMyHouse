using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IGuestRepo
    {
        Guest FindByPhone(string phone);       
    }
}
