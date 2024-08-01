using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Shared.Basics;

namespace DeVesen.Bazaar.Server.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static async Task SetupAppEnvironment(this IServiceProvider services)
        {
            CheckDataDirectory();

            await services.GetService<ManufacturerStorage>()!.InitializeStorage();
            await services.GetService<ArticleCategoryStorage>()!.InitializeStorage();

            await Task.CompletedTask;
        }

        private static void CheckDataDirectory()
        {
            if (Directory.Exists(AppEnvironment.DataDirectory))
            {
                return;
            }

            Directory.CreateDirectory(AppEnvironment.DataDirectory);
        }

        public static async Task InitializeStorage(this ManufacturerStorage storage)
        {
            var initialCategories = new[]
            {
                "Nike",
                "Adidas",
                "Puma",
                "Under Armour",
                "Reebok",
                "Asics",
                "New Balance",
                "Columbia Sportswear",
                "The North Face",
                "Oakley",
                "Ray-Ban",
                "Garmin",
                "Suunto",
                "Trek",
                "Giant",
                "Specialized",
                "Cannondale",
                "Scott",
                "Brooks",
                "Saucony",
                "Salomon",
                "Atomic",
                "Rossignol",
                "Fischer",
                "Head",
                "Speedo",
                "Arena",
                "Patagonia",
                "Mammut"
            };

            foreach (var item in initialCategories)
            {
                await storage.CreateAsync(new Manufacturer
                {
                    Id = (Guid.NewGuid() + item).ToShortHash(),
                    Name = item
                });
            }
        }

        public static async Task InitializeStorage(this ArticleCategoryStorage storage)
        {
            var initialCategories = new[]
            {
                "Sportbekleidung",
                "Schuhe",
                "Zubehör",
                "Rucksäcke",
                "Brillen",
                "Uhren",
                "Elektronik",
                "Laufen",
                "Schwimmen",
                "Radfahren",
                "Wintersport",
                "Bergsteigen",
                "Klettern",
                "Wandern",
                "Wassersport",
                "Reiten",
                "Fußball",
                "Basketball",
                "Tennis",
                "Golf",
                "Camping",
                "Angeln",
                "Jagd",
                "Fliegen"
            };

            foreach (var item in initialCategories)
            {
                await storage.CreateAsync(new ArticleCategory
                {
                    Id = (Guid.NewGuid() + item).ToShortHash(),
                    Name = item
                });
            }
        }
    }
}
