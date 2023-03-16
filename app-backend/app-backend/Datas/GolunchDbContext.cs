using app_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace app_backend.Datas
{
    public class GolunchDbContext : DbContext
    {
        public GolunchDbContext(DbContextOptions<GolunchDbContext> options) : base(options) 
        {

        }

        public DbSet<Localisation> Localisations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuType> MenuTypes { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Fav> Favoris { get; set; }


        //Flo SRID 4326 fait référence à WGS 84, une norme utilisée dans le GPS et d’autres systèmes géographiques.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Flo when using NEtopology
            modelBuilder.Entity<Localisation>().Property(x => x.Position)
                .HasSrid(4326);
            // Flo > Utile mais ne fonctionne pas...
            //.HasComputedColumnSql("geography::Point(Latitude, Longitude, 4326)");

            //Karim relation One to Many localisation restaurant

            //Sam relation Many to Many restaurant user
            modelBuilder.Entity<Fav>()
                .HasOne(r => r.Restaurant)
                .WithMany(ru => ru.Favoris)
                .HasForeignKey(ri => ri.RestaurantId);

            modelBuilder.Entity<Fav>()
               .HasOne(r => r.User)
               .WithMany(ru => ru.Favoris)
               .HasForeignKey(ri => ri.UserId);
        }

    }
}
