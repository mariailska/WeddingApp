using WeddingApp.Api.Entities;

namespace WeddingApp.Api.Data;
using Microsoft.EntityFrameworkCore;

public class WeddingDbContext : DbContext
{
    public WeddingDbContext(DbContextOptions<WeddingDbContext> options) : base(options){}
    
    public DbSet<Invitation> Invitations { get; set; } = null!;
    public DbSet<Guest> Guests { get; set; } = null!;

}