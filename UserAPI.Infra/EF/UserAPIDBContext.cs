using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Domain.Models;

namespace UserAPI.Infra.EF
{
    public class UserAPIDBContext : DbContext
    {
        public DbSet<User> users { get; set; }

        public UserAPIDBContext(DbContextOptions<UserAPIDBContext> options) : base(options)
        {

        }
    }
}
