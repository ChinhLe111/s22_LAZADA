using System;
using System.Collections.Generic;

#nullable disable

namespace WebLazadaApi.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}
