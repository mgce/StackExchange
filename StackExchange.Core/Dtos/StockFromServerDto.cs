using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Core.Dtos
{
    public class StockFromServerDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
    }
}
