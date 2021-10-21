using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api.Models
{
    public static class DBCreate
    {
        public static void INIT(IServiceProvider serviceProvider)
        {
            var context = new WebApiDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<WebApiDbContext>>());

            context.Database.EnsureCreated();
            if (context.Product.Any())
            {
                return;
            }

        }

    }
}
