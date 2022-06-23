// using FluentAssertions;
// using System;
// using System.Buffers;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Reflection;
// using System.Runtime.CompilerServices;
// using System.Threading.Tasks;
// using Catalog.Api.Controllers;
// using Catalog.Api.Dtos;
// using Catalog.Api.Entities;
// using Catalog.Api.Repositories;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Moq;
// using Xunit;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Api.Controllers;
using Catalog.Api.Dtos;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using Xunit;
using System.Buffers;

namespace Catalog.UnitTests
{

    public class ItemsControllerTests
    {

        private readonly Mock<IItemsRepository> repositoryStub = new();
        private readonly Mock<ILogger<ItemsController>> loggerStub = new();
        private readonly Random rand = new();

        // UnitofWork_StateUnderTest_ExpectedBehaviour : Naming convention
        [Fact]
        public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Item)null);


            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetItemAsync(Guid.NewGuid());

            // Assert   
             result.Result.Should().BeOfType<NotFoundResult>();
            // Assert.IsType<NotFoundResult>(result.Result);
        }

           [Fact]
        public async Task GetItemAsync_WithExistingItem_ReturnsNotFound()
        {
        // Arrange

        //********HERE*************

        Item expectedItem = CreateRandomItem();

        repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedItem);

        
        var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

        // Act
        var result = await controller.GetItemAsync(Guid.NewGuid());

        // Assert
           result.Value.Should().BeEquivalentTo(
            expectedItem,
            options => options.ComparingByMembers<Item>());

        // Assert.IsType<ItemDto>(result.Value);
        // var dto = (result as ActionResult<ItemDto>).Value;
        // Assert.Equal(expectedItem.Id, dto.Id);
        // Assert.Equal(expectedItem.Name, dto.Name);
       
        }

        [Fact]
        public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
        {
            // Arrange
            var expectedItems = new[]{CreateRandomItem(), CreateRandomItem(), CreateRandomItem()};

            repositoryStub.Setup(repo => repo.GetItemsAsync())
                .ReturnsAsync(expectedItems);

            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);


            // Act
            var actualItems = await controller.GetItemsAsync();


            // Assert
            actualItems.Should().BeEquivalentTo(
                expectedItems,
                options => options.ComparingByMembers<Item>());

        }

        [Fact]
        public async Task CreateItemAsync_WithItemToCreate_ReturnsCreatedItem()
        {
            // Arrange
            var itemToCreate = new CreateItemDto()
            {
                Name = Guid.NewGuid().ToString(),
                Price = rand.Next(1000)
            };

            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.CreateItemAsync(itemToCreate);

            // Assert
           var createdItem = (result.Result as CreatedAtActionResult).Value as ItemDto;
           itemToCreate.Should().BeEquivalentTo(
           createdItem,
           options => options.ComparingByMembers<ItemDto>().ExcludingMissingMembers()
           );
           TimeSpan span = new TimeSpan(0,0,0,0,1000);
           createdItem.Id.Should().NotBeEmpty();
           createdItem.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, span);
        }

           [Fact]
        public async Task GetItemAsync_WithExistingItem_ReturnsAllItems()
        {
            // Arrange
            var expectedItems = new[]{CreateRandomItem(), CreateRandomItem(), CreateRandomItem()};

            repositoryStub.Setup(repo => repo.GetItemsAsync())
                .ReturnsAsync(expectedItems);

            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);


            // Act
            var actualItems = await controller.GetItemsAsync();


            // Assert
            actualItems.Should().BeEquivalentTo(
                expectedItems,
                options => options.ComparingByMembers<Item>());

        }

        [Fact]
        public async Task UpdateItemAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange

            
        Item existingItem = CreateRandomItem();

        repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingItem);

        var itemId = existingItem.Id;
        var itemToUpdate = new UpdateItemDto() 
        {
            Name = Guid.NewGuid().ToString(),
            Price = existingItem.Price + 3
        };

        var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

  
            // Act
        var result = await controller.UpdateItemAsync(itemId, itemToUpdate);

            // Assert
        result.Should().BeOfType<NoContentResult>();
           
        }

        


        

        private Item CreateRandomItem()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Price = rand.Next(1000),
                CreatedDate = DateTimeOffset.UtcNow
            };
        }



     
    }
}