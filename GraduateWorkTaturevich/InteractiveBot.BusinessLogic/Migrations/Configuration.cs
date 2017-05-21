using BusinessLogic.Infrastructure.DAL;
using BusinessLogic.Entities.FactoryDomain;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace BusinessLogic.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlDbContext context)
        {
            context.Categories.AddOrUpdate(new Category
            {
                Name = "Заготовка",
                ImageName = "workpieceBMZ.png",
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Заготовка непрерывнолитая (блюм)",
                        Mark = "S235JR, S275JR, S355J2",
                        Sortament = "▭ 250×300, L=4000-5400 mm",
                        Specification = "DIN EN 10025-1:2005, DIN EN 10025-2:2005",
                        ImageName = "workpieceBMZ.png"
                    },
                    new Product
                    {
                        Name = "Заготовка непрерывнолитая (круглая)",
                        Mark = "хим.состав по ГОСТ 1050-2013, ГОСТ 4543-71, DIN EN 10025-1:2005, DIN EN 10025-2:2005, DIN EN 10083-3:2007",
                        Sortament = "Ø 200",
                        Specification = "Спецификация №1061-0/СС-2010",
                        ImageName = "workpiece1BMZ.png"
                    },
                    new Product
                    {
                        Name = "Заготовка непрерывнолитая",
                        Mark = "30Г2, 30Г, 35Г",
                        Sortament = "125×125, L=8000-12000 mm 140×140, L=8000-12000 mm",
                        Specification = "	ГОСТ 4543-71",
                        ImageName = "workpieceBMZ.png"
                    },
                }
            });
            context.Categories.AddOrUpdate(new Category
            {
                Name = "Прокат",
                ImageName = "prokatBMZ.png",
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Арматура напрягаемая периодического профиля класса S800 в стержнях",
                        Sortament = "Ø  10, 12, 14 mm",
                        Mark = "хим.состав по СТБ 1706-2006",
                        Specification = "СТБ 1706-2006",
                        ImageName = "prokatBMZ.png"
                    },
                    new Product
                    {
                        Name = "Арматурная сталь класса B500NA",
                        Sortament = "Ø 5,0; 6,0; 8,0; 10,0; 12,0 mm",
                        Mark = "хим.состав по DIN 488-1:2009",
                        Specification = "DIN 488-1, 3:2009, DIN 488-6:2010",
                        ImageName = "prokat1BMZ.png"
                    },
                    new Product
                    {
                        Name = "Арматурная сталь класса B500NA",
                        Sortament = "Ø 5,0; 6,0; 8,0; 10,0; 12,0 mm",
                        Mark = "хим.состав по NS 3576-1:2005",
                        Specification = "NS 3576-1:2005, NS EN 10080:2005",
                        ImageName = "prokat2BMZ.png"
                    }
                }
            });
            context.Categories.AddOrUpdate(new Category
            {
                Name = "Трубы бесшовные",
                ImageName = "pipesBMZ.png",
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Трубы стальные бесшовные горячедеформированные для нефте- и газопроводов",
                        Sortament = "60,0-168,0 × 4,0-16,0; 60,3-168,3 × 3,18-21,95 mm",
                        Mark = "углеродистые, легированные",
                        Specification = "ГОСТ 550-75",
                        ImageName = "pipesBMZ.png"
                    },
                    new Product
                    {
                        Name = "Трубы стальные бесшовные горячедеформированные для нефте- и газопроводов",
                        Sortament = "60,32-168,28 × 4,83-1углеродистые, легированныеуглеродистые, легированные",
                        Specification = "API Spec5CT(9р.)-2011",
                        Mark = "углеродистые, легированные",
                        ImageName = "pipesBMZ.png"
                    },
                    new Product
                    {
                        Name = "Трубы стальные бесшовные горячедеформированные для нефте- и газопроводов",
                        Sortament = "21,3-168,3 × 2,41-21,95 mm",
                        Mark ="углеродистые, легированные",
                        Specification = "ASTM A106/A106M-15; ASTM A53/A53M-12; ASME B36.10M-2004",
                        ImageName = "pipesBMZ.png"
                    }
                }
            });
            context.SaveChanges();
        }
    }
}
