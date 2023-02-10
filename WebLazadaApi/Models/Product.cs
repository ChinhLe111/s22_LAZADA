using System;
using System.Collections.Generic;

#nullable disable

namespace WebLazadaApi.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }

        public virtual Category Category { get; set; }
      /*  public virtual ICollection<Cart> Carts { get; set; }*/
    }
}
