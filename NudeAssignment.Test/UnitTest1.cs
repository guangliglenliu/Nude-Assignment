using System;
using Xunit;
using NudeAssignment.Services;
using Moq;
using NudeAssignment.Controllers;
using System.Collections.Generic;
using NudeAssignment.Models;
using System.Linq;

namespace NudeAssignment.Test
{
    public class UnitTest1
    {
        private readonly Mock<ICoverageItemService> _mockService;
        private CustomerItemController _controller;

        public UnitTest1()
        {
            _mockService = new Mock<ICoverageItemService>();
        }

        [Fact]
        public void TestGetCategoryList()
        {
            // Arrange
            _mockService.Setup(s => s.GetCoverageItemCategories())
                .Returns(new List<LKPCoverageItemCategory>
                {
                    new LKPCoverageItemCategory
                    {
                        CategoryId = 1,
                        Name = "Category1",
                        Description = ""
                    },
                    new LKPCoverageItemCategory
                    {
                        CategoryId = 2,
                        Name = "Category2",
                        Description = null
                    },
                    new LKPCoverageItemCategory
                    {
                        CategoryId = 3,
                        Name = "Category3",
                        Description = ""
                    }
                });

            _controller = new CustomerItemController(_mockService.Object);

            // Act
            var categories = _controller.Categories().ToList();

            // Assert
            Assert.Equal(3, categories.Count);
            Assert.Equal(1, categories[0].CategoryId);
            Assert.True("Category3" == categories[2].Name);
        }

        [Fact]
        public void TestRemove()
        {
            // Arrange
            _mockService.Setup(r => r.RemoveCustomerItem(Guid.Parse("B9F89C99-E884-4A1D-B37E-8872AB3121A4"))).Returns(0);
                
            _controller = new CustomerItemController(_mockService.Object);

            // Act
            var removed = _controller.Remove(Guid.Parse("B9F89C99-E884-4A1D-B37E-8872AB3121A4"));

            // Assert
            _mockService.Verify(mock => mock.RemoveCustomerItem(It.Is<Guid>(id => id.Equals(Guid.Parse("B9F89C99-E884-4A1D-B37E-8872AB3121A4")))), Times.Once);
            Assert.Equal(0, removed);
        }
    }
}
