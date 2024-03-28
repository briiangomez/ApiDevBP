using ApiDevBP.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDevBP.Context
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
    }
}
