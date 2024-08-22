using AfterNoonV2.Domain.Entities;
using AfterNoonV2.Domain.Entities.Common;
using AfterNoonV2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Persistance
{
    public class AfterNoonV2DbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AfterNoonV2DbContext(DbContextOptions<AfterNoonV2DbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImagesFiles { get; set; }
        public DbSet<InvocesFile> InvocesFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Enpoint> Enpoints { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(x => x.Id);
            builder.Entity<Basket>().HasOne(b => b.Order).WithOne(o => o.Basket).HasForeignKey<Order>(b => b.Id);

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                {
                    data.Entity.CreateDate = DateTime.UtcNow;
                    data.Entity.UpdateDate = DateTime.UtcNow;
                }
                else if (data.State == EntityState.Modified)
                    data.Entity.UpdateDate = DateTime.Now;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
