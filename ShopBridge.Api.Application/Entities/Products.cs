using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopBridge.Api.Application.Entities
{
    public class Products
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public string Barcode { get; set; }

        public decimal Rate { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
