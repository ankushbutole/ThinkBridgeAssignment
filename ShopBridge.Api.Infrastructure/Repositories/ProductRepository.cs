using Dapper;
using Microsoft.Extensions.Configuration;
using ShopBridge.Api.Application.Entities;
using ShopBridge.Api.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Api.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration configuration;
        public ProductRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Products entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
        public async Task<IReadOnlyList<Products>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Products>(sql);
                return result.ToList();
            }
        }
        public async Task<Products> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Products>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<int> UpdateAsync(Products entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public PagingResponseModel<List<Products>> GetProductsForPage(int currentPageNumber, int pageSize)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            string query = @"SELECT 
                            COUNT(*)
                            FROM Products
 
                            SELECT  * FROM Products
                            ORDER BY Id
                            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var reader = connection.QueryMultiple(query, new { Skip = skip, Take = take });

                int count = reader.Read<int>().FirstOrDefault();
                List<Products> allProducts = reader.Read<Products>().ToList();

                var result = new PagingResponseModel<List<Products>>(allProducts, count, currentPageNumber, pageSize);
                return result;
            }

            
        }

        public async Task<List<Products>> GetProductsByAsce(string propertyName)
        {
            var sql = "SELECT * FROM Products";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Products>(sql);
                var list = result.AsQueryable().OrderByPropertyName(propertyName, true).ToList();
                return list;
            }

        }

        public async Task<List<Products>> GetProductsByDesc(string propertyName)
        {
            var sql = "SELECT * FROM Products";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Products>(sql);
                var list = result.AsQueryable().OrderByPropertyName(propertyName, false).ToList();
                return list;
            }

        }
    }
}
