using System;
using System.Collections.Generic;
using System.Text;
using LaughSeenShpi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LaughSeenShpi.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }


        public DbSet<Room> Room { get; set; }

    }
}
