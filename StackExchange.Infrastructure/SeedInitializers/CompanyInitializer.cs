using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Core.Entities;

namespace StackExchange.Infrastructure.SeedInitializers
{
    public class CompanyInitializer
    {
        private readonly Context _context;

        public CompanyInitializer(Context context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (!_context.Companies.Any())
            {
                _context.AddRange(companies);
                await _context.SaveChangesAsync();
            }
        }

        private List<Company> companies = new List<Company>()
        {
            new Company()
            {
                Name = "Future Processing",
                Code = "FP",
                StackUnits = 1000,
                ActualStackPrice = new List<StackPrice>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Stacks = new List<Stack>()
            },
            new Company()
            {
                Name = "FP Lab",
                Code = "FPL",
                StackUnits = 1000,
                ActualStackPrice = new List<StackPrice>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Stacks = new List<Stack>()
            },
            new Company()
            {
                Name = "Progress Bar",
                Code = "PGB",
                StackUnits = 1000,
                ActualStackPrice = new List<StackPrice>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Stacks = new List<Stack>()
            },
            new Company()
            {
                Name = "FP Coin",
                Code = "FPC",
                StackUnits = 1000,
                ActualStackPrice = new List<StackPrice>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Stacks = new List<Stack>()
            },
            new Company()
            {
                Name = "FP Adventure",
                Code = "FPA",
                StackUnits = 1000,
                ActualStackPrice = new List<StackPrice>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Stacks = new List<Stack>()
            },
            new Company()
            {
                Name = "Deadline 24",
                Code = "DL24",
                StackUnits = 1000,
                ActualStackPrice = new List<StackPrice>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Stacks = new List<Stack>()
            }
        };

    }
}
