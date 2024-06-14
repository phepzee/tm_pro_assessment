using Microsoft.EntityFrameworkCore;
using ThandoMazibuko.Models;

namespace ThandoMazibuko.Data
{
    public class CustomerFeedbackDbContext : DbContext
    {
        public CustomerFeedbackDbContext(DbContextOptions<CustomerFeedbackDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerFeedback> CustomerFeedbacks { get; set; } = null!;
        public DbSet<UserRegistration> UsersRegistration { get; set; } = null!;
        public DbSet<Login> Logins { get; set; } = null!;
    }
}
