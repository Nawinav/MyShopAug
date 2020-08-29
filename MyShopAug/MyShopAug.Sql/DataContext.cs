using MyShopAug.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopAug.Sql
{
    public class DataContext:DbContext
    {
        public DataContext():base("MyConnection")
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
    }
}
