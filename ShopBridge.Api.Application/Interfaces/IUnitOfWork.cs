using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Api.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
    }
}
