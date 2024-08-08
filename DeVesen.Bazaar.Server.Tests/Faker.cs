using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;

namespace DeVesen.Bazaar.Server.Tests;

[ExcludeFromCodeCoverage]
public static class Faker
{
    public static class Entity
    {
        public static ArticleCategoryEntity ArticleCategory1 => GetArticleCategoryEntity("1", "ArticleCategory_1");
        public static ArticleCategoryEntity ArticleCategory2 => GetArticleCategoryEntity("2", "ArticleCategory_2");
        public static ArticleCategoryEntity ArticleCategory3 => GetArticleCategoryEntity("3", "ArticleCategory_3");
        public static ArticleCategoryEntity GetArticleCategoryEntity(string? id = null, string? name = null)
            => new()
            {
                Id = id ?? "4",
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };

        public static ManufacturerEntity Manufacturer1 => GetManufacturerEntity("4", "Manufacturer_1");
        public static ManufacturerEntity Manufacturer2 => GetManufacturerEntity("5", "Manufacturer_2");
        public static ManufacturerEntity Manufacturer3 => GetManufacturerEntity("6", "Manufacturer_3");
        public static ManufacturerEntity GetManufacturerEntity(string? id = null, string? name = null)
            => new()
            {
                Id = id ?? "7",
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };

        public static ArticleEntity Article1 => GetArticleEntity(id: "eb913801", number: 1, title: "Article_1", vendorId: "61eb9e6e");
        public static ArticleEntity Article2 => GetArticleEntity(id: "9237bcb8", number: 2, title: "Article_2", vendorId: "9237bcb8");
        public static ArticleEntity Article3 => GetArticleEntity(id: "ab9258ee", number: 3, title: "Article_3", vendorId: "ab9258ee");
        public static ArticleEntity GetArticleEntity(string id = "c6580c62",
            string vendorId = "03b4cda6",
            long number = 1,
            string title = "title",
            string category = "category",
            string manufacturer = "manufacturer",
            double price01 = 90,
            double? price02 = null,
            string? description = "")
            => new()
            {
                Id = id,
                VendorId = vendorId,
                Number = number,
                Title = title,
                ArticleCategory = category,
                Manufacturer = manufacturer,
                Created = new DateTime(2024, 1, 1, 1, 1, 1),
                Price01 = price01,
                Price02 = price02,
                Description = description ?? string.Empty,
            };

        public static VendorEntity Vendor1 => GetVendorEntity(id: "61eb9e6e", firstName: "FirstName_1", lastName: "LastName_1");
        public static VendorEntity Vendor2 => GetVendorEntity(id: "9237bcb8", firstName: "FirstName_2", lastName: "LastName_2");
        public static VendorEntity Vendor3 => GetVendorEntity(id: "ab9258ee", firstName: "FirstName_3", lastName: "LastName_2");
        public static VendorEntity GetVendorEntity(string id = "03b4cda6",
            string salutation = "salutation#",
            string firstName = "firstName#",
            string lastName = "lastName#",
            string address = "address#",
            string eMail = "eMail#",
            string phone = "phone#")
            => new()
            {
                Id = id,
                Salutation = salutation,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                EMail = eMail,
                Phone = phone,
                Note = string.Empty
            };
    }

    public static class Domain
    {
        public static ArticleCategory ArticleCategory1 => GetArticleCategory("1", "ArticleCategory_1");
        public static ArticleCategory ArticleCategory2 => GetArticleCategory("2", "ArticleCategory_2");
        public static ArticleCategory GetArticleCategory(string? id = null, string? name = null)
            => new()
            {
                Id = id ?? "4",
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };

        public static Manufacturer Manufacturer1 => GetManufacturer("4", "Manufacturer_1");
        public static Manufacturer Manufacturer2 => GetManufacturer("5", "Manufacturer_2");
        public static Manufacturer GetManufacturer(string? id = null, string? name = null)
            => new()
            {
                Id = id ?? "7",
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };

        public static Article Article1 => GetArticle(vendorId: "61eb9e6e", id: "eb913801", number: 1, title: "Article_1");
        public static Article Article2 => GetArticle(vendorId: "9237bcb8", id: "9237bcb8", number: 2, title: "Article_2");
        public static Article GetArticle(string id = "c6580c62",
            string vendorId = "03b4cda6",
            long number = 1,
            string title = "title",
            string category = "category",
            string manufacturer = "manufacturer",
            double price01 = 90,
            double? price02 = null,
            string? description = "")
            => new()
            {
                Id = id,
                VendorId = vendorId,
                Number = number,
                Title = title,
                ArticleCategory = category,
                Manufacturer = manufacturer,
                Created = new DateTime(2024, 1, 1, 1, 1, 1),
                Price01 = price01,
                Price02 = price02,
                Description = description ?? string.Empty
            };

        public static Vendor Vendor1 => GetVendor(id: "61eb9e6e", firstName: "FirstName_1", lastName: "LastName_1");
        public static Vendor Vendor2 => GetVendor(id: "9237bcb8", firstName: "FirstName_2", lastName: "LastName_2");
        public static Vendor GetVendor(string id = "03b4cda6",
                                       string salutation = "salutation#",
                                       string firstName = "firstName#",
                                       string lastName = "lastName#",
                                       string address = "address#",
                                       string eMail = "eMail#",
                                       string phone = "phone#")
            => new()
            {
                Id = id,
                Salutation = salutation,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                EMail = eMail,
                Phone = phone,
                Note = string.Empty
            };
    }

