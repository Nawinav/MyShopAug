using MyShopAug.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShopAug.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productCategories = cache["products"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        public void commit()
        {
            cache["products"] = productCategories;
        }
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }
        public void Update(ProductCategory productCategory)
        {
            ProductCategory productToUpdate = productCategories.SingleOrDefault(x => x.Id == productCategory.Id);
            if (productToUpdate != null)
            {
                productToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.SingleOrDefault(x => x.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }

        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory productToDelete = productCategories.SingleOrDefault(x => x.Id == Id);
            if (productToDelete != null)
            {
                productCategories.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
    }
}
    