using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using StackExchange.Core.Entities;
using StackExchange.Infrastructure;
using Xunit;

namespace StackExchange.Tests.Entities
{
    public class CompanyTests
    {
        private IFixture fixture;

        public CompanyTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private Context GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new Context(options);

            var user = fixture.Build<User>().Without(u => u.Stacks).Create();

            var company1 = new Company()
            {
                ActualStackPrice = new List<StackPrice>(),
                Code = "code",
                CreatedAt = DateTime.UtcNow.AddMinutes(-10),
                Id = 1,
                Name = "Company",
                Stacks = new List<Stack>(),
                UpdatedAt = DateTime.UtcNow
            };
            var company2 = new Company()
            {
                ActualStackPrice = new List<StackPrice>(),
                Code = "code",
                CreatedAt = DateTime.UtcNow.AddMinutes(-10),
                Id = 2,
                Name = "Company",
                Stacks = new List<Stack>(),
                UpdatedAt = DateTime.UtcNow
            };
            for(var i = 0; i < 10; i++)
            {
                var stack = new Stack((decimal)(i+32412)/1000, 100+i,user,company2 );
                var stackPrice = new StackPrice()
                {
                    Company = company2,
                    CompanyId = company2.Id,
                    Id = 10+i,
                    Price = (decimal) (34242 + i) / 100,
                    CreatedAt = DateTime.Now.AddMinutes(i),
                    UpdatedAt = DateTime.Now.AddMinutes(i)

                };
                company2.ActualStackPrice.Add(stackPrice);
            }

            context.Add(company1);
            context.Add(company2);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void Should_GetActualCompanyPrice_ReturnZero()
        {            
            using(var context = GetContextWithData())
            {
                var company = context.Companies.SingleOrDefaultAsync(x=>x.Id == 1).Result;
                var price = company.GetActualStackPrice();

                Assert.NotNull(price);
                Assert.Equal(price, 0);
            }
        }

        [Fact]
        public void Should_GetActualCompanyPrice_ReturnStack()
        {
            using (var context = GetContextWithData())
            {
                var company = context.Companies.SingleOrDefaultAsync(x => x.Id == 2).Result;
                var price = company.GetActualStackPrice();

                Assert.NotNull(price);
                Assert.Equal(price, (decimal)34251/100);
            }
        }
    }
}
