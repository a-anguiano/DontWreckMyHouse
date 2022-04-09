namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IHostRepo
    {
        Host FindByPhone(string phone);
        List<Host> FindByState(string stateAbbr);
    }
}
