﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IGuestRepo
    {
        //FindBy...
        Guest FindByPhone(string phone);        //debating string or int
        //Similar to IHostRepo
    }
}