    public static class Dto
    {
        public static ArticleCategory ArticleCategory1 => GetArticleCategory("1", "ArticleCategory_1");
        public static ArticleCategory ArticleCategory2 => GetArticleCategory("2", "ArticleCategory_2");
        public static ArticleCategory GetArticleCategory(string? id = null, string? name = null)
            => new()
            {
                Id = id ?? "4",
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };

        public static ArticleCategoryCreateDto ArticleCategoryCreate01 => GetArticleCategoryCreate("ArticleCategory_1");
        public static ArticleCategoryCreateDto ArticleCategoryCreate02 => GetArticleCategoryCreate("ArticleCategory_2");
        public static ArticleCategoryCreateDto GetArticleCategoryCreate(string? name = null)
            => new()
            {
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };

        public static ArticleCategoryUpdateDto ArticleCategoryUpdate01 => GetArticleCategoryUpdate("ArticleCategory_1");
        public static ArticleCategoryUpdateDto ArticleCategoryUpdate02 => GetArticleCategoryUpdate("ArticleCategory_2");
        public static ArticleCategoryUpdateDto GetArticleCategoryUpdate(string? name = null)
            => new()
            {
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };


        public static ManufacturerEntity Manufacturer1 => GetManufacturerEntity("4", "Manufacturer_1");
        public static ManufacturerEntity Manufacturer2 => GetManufacturerEntity("5", "Manufacturer_2");
        public static ManufacturerEntity GetManufacturerEntity(string? id = null, string? name = null)
            => new()
            {
                Id = id ?? "7",
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };

        public static ManufacturerCreateDto ManufacturerCreate01 => GetManufacturerCreate("Manufacturer_1");
        public static ManufacturerCreateDto ManufacturerCreate02 => GetManufacturerCreate("Manufacturer_2");
        public static ManufacturerCreateDto GetManufacturerCreate(string? name = null)
            => new()
            {
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };

        public static ManufacturerUpdateDto ManufacturerUpdate01 => GetManufacturerUpdate("Manufacturer_1");
        public static ManufacturerUpdateDto ManufacturerUpdate02 => GetManufacturerUpdate("Manufacturer_2");
        public static ManufacturerUpdateDto GetManufacturerUpdate(string? name = null)
            => new()
            {
                Name = name ?? $"AC_{Guid.NewGuid()}"
            };


        public static ArticleCreateDto ArticleCreate01 => GetArticleCreate(number: 1, title: "Article_1", vendorId: "61eb9e6e");
        public static ArticleCreateDto ArticleCreate02 => GetArticleCreate(number: 2, title: "Article_2", vendorId: "9237bcb8");
        public static ArticleCreateDto GetArticleCreate(string vendorId = "03b4cda6",
            long number = 1,
            string title = "title",
            string category = "category",
            string manufacturer = "manufacturer",
            double price01 = 90,
            double? price02 = null,
            string description = "")
            => new()
            {
                VendorId = vendorId,
                Number = number,
                Title = title,
                ArticleCategory = category,
                Manufacturer = manufacturer,
                Price01 = price01,
                Price02 = price02,
                Description = description
            };

        public static ArticleUpdateDto ArticleUpdate01 => GetArticleUpdate(number: 1, title: "Article_1", vendorId: "61eb9e6e");
        public static ArticleUpdateDto ArticleUpdate02 => GetArticleUpdate(number: 2, title: "Article_2", vendorId: "9237bcb8");
        public static ArticleUpdateDto GetArticleUpdate(string vendorId = "03b4cda6",
            long number = 1,
            string title = "title",
            string category = "category",
            string manufacturer = "manufacturer",
            double price01 = 90,
            double? price02 = null,
            string description = "")
            => new()
            {
                VendorId = vendorId,
                Number = number,
                Title = title,
                ArticleCategory = category,
                Manufacturer = manufacturer,
                Created = new DateTime(2024, 1, 1, 1, 1, 1),
                Price01 = price01,
                Price02 = price02,
                Description = description
            };


        public static VendorDto Vendor1 => GetVendorEntity(id: "61eb9e6e", firstName: "FirstName_1", lastName: "LastName_1");
        public static VendorDto Vendor2 => GetVendorEntity(id: "9237bcb8", firstName: "FirstName_2", lastName: "LastName_2");
        public static VendorDto GetVendorEntity(string id = "03b4cda6",
            string salutation = "salutation#",
            string firstName = "firstName#",
            string lastName = "lastName#",
            string address = "address#",
            string eMail = "eMail#",
            string phone = "phone#")
            => new()
            {
                Id = id,
                Salutation = salutation,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                EMail = eMail,
                Phone = phone
            };

        public static VendorCreateDto VendorCreate1 => GetVendorCreate(firstName: "FirstName_1", lastName: "LastName_1");
        public static VendorCreateDto VendorCreate2 => GetVendorCreate(firstName: "FirstName_2", lastName: "LastName_2");
        public static VendorCreateDto GetVendorCreate(
            string salutation = "salutation#",
            string firstName = "firstName#",
            string lastName = "lastName#",
            string address = "address#",
            string eMail = "eMail#",
            string phone = "phone#")
            => new()
            {
                Salutation = salutation,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                EMail = eMail,
                Phone = phone
            };

        public static VendorUpdateDto VendorUpdate1 => GetVendorUpdate(firstName: "FirstName_1", lastName: "LastName_1");
        public static VendorUpdateDto VendorUpdate2 => GetVendorUpdate(firstName: "FirstName_2", lastName: "LastName_2");
        public static VendorUpdateDto GetVendorUpdate(
            string salutation = "salutation#",
            string firstName = "firstName#",
            string lastName = "lastName#",
            string address = "address#",
            string eMail = "eMail#",
            string phone = "phone#")
            => new()
            {
                Salutation = salutation,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                EMail = eMail,
                Phone = phone
            };
    }
}