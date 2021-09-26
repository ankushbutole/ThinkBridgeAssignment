using ShopBridge.Api.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Api.Application.Interfaces
{
    public interface IProductRepository : IGenericRepository<Products>
    {
        PagingResponseModel<List<Products>> GetProductsForPage(int currentPageNumber, int pageSize);
        Task<List<Products>> GetProductsByAsce(string propertyName);

        Task<List<Products>> GetProductsByDesc(string propertyName);
    }
}
