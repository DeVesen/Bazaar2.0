using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Contracts;

public interface IDealerService
{
    public Task<IResult> ExistByIdAsync(string value);

    public Task<IResult<DealerModel>> GetByIdAsync(string dealerId);
    public Task<IResult<DealerModel[]>> GetAllAsync();

    public Task<IResult<DealerModel>> CreateAsync(DealerCreateModel data);
    public Task<IResult<DealerModel>> UpdateAsync(DealerModel data);
    public Task<IResult> DeleteAsync(string id);
}