﻿using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;

namespace PhoneList.Droid
{
    public class RecyclerViewAdapter : RecyclerView.Adapter, IUsersListAdapter
    {
        private List<User> _usersList;
        IRepository _repository;

        public RecyclerViewAdapter(IRepository repository)
        {
            _repository = repository;
            _usersList = new List<User>();
        }

        public override int ItemCount => _usersList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder vh = holder as RecyclerViewHolder;
            var presenter = new Presenter(vh, new Interactor(new ModelCreator(_repository)));
            presenter.Init(_usersList[position].Id);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.item, parent, false);

            var vh = new RecyclerViewHolder(itemView);
            return vh;
        }

        public void UpdateUsersList(List<User> usersList)
        {
            _usersList = usersList;
        }
    }
}