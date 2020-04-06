using System.Collections.Generic;
using Pygma.Data;
using Pygma.Data.Domain.Entities;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.TestDb.Seed
{
    public class BlogPostCommentSeed : ISeeder
    {
        public void Seed(PygmaDbContext dbContext)
        {
            dbContext.SetInsertIdentity("Products", true);
            CreateCabinetDoors(dbContext);
            dbContext.SaveChanges();
            dbContext.SetInsertIdentity("Products", false);
            dbContext.SaveChanges();
        }
        
        private static void CreateCabinetDoors(PygmaDbContext dbContext)
        {
            var list = new List<BlogPostComment>
            {
                // CreateProduct(1, 12, 30, EnProductType.CabinetDoor, "1/012030", EnProductCategory.Standard, 5.1m),
                // CreateProduct(2, 12, 30, EnProductType.CabinetDoor, "2/012030", EnProductCategory.Classic, 6.2m),
                // CreateProduct(3, 12, 30, EnProductType.CabinetDoor, "3/012030", EnProductCategory.Superior, 7.3m),
                // CreateProduct(4, 12, 30, EnProductType.CabinetDoor, "4/012030", EnProductCategory.Gloss, 8.4m),
                // CreateProduct(SeedConstants.InActiveProduct, 30, 80, EnProductType.CabinetDoor, "1/030060", EnProductCategory.Standard, 5.1m, false),
                // CreateProduct(6, 30, 60, EnProductType.CabinetDoor, "2/030060", EnProductCategory.Classic, 5.1m, false),
                // CreateProduct(7, 30, 60, EnProductType.CabinetDoor, "4/030060", EnProductCategory.Gloss, 75.1m, false),
                // CreateProduct(8, 30, 80, EnProductType.CabinetDoor, "1/030080", EnProductCategory.Standard,  51.1m),
                // CreateProduct(9, 30, 80, EnProductType.CabinetDoor, "2/030080", EnProductCategory.Classic,  5.1m),
                // CreateProduct(10, 30, 80, EnProductType.CabinetDoor, "3/030080", EnProductCategory.Superior,  5.1m),
                // CreateProduct(11, 30, 80, EnProductType.CabinetDoor, "4/030080", EnProductCategory.Gloss,  65.1m),
                // CreateProduct(12, 30, 90, EnProductType.CabinetDoor, "1/030090", EnProductCategory.Standard, 5.7m),
                // CreateProduct(13, 30, 90, EnProductType.CabinetDoor, "2/030090", EnProductCategory.Classic,  15.1m),
                // CreateProduct(14, 30, 90, EnProductType.CabinetDoor, "3/030090", EnProductCategory.Superior, 5.1m),
                // CreateProduct(15, 30, 90, EnProductType.CabinetDoor, "4/030090", EnProductCategory.Gloss, 5.1m,
                //     false)
            };
            
            //dbContext.Products.AddRange(list);
        }
        
        // private static Product CreateProduct(int id, decimal height, decimal width, EnProductType productType, 
        //     string code, EnProductCategory productCategory, decimal price, bool active = true)
        // {
        //     return new Product()
        //     {
        //         Id = id,
        //         Height = height,
        //         Width = width,
        //         ProductType = productType,
        //         Code = code,
        //         ProductCategory = productCategory,
        //         Price = price,
        //         Active = active
        //     };
        // }
    }
}