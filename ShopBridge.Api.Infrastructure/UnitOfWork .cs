using ShopBridge.Api.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Api.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository productRepository)
        {
            Products = productRepository;
        }
        public IProductRepository Products { get; }
    }
}
