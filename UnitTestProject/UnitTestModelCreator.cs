﻿using System;
using System.Reflection;
using Moq;
using NUnit.Framework;
using PhoneList;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTestModelCreator
    {
        IModelCreator _modelCreator;
        Mock<IRepository> _repositoryMock;

        [SetUp]
        public void ModelCreatorSetUp()
        {
            _repositoryMock = new Mock<IRepository>();
            _modelCreator = new ModelCreator(_repositoryMock.Object);
        }

        [Test]
        public void TestModelCreatorCtorCheckRepositorySet()
        {
            var fieldInfo = typeof(ModelCreator).GetField("_repository", BindingFlags.NonPublic | BindingFlags.Instance);
            var data = fieldInfo.GetValue(_modelCreator);
            Assert.AreEqual(_repositoryMock.Object, data);
        }

        [Test]
        public void TestModelCreatorCtorExNullRepository()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _modelCreator = new ModelCreator(null);
            });
        }

        [Test]
        public async Task TestModelCreatorGetModel()
        {
            int userId = 0;
            var fname = "";
            var lname = "";
            var model = new ViewModel(fname, lname);

            var resultModel = await _modelCreator.GetModel(userId);

            Assert.AreEqual(model, resultModel);
        }

        [Test]
        public async Task TestModelCreatorGetUserFromRepository()
        {
            int userId = 0;
            var user = new User(0, "fname", "lname", 0, "");
            var model = new ViewModel("fname", "lname");

            _repositoryMock.Setup(f => f.Get(userId))
                                        .Returns(Task.FromResult(user));

            await _modelCreator.GetModel(userId);

            _repositoryMock.Verify(f => f.Get(userId), Times.Once);
        }

        [Test]
        public async Task TestModelCreatorGetUserFromRepositoryNullUser()
        {
            int userId = 0;
            User user = null;
            var expModel = new ViewModel("", "");

            _repositoryMock.Setup(f => f.Get(userId))
                                        .Returns(Task.FromResult(user));

            var resModel = await _modelCreator.GetModel(userId);

            _repositoryMock.Verify(f => f.Get(userId), Times.Once);

            Assert.AreEqual(expModel, resModel);
        }

        [Test]
        public async Task TestModelCreatorGetRealUserFromRepository()
        {

            int userId = 0;
            User expUser = new User(0, "name0", "LastName0", 10, "");
            var expModel = new ViewModel("name0", "LastName0");

            _repositoryMock.Setup(f => f.Get(userId))
                                        .Returns(Task.FromResult(expUser));

            var resModel = await _modelCreator.GetModel(userId);

            _repositoryMock.Verify(f => f.Get(userId), Times.Once);

            Assert.AreEqual(expModel, resModel);
        }

        [Test]
        public void TestModelCreatorGetAllIdList()
        {
            List<int> idList = new List<int>();
            _repositoryMock.Setup(f => f.GetAllIdList())
                                        .Returns(idList);
            _modelCreator.GetAllIdList();

            _repositoryMock.Verify(f => f.GetAllIdList(), Times.Once);
        }

        [Test]
        public async Task TestModelCreatorGetNextUserFromRepository()
        {
            var user = new User(0, "fname", "lname", 0, "");
            var model = new ViewModel("fname", "lname");

            _repositoryMock.Setup(f => f.GetNextUser())
                                        .Returns(Task.FromResult(user));

            await _modelCreator.GetNextUserModel();

            _repositoryMock.Verify(f => f.GetNextUser(), Times.Once);
        }

        [Test]
        public async Task TestModelCreatorGetNextUserFromRepositoryNull()
        {
            User user = null;
            var model = new ViewModel("", "");

            _repositoryMock.Setup(f => f.GetNextUser())
                                        .Returns(Task.FromResult(user));

            await _modelCreator.GetNextUserModel();

            _repositoryMock.Verify(f => f.GetNextUser(), Times.Once);
        }
    }
}
