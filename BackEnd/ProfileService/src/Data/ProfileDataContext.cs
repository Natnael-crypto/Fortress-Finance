using Microsoft.EntityFrameworkCore;
using ProfileService.Models;

namespace ProfileService.Data;
public class ProfileDataContext(DbContextOptions<ProfileDataContext> options) : DbContext(options)
{
    public DbSet<Profile> Profiles { get; set; }

    public DbSet<SecurityQuestion> SecurityQuestions { get; set; }

    public DbSet<Address> Addresses { get; set; }
    
}