using Microsoft.EntityFrameworkCore;
using CURD_API_LEARING.Models;

namespace CURD_API_LEARING.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
