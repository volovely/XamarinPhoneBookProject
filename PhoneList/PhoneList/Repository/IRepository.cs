﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneList
{
    public interface IRepository
    {
        Task<User> Get(int id);
    }
}
