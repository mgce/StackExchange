using Newtonsoft.Json;
using StackExchange.Core.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.BackgroundJobs.Scheduler;
using StackExchange.Core.Entities;
using StackExchange.Core.Repositories;

namespace StackExchange.BackgroundJobs
{
    public class GetStockPriceTask : IScheduledTask
    {
        private ICompanyRepository _companyRepository;
        private IStackPriceRepository _stackPriceRepository;

        public GetStockPriceTask(ICompanyRepository companyRepository,
            IStackPriceRepository stackPriceRepository)
        {
            _companyRepository = companyRepository;
            _stackPriceRepository = stackPriceRepository;
        }

        public string Schedule => "*/1 * * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient
            { BaseAddress = new Uri("http://webtask.future-processing.com:8068/stocks")};
            var response = await httpClient.GetAsync("http://webtask.future-processing.com:8068/stocks", cancellationToken);
            response.EnsureSuccessStatusCode();
            using (response.Content)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var stocks = JsonConvert.DeserializeObject<StocksDto>(responseBody).Items;

                var companies = _companyRepository.GetAllAsync().Result;
                
                foreach (var stock in stocks)
                {
                    var company = companies.SingleOrDefault(x => x.Name == stock.Name && x.Code == stock.Code);
                    if (company != null)
                    {
                        var stockPrice = new StackPrice
                        {
                            Company = company,
                            CompanyId = company.Id,
                            Price = stock.Price,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };
                        await _stackPriceRepository.AddAsync(stockPrice);
                    }                 
                }
            }
        }
    }
}
