using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Hubs;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared.Extensions;

namespace DeVesen.Bazaar.Server.Storage;

public class VendorStorage
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly VendorHubContext _vendorHubContext;

    public VendorStorage(IVendorRepository vendorRepository, IArticleRepository articleRepository, VendorHubContext vendorHubContext)
    {
        _vendorRepository = vendorRepository;
        _articleRepository = articleRepository;
        _vendorHubContext = vendorHubContext;
    }

    public async Task<bool> ExistByIdAsync(string id)
    {
        return await _vendorRepository.ExistByIdAsync(id);
    }

    public async Task<VendorEntity> GetAsync(string vendorId)
    {
        return await _vendorRepository.GetAsync(vendorId);
    }

    public async Task<IEnumerable<VendorEntity>> GetAllAsync(VendorFilter filter)
    {
        var elements = await _vendorRepository.GetAllAsync();

        if (string.IsNullOrWhiteSpace(filter.Id) is false)
        {
            elements = elements.Where(p => p.Id == filter.Id);
        }
        if (string.IsNullOrWhiteSpace(filter.SearchText) is false)
        {
            elements = elements.Where(p => p.FirstName.BiContainsIgnoreCase(filter.SearchText)
                                        || p.LastName.BiContainsIgnoreCase(filter.SearchText)
                                        || p.Address.BiContainsIgnoreCase(filter.SearchText)
                                        || p.Phone.BiContainsIgnoreCase(filter.SearchText)
                                        || p.EMail.BiContainsIgnoreCase(filter.SearchText)
                                        || p.Note.BiContainsIgnoreCase(filter.SearchText));
        }

        return elements.Select(p => p);
    }

    public async Task CreateAsync(VendorEntity element)
    {
        if (await _vendorRepository.ExistByIdAsync(element.Id))
        {
            throw new InvalidDataException($"Id '{element.Id}' already exist!");
        }

        await _vendorRepository.CreateAsync(element);

        await _vendorHubContext.SendAdded();
    }

    public async Task UpdateAsync(VendorEntity element)
    {
        if (await _vendorRepository.ExistByIdAsync(element.Id) is false)
        {
            throw new InvalidDataException($"Id '{element.Id}' not found!");
        }

        await _vendorRepository.UpdateAsync(element);

        await _vendorHubContext.SendUpdated(element.Id);
    }

    public async Task DeleteAsync(string id)
    {
        if (await _vendorRepository.ExistByIdAsync(id) is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        await DeleteRelatedArticles(id);

        await _vendorRepository.DeleteAsync(id);

        await _vendorHubContext.SendRemoved(id);
    }

    private async Task DeleteRelatedArticles(string vendorId)
    {
        var articles = await _articleRepository.GetAllAsync();
        var relatedArticles = articles.Where(p => p.VendorId == vendorId);

        foreach (var relatedArticle in relatedArticles)
        {
            await _articleRepository.DeleteAsync(relatedArticle.Id);
        }
    }
}