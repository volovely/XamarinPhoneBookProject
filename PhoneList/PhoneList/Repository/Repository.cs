﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneList
{
    public class Repository : IRepository
    {
        IDataSource _dataSource;

        public Repository(IDataSource dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<User> Get(int id)
        {
            var user = await _dataSource.GetUserById(id);
            return user;
        }
    }
}
