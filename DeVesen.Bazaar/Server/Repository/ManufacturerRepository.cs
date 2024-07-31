using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Server.Repository;

[ExcludeFromCodeCoverage]
public class ManufacturerRepository : IManufacturerRepository
{
    private readonly List<ManufacturerEntity> _innerList = new()
    {
        new ManufacturerEntity
        {
            Id = Guid.Parse("f1a3d97b-4d3b-4c1b-9a9b-1c2d4b8e5a3e"),
            Name = "Nike"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f2b4e18c-5d4c-5d2c-a0b1-2d3e5c9f6b4f"),
            Name = "Adidas"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f3c5f29d-6e5d-6e3d-b1c2-3e4f6d0g7c5g"),
            Name = "Puma"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f4d6g30e-7f6e-7f4e-c2d3-4f5g7h1h8d6h"),
            Name = "Under Armour"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f5e7h41f-8g7f-8g5f-d3e4-5g6h8i2i9e7i"),
            Name = "Reebok"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f6f8i52g-9h8g-9h6g-e4f5-6h7i9j3j0f8j"),
            Name = "Asics"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f7g9j63h-0i9h-0i7h-f5g6-7i8j0k4k1g9k"),
            Name = "New Balance"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f8h0k74i-1j0i-1j8i-g6h7-8j9k1l5l2h0l"),
            Name = "Columbia Sportswear"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f9i1l85j-2k1j-2k9j-h7i8-9k0l2m6m3i1m"),
            Name = "The North Face"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f0j2m96k-3l2k-3l0k-i8j9-0l3m7n7j2i2n"),
            Name = "Oakley"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f1k3n07l-4m3l-4m1l-j9k0-1m4n8o8k3j3o"),
            Name = "Ray-Ban"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f2l4o18m-5n4m-5n2m-k0l1-2n5o9p9l4k4p"),
            Name = "Garmin"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f3m5p29n-6o5n-6o3n-l1m2-3o6p0q0m5l5q"),
            Name = "Suunto"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f4n6q30o-7p6o-7p4o-m2n3-4p7q1r1n6m6r"),
            Name = "Trek"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f5o7r41p-8q7p-8q5p-n3o4-5q8r2s2o7n7s"),
            Name = "Giant"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f6p8s52q-9r8q-9r6q-o4p5-6r9s3t3p8o8t"),
            Name = "Specialized"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f7q9t63r-0s9r-0s7r-p5q6-7s0t4u4q9p9u"),
            Name = "Cannondale"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f8r0u74s-1t0s-1t8s-q6r7-8t1u5v5r0q0v"),
            Name = "Scott"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f9s1v85t-2u1t-2u9t-r7s8-9u2v6w6s1r1w"),
            Name = "Brooks"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f0t2w96u-3v2u-3v0u-s8t9-0v3w7x7t2s2x"),
            Name = "Saucony"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f1u3x07v-4w3v-4w1v-t9u0-1w4x8y8u3t3y"),
            Name = "Salomon"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f2v4y18w-5x4w-5x2w-u0v1-2x5y9z9v4u4z"),
            Name = "Atomic"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f3w5z29x-6y5x-6y3x-v1w2-3y6z0a0w5v5a"),
            Name = "Rossignol"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f4x6a30y-7z6y-7z4y-w2x3-4z7a1b1x6w6b"),
            Name = "Fischer"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f5y7b41z-8a7z-8a5z-x3y4-5a8b2c2y7x7c"),
            Name = "Head"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f6z8c52a-9b8a-9b6a-y4z5-6b9c3d3z8y8d"),
            Name = "Speedo"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f7a9d63b-0c9b-0c7b-z5a6-7c0d4e4a9z9e"),
            Name = "Arena"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f8b0e74c-1d0c-1d8c-a6b7-8d1e5f5b0a0f"),
            Name = "Patagonia"
        },
        new ManufacturerEntity
        {
            Id = Guid.Parse("f9c1f85d-2e1d-2e9d-b7c8-9e2f6g6c1b1g"),
            Name = "Mammut"
        }
    };

    public async Task<bool> ExistAsync(Guid id)
    {
        await Task.Delay(1);

        return _innerList.Any(p => p.Id == id);
    }

    public async Task<bool> ExistAsync(string name, Guid? allowedId = null)
    {
        await Task.Delay(1);

        return _innerList.Any(p => p.Name == name && p.Id != allowedId);
    }

    public async Task<ManufacturerEntity> GetAsync(Guid id)
    {
        await Task.Delay(1);

        return _innerList.First(p => p.Id == id);
    }

    public async Task<ManufacturerEntity> GetAsync(string name)
    {
        await Task.Delay(1);

        return _innerList.First(p => p.Name == name);
    }

    public async Task<IEnumerable<ManufacturerEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return _innerList.AsEnumerable();
    }

    public async Task CreateAsync(ManufacturerEntity entity)
    {
        _innerList.Add(entity);

        await Task.Delay(100);
    }

    public async Task UpdateAsync(ManufacturerEntity entity)
    {
        var elementIndex = _innerList.FindIndex(p => p.Id == entity.Id);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException(ResourceText.Transform(ResourceText.Manufacturer.NotFoundById, _ => entity.Id));
        }

        _innerList[elementIndex] = entity;

        await Task.Delay(100);
    }

    public async Task DeleteAsync(Guid id)
    {
        var element = _innerList.FirstOrDefault(p => p.Id == id);

        if (element == null)
        {
            throw new InvalidOperationException(ResourceText.Transform(ResourceText.Manufacturer.NotFoundById, _ => id));
        }

        _innerList.Remove(element);

        await Task.Delay(100);
    }
}