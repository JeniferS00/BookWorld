using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWorld.ViewModel
{
    public class ShoppingCartViewModel
    {
        public string ItemID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public string ImagePath { get; set; }
        public string ItemName { get; set; }

    }
}