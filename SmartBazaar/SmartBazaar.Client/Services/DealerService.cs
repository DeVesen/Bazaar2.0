using SmartBazaar.Client.States.DealerInfoState;
using SmartBazaar.Shared;
using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Extensions;
using SmartBazaar.Shared.Models;
using SmartBazaar.Shared.Types;

namespace SmartBazaar.Client.Services;

public class DealerService(DealerInfoFacade dealerFacade) : IDealerService
{
    private readonly List<DealerModel> _innerList =
    [
        CreateDealerModel_Company("9862F036-6916-47B0-BA4E-CF57187C0F9A", "", "Sportschick"),
        CreateDealerModel_Private("E5E51748-BDFF-445A-85B4-C9EFAB6E808F", "Herbert", "Müller"),
        CreateDealerModel_Association("23E2903D-0D02-4632-85E5-374F62FE6BA5", "Sven", "Reichert"),
    ];


    public Task<IResult> ExistByIdAsync(string value)
    {
        var found = _innerList.Any(p => p.Id.Equals(value, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(Result.Create(found, found ? string.Empty : "Item not found"));
    }public async Task<IResult<DealerModel>> GetByIdAsync(string dealerId)
    {
        var result = _innerList.FirstOrDefault(p => p.Id.Equals(dealerId, StringComparison.OrdinalIgnoreCase));
        var errorMessage = result != null
            ? "Element not found!"
            : string.Empty;

        return await Task.FromResult(Result<DealerModel>.Create(result != null, errorMessage, result));
    }

    public async Task<IResult<DealerModel[]>> GetAllAsync()
    {
        var result = _innerList.AsEnumerable();

        return await Task.FromResult(Result<DealerModel[]>.Success(result.ToArray()));
    }


    public Task<IResult<DealerModel>> CreateAsync(DealerCreateModel data)
    {
        var model = new DealerModel
        {
            Id = Guid.NewGuid().ToString(),
        }.Adapt(data);

        _innerList.Add(model);

        dealerFacade.ReLoadDealer(model);

        return Task.FromResult(Result<DealerModel>.Success(model));
    }

    public Task<IResult<DealerModel>> UpdateAsync(DealerModel data)
    {
        var index = _innerList.FindIndex(p => p.Id.Equals(data.Id, StringComparison.OrdinalIgnoreCase));

        if (index < 0)
        {
            return Task.FromResult(Result<DealerModel>.Fail("Element not found!"));
        }

        _innerList[index] = data;

        dealerFacade.ReLoadDealer(data);

        return Task.FromResult(Result<DealerModel>.Success(data));
    }

    public Task<IResult> DeleteAsync(string id)
    {
        _innerList.RemoveAll(p => p.Id == id);

        dealerFacade.RemoveDealer(id);

        return Task.FromResult(Result.Success());
    }


    private static DealerModel CreateDealerModel_Company(string id, string firstName, string lastName)
        => new()
        {
            Id = id,
            Name = new()
            {
                FirstName = firstName,
                LastName = lastName
            },
            SalesShareRatio = new()
            {
                Type = DealerTypes.Company,
                SalesShare = 0.10d,
                OfferUnitPrice = 0.0d
            }
        };

    private static DealerModel CreateDealerModel_Private(string id, string firstName, string lastName)
        => new()
        {
            Id = id,
            Name = new()
            {
                FirstName = firstName,
                LastName = lastName
            },
            SalesShareRatio = new()
            {
                Type = DealerTypes.Private,
                SalesShare = 0.15d,
                OfferUnitPrice = 0.50d
            }
        };

    private static DealerModel CreateDealerModel_Association(string id, string firstName, string lastName)
        => new()
        {
            Id = id,
            Name = new()
            {
                FirstName = firstName,
                LastName = lastName
            },
            SalesShareRatio = new()
            {
                Type = DealerTypes.Association,
                SalesShare = 0.0d,
                OfferUnitPrice = 0.0d
            }
        };
}