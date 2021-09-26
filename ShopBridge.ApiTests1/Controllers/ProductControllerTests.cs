using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridge.Api.Application.Entities;
using ShopBridge.Api.Application.Interfaces;
using ShopBridge.Api.Controllers;
using ShopBridge.Api.LoggerService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Api.Controllers.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IGenericRepository<Products>> _genericRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILoggerManager> _loggerManagerMock;
        public ProductControllerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _genericRepositoryMock = new Mock<IGenericRepository<Products>>();
            _loggerManagerMock = new Mock<ILoggerManager>();

        }
        [TestMethod()]
        public async Task GetAllProdcts()
        {
            //Act
            var productController = new ProductController(_loggerManagerMock.Object,_unitOfWorkMock.Object);
            _unitOfWorkMock.Setup(m => m.Products.GetAllAsync()).ReturnsAsync((List<Products>)null);
            _genericRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync((List<Products>)null);
            _productRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync((List<Products>)null);
            var actionResult = await productController.GetAll().ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actionResult);
        }
    }
}