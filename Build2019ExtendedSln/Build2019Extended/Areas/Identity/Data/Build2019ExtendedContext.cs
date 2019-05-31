using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Build2019Extended.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Build2019Extended.Models
{
    public class Build2019ExtendedContext : IdentityDbContext<Build2019ExtendedUser>
    {
        public Build2019ExtendedContext(DbContextOptions<Build2019ExtendedContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
