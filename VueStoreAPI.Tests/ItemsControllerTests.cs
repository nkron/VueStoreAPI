using System;
using System.Collections.Generic;
using System.Text;
using VueStoreAPI.Controllers;
using Xunit;
using Moq;
using VueStore.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VueStore.Repository.Models;
using Castle.Core.Logging;
using VueStoreAPI.Core;
using Microsoft.AspNetCore.Http;

namespace VueStoreAPI.Tests
{
    public class ItemsControllerTests
    {
        private readonly ItemsController _sut;
        private readonly Mock<IItemRepo> _itemRepoMock = new Mock<IItemRepo>();
        private readonly Mock<IFakeLogger> _loggerMock = new Mock<IFakeLogger>();
        private readonly Item _itemMock;
        public ItemsControllerTests()
        {
            _sut = new ItemsController(_itemRepoMock.Object, _loggerMock.Object);
            _itemMock = new Item { Id = 0, ItemName = "test0", Cost = 33 };
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            //Arrange
            var items = new List<Item> { _itemMock, new Item { Id = 1, ItemName = "test1", Cost = 44 } };
            _itemRepoMock.Setup(x => x.GetAll())
                .ReturnsAsync(items);

            //Act
            var response = _sut.GetAll().Result;

            //Assert
            Assert.IsType<OkObjectResult>(response);
        }
        [Fact]
        public void GetAll_Returns500WhenError()
        {
            //Arrange
            var items = new List<Item> { _itemMock, new Item { Id = 1, ItemName = "test1", Cost = 44 } };
            _itemRepoMock.Setup(x => x.GetAll())
                .Throws(new Exception("mock exception"));
            _loggerMock.Setup(x => x.log("test"))
                .Verifiable();
            
            const int expected = 500;
            int actual;
            //Act
            var response = (ObjectResult)_sut.GetAll().Result;
            actual = response.StatusCode.Value;
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetByID_ReturnsOk()
        {
            //Arrange
            _itemRepoMock.Setup(x => x.GetItemByID(0))
                .ReturnsAsync(_itemMock);

            //Act
            var response = _sut.GetByID(0).Result;

            //Assert
            Assert.IsType<OkObjectResult>(response);
        }
        [Fact]
        public void Create_ReturnsCreated()
        {
            //Arrange
            _itemRepoMock.Setup(x => x.CreateItem(_itemMock))
                .ReturnsAsync(1);

            //Act
            var response = _sut.Create("name",1).Result;

            //Assert
            Assert.IsType<CreatedResult>(response);
        }
        [Fact]
        public void Create_ReturnsCreatedItem()
        {
            //Arrange
            _itemRepoMock.Setup(x => x.CreateItem(_itemMock))
                .ReturnsAsync(0);
            
            Item expected = new Item { Cost = 123, Id = 0, ItemName = "name" };
            Item actual;
            //Act
            var response = (ObjectResult)_sut.Create("name", 123).Result;
            actual = (Item)response.Value;
            //Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.ItemName, actual.ItemName);
            Assert.Equal(expected.Cost, actual.Cost);
        }
        [Fact]
        public void Delete_ReturnsNoContent()
        {
            //Arrange
            _itemRepoMock.Setup(x => x.DeleteItem(0))
                .ReturnsAsync(1);

            //Act
            var response = _sut.Delete(0).Result;

            //Assert
            Assert.IsType<NoContentResult>(response);
        }
        [Fact]
        public void Update_ReturnsOk()
        {
            //Arrange
            _itemRepoMock.Setup(x => x.UpdateItem(_itemMock))
                .ReturnsAsync(1);

            //Act
            var response = _sut.Update(_itemMock).Result;

            //Assert
            Assert.IsType<NoContentResult>(response);
        }
    }
}
