using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IHostRepo
    {
        //FindBy
    
        //Find by specific, or find through a list?
        //ex. for giving admin list to choose from would be 
        List<Host> FindByState(string stateAbbr);

        //Perhaps if extra time, have a way to narrow down the search if needed
        //use LINQ
    }
}
