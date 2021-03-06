﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneList
{
    public class ModelCreator : IModelCreator
    {
        IRepository _repository;

        public ModelCreator(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<int> GetAllIdList()
        {
            var idList = _repository.GetAllIdList();
            return idList;
        }

        public async Task<ViewModel> GetModel(int id)
        {
            var user = await _repository.Get(id);
            ViewModel model;
            if (user == null)
            {
                model = new ViewModel("", "");
            }
            else
            {
                model = new ViewModel(user.Name, user.LastName); 
            }
            return model;
        }

        public async Task<ViewModel> GetNextUserModel()
        {
            var user = await _repository.GetNextUser();
            ViewModel model;
            if (user == null)
            {
                model = new ViewModel("", "");
            }
            else
            {
                model = new ViewModel(user.Name, user.LastName);
            }
            return model;
        }
    }
}
