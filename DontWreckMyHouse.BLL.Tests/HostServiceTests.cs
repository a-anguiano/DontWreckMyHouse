using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.BLL.Tests.RepoDoubles;
using DontWreckMyHouse.Core;
using NUnit.Framework;

namespace DontWreckMyHouse.BLL.Tests
{
    public class HostServiceTests
    {
        HostService service = new HostService(new HostRepoDouble());
    }

    //id,last_name,email,phone,address,city,state,postal_code,standard_rate,weekend_rate
    //anything to test??
    //[Test]
    //public void ShouldNotSaveNullName()
    //{
    //    Host host = new Host();
    //    Result<Host> result = service.Add(host);
    //    Assert.IsFalse(result.Success);
    //}
}
