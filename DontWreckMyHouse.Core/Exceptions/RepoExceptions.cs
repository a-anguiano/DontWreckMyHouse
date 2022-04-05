using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Exceptions
{
    public class RepoExceptions : Exception
    {
        public RepoExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
