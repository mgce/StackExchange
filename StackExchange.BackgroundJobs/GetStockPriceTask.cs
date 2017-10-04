using Newtonsoft.Json;
using StackExchange.Core.Dtos;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using StackExchange.BackgroundJobs.Scheduler;

namespace StackExchange.BackgroundJobs
{
    public class GetStockPriceTask : IScheduledTask
    {
        public string Schedule => "0 1 * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://webtask.future-processing.com:8068/stocks");
            var response = await httpClient.GetAsync("http://webtask.future-processing.com:8068/stocks");
            response.EnsureSuccessStatusCode();
            using (HttpContent content = response.Content)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var stocks = JsonConvert.DeserializeObject<StocksDto>(responseBody);
                var test = stocks.Items;
            }
        }
    }
}
