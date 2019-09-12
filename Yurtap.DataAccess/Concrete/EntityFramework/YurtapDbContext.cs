using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Entity;

namespace Yurtap.DataAccess.Concrete.EntityFramework
{
    public class YurtapDbContext : DbContext
    {
        public YurtapDbContext()
        {

        }

        //public YurtapDbContext(DbContextOptions<YurtapDbContext> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;database=YurtapDb;Integrated Security=true;AttachDbFileName=C:\Users\Administrator\YurtapDbv11.mdf");
            //optionsBuilder.UseSqlServer(@"Server=tcp:yurtap.database.windows.net,1433;Initial Catalog=YurtapDb;Persist Security Info=False;User ID=yurtap;Password=yrtp_0832;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\v11.0;database=YurtapDbv11;Integrated Security=true;");
            //DbContextOptionsBuilder<YurtapDbContext> options = new DbContextOptionsBuilder<YurtapDbContext>();
            // new YurtapDbContext(options.Options);
            base.OnConfiguring(optionsBuilder);
        }


    public DbSet<KisiEntity> Kisiler { get; set; }
        public DbSet<OgrenciEntity> Ogrenciler { get; set; }
        public DbSet<PersonelEntity> Personeller { get; set; }
        public DbSet<YoklamaBaslikEntity> YoklamaBasliklari { get; set; }
        public DbSet<YoklamaEntity> Yoklamalar { get; set; }
        public DbSet<KullaniciEntity> Kullanicilar { get; set; }
        public DbSet<RolEntity> Roller { get; set; }
        public DbSet<KullaniciRolEntity> KullaniciRolleri { get; set; }
    }
}