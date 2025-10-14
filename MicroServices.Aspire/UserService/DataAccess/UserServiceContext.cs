using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.DataAccess
{
    public class UserServiceContext : DbContext
    {
        public UserServiceContext(DbContextOptions<UserServiceContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
