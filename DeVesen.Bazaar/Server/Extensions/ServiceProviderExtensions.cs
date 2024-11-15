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
            var initialManufacturer = new[]
            {
                "00 Mitgebracht",
                "Rossignol",
                "Salomon",
                "Atomic",
                "Fischer",
                "Head",
                "Blizzard",
                "Völkl",
                "Nordica",
                "K2",
                "Dynafit",
                "Black Crows",
                "Elan",
                "Scott",
                "Tecnica",
                "Marker",
                "Leki",
                "Scarpa",
                "Ortovox",
                "Mammut",
                "Spyder",
                "Descente",
                "Kjus",
                "Burton",
                "Lib Tech",
                "GNU",
                "Ride Snowboards",
                "K2 Snowboarding",
                "Capita",
                "Salomon",
                "Nitro Snowboards",
                "Jones Snowboards",
                "Roxy",
                "Flow",
                "Union",
                "Northwave",
                "ThirtyTwo",
                "KHW",
                "Hamax",
                "Sirch",
                "Stiga",
                "Davoser",
                "AlpenGaudi",
                "EKO",
                "Berg Toys",
                "Gasser",
                "Trek",
                "Specialized",
                "Giant",
                "Canyon",
                "Bianchi",
                "Scott",
                "Cube",
                "Merida",
                "Cannondale",
                "Pinarello",
                "Colnago",
                "Focus",
                "Cervélo",
                "Shimano",
                "SRAM",
                "Campagnolo",
                "Pearl Izumi",
                "Castelli",
                "Gore Wear",
                "Assos",
                "Wilson",
                "Head",
                "Babolat",
                "Yonex",
                "Dunlop",
                "Prince",
                "Tecnifibre",
                "K-Swiss",
                "Lotto",
                "Nike",
                "Adidas",
                "Asics",
                "Nike",
                "Adidas",
                "Asics",
                "Brooks",
                "New Balance",
                "Hoka One One",
                "Salomon",
                "Saucony",
                "Altra",
                "Inov-8",
                "On Running",
                "Mizuno",
                "Puma",
                "The North Face",
                "Columbia",
                "Patagonia",
                "Mammut",
                "Arc'teryx",
                "Vaude",
                "Fjällräven",
                "Salomon",
                "Scarpa",
                "La Sportiva",
                "Merrell",
                "Lowa",
                "Haglöfs",
                "Jack Wolfskin",
                "Deuter",
                "Osprey",
                "Petzl",
                "Black Diamond",
                "Mammut",
                "Scarpa",
                "La Sportiva",
                "Edelrid",
                "Beal",
                "Grivel",
                "DMM",
                "Wild Country",
                "Metolius",
                "Ocún",
                "Speedo",
                "Arena",
                "TYR Sport",
                "Zoggs",
                "Aquasphere",
                "Finis",
                "Funky Trunks",
                "Mad Wave",
                "Michael Phelps (MP)",
                "Nike",
                "Adidas",
                "Puma",
                "Umbro",
                "Joma",
                "Kappa",
                "Hummel",
                "Erima",
                "Macron",
                "Uhlsport",
                "Mitre",
                "Select",
                "Nike",
                "Adidas",
                "Under Armour",
                "Spalding",
                "Wilson",
                "Jordan",
                "Molten",
                "Champion",
                "New Balance",
                "Converse",
                "Kempa",
                "Hummel",
                "Adidas",
                "Select",
                "Mizuno",
                "Salming",
                "Erima",
                "Uhlsport"
            };

            foreach (var item in initialManufacturer)
            {
                if (await storage.ExistByNameAsync(item))
                {
                    continue;
                }

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
                "Trikots",
                "Shorts",
                "Hosen",
                "Jacken",
                "T-Shirts",
                "Sweatshirts/Hoodies",
                "Sport-BHs",
                "Basecaps, Mützen",
                "Socken",
                "Handschuhe",
                "Schals und Halstücher",
                "Sportbrillen",
                "Laufschuhe",
                "Wanderschuhe",
                "Kletterschuhe",
                "Tennisschuhe",
                "Fußballschuhe",
                "Basketballschuhe",
                "Handballschuhe",
                "Skischuhe",
                "Snowboardboots",
                "Radsportschuhe",
                "Freizeitschuhe",
                "Helme",
                "Protektoren",
                "Handschuhe",
                "Schienbeinschoner",
                "Torwarthandschuhe",
                "Schwimmbrillen",
                "Neoprenanzüge",
                "Lawinen-Airbags",
                "Ski",
                "Snowboards",
                "Rodel/Schlitten",
                "Fahrräder",
                "Tennis-, Badminton-, Squash-Sc",
                "Fußbälle, Basketbälle, Handbäl",
                "Schwimmflossen",
                "Schwimmhilfen",
                "Kletterseile",
                "Klettergurte",
                "Kletterkarabiner und Sicherung",
                "Stöcke",
                "Schlägerbälle",
                "Rucksäcke",
                "Trinksysteme",
                "Zelte und Schlafsäcke",
                "Isomatten",
                "Trekkingstöcke",
                "Gamaschen",
                "Stirnlampen",
                "Campingkocher",
                "Fahrradkomponenten",
                "Fahrradhelme",
                "Fahrradbrillen",
                "Fahrradtrikots und Hosen",
                "Fahrradhandschuhe",
                "Fahrradsättel",
                "Fahrradcomputer (GPS)",
                "Fahrradpumpen",
                "Fahrradtaschen",
                "Flaschenhalter",
                "Schwimmanzüge",
                "Schwimmkappen",
                "Schwimmbrillen",
                "Flossen",
                "Paddles",
                "Schwimmbretter",
                "Pull Buoys",
                "Fitnessmatten",
                "Gewichte",
                "Resistance Bands",
                "Trainingshandschuhe",
                "Springseile",
                "Fitnessuhren",
                "Pulsmesser",
                "Handgelenksbandagen",
                "Kniebandagen",
                "Medizinbälle",
                "Fitnessgeräte",
                "Laufbänder",
                "Heimtrainer",
                "Crosstrainer",
                "Skibindungen",
                "Skistöcke",
                "Skihelme",
                "Schneeschuhe",
                "Schlitten und Rodel",
                "Snowboardbindungen",
                "Goggles",
                "Skibrillen",
                "Snowboardbrillen",
                "Torwarthandschuhe",
                "Sporttaschen",
                "Ballpumpen",
                "Fußballnetze",
                "Taktiktafeln",
                "Wasserflaschen / Trinkflaschen",
                "Pfeifen",
                "Schiedsrichterzubehör"
            };

            foreach (var item in initialCategories)
            {
                if (await storage.ExistByNameAsync(item))
                {
                    continue;
                }

                await storage.CreateAsync(new ArticleCategory
                {
                    Id = (Guid.NewGuid() + item).ToShortHash(),
                    Name = item
                });
            }
        }
    }
}
