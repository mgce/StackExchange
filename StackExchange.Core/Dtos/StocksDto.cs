using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Core.Dtos
{
    public class StocksDto
    {
        public ICollection<StockFromServerDto> Items { get; set; }
    }
}
