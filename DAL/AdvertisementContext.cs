using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdvertisementContext : DbContext
    {
        public AdvertisementContext()
        {
        }
        public AdvertisementContext(string connectionName) : base(connectionName)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
