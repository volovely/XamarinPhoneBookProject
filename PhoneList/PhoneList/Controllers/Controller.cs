﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneList
{
    public class Controller
    {
        private IUsersListAdapter _usersListAdapter;
        IRepository _repository;

        public Controller(IUsersListAdapter usersListAdapter, IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException();
            _usersListAdapter = usersListAdapter ?? throw new ArgumentNullException();
        }

        public async Task Start()
        {
            List<User> dataSourceUsersList = _repository.GetAllUsers();
            List<User> usersList = new List<User>();

            await Task.Run(async () =>
            {
                foreach (var item in dataSourceUsersList)
                {
                    await Task.Delay(2000);
                    usersList.Add(item);
                    _usersListAdapter.UpdateUsersList(usersList);
                    
                }
            });
        }
    }
}
