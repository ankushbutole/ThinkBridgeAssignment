using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Api.Application.Entities;
using ShopBridge.Api.Application.Interfaces;
using ShopBridge.Api.LoggerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Api.Controllers
{
    /// <summary>
    /// The Product controller groups together all methods related to Product operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ILoggerManager _logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The Product controller initilaization
        /// </summary>
        public ProductController(ILoggerManager logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all product details
        /// </summary>
        
        [HttpGet]
        [Route("getallproductsdetails")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInfo("Getting all products details");
             var data = await unitOfWork.Products.GetAllAsync();
             return Ok(data);
        }

        /// <summary>
        /// Get product details By id 
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Products.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        /// <summary>
        /// Add product details 
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("addproductdetails")]
        public async Task<IActionResult> Add(Products product)
        {
            var data = await unitOfWork.Products.AddAsync(product);
            return Ok(data);
        }


        /// <summary>
        /// Delete product details 
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("deleteproductdetails")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Products.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            else {
                var productdata = await unitOfWork.Products.DeleteAsync(id);
                return Ok(productdata);
            }
            
        }

        /// <summary>
        /// Update product details 
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("updateproductdetails")]
        public async Task<IActionResult> Update(Products product)
        {
            var data = await unitOfWork.Products.UpdateAsync(product);
            return Ok(data);
        }

        /// <summary>
        /// Get product details as per pagination 
        /// </summary>
        [HttpGet]
        [Route("page")]
        public IActionResult GetProductsForPage(int currentPageNumber, int pageSize)
        {
            var data = unitOfWork.Products.GetProductsForPage(currentPageNumber, pageSize);
            return Ok(data);
        }

        /// <summary>
        /// Get product details in ascending order for specified property 
        /// </summary>
        [HttpGet]
        [Route("getproductsbyAsce")]
        public async Task<IActionResult> GetProductByAsceOrder(string propertyName)
        {
            var data = await unitOfWork.Products.GetProductsByAsce(propertyName);
            return Ok(data);
        }

        /// <summary>
        /// Get product details in descending order for specified property 
        /// </summary>
        [HttpGet]
        [Route("getproductsbyDesc")]
        public async Task<IActionResult> GetProductByDescOrder(string propertyName)
        {
            var data = await unitOfWork.Products.GetProductsByDesc(propertyName);
            return Ok(data);
        }

    }
}
