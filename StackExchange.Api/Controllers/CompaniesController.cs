using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Core.Dtos;
using StackExchange.Core.Repositories;
using StackExchange.Infrastructure.EF;

namespace StackExchange.Api.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IStackPriceRepository _stackPriceRepository;

        public CompaniesController(ICompanyRepository companyRepository,
            IStackPriceRepository stackPriceRepository)
        {
            _companyRepository = companyRepository;
            _stackPriceRepository = stackPriceRepository;
        }
        // GET
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var companyDtoList = new List<CompanyDto>();

            var companies = await _companyRepository.GetAllAsync();
            foreach (var company in companies)
            {
                var price = await _stackPriceRepository.GetActualPriceByCompany(company.Id);
                companyDtoList.Add(new CompanyDto()
                {
                    Name = company.Name,
                    Price = price
                });
            }
            return new JsonResult(companyDtoList);
        }
    }
}