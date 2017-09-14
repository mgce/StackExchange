using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Core.Repositories;
using StackExchange.Infrastructure.EF;

namespace StackExchange.Api.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        // GET
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var companies = await _companyRepository.GetAllAsync();
            return new JsonResult(companies);
        }
    }
}