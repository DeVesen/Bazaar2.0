using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using DeVesen.Bazaar.Shared.Events;

namespace DeVesen.Bazaar.Client.Extensions
{
    public static class HubConnectionProviderExtensions
    {
        public static VendorHubConnectionService CreateVendorHubConnectionService(IServiceProvider provider)
        {
            var navigationManager = provider.GetService<NavigationManager>();

            var hubUri = navigationManager?.ToAbsoluteUri("/DataSync/Vendor")
                         ?? throw new InvalidOperationException("NavigationManager not initialized yet!");

            return new VendorHubConnectionService(hubUri);
        }

        public static ArticleHubConnectionService CreateArticleHubConnectionService(IServiceProvider provider)
        {
            var navigationManager = provider.GetService<NavigationManager>();

            var hubUri = navigationManager?.ToAbsoluteUri("/DataSync/Article")
                         ?? throw new InvalidOperationException("NavigationManager not initialized yet!");

            return new ArticleHubConnectionService(hubUri);
        }

        public static ManufacturerHubConnectionService CreateManufacturerHubConnectionService(IServiceProvider provider)
        {
            var navigationManager = provider.GetService<NavigationManager>();

            var hubUri = navigationManager?.ToAbsoluteUri("/DataSync/Manufacturer")
                         ?? throw new InvalidOperationException("NavigationManager not initialized yet!");

            return new ManufacturerHubConnectionService(hubUri);
        }

        public static ArticleCategoryHubConnectionService CreateArticleCategoryHubConnectionService(IServiceProvider provider)
        {
            var navigationManager = provider.GetService<NavigationManager>();

            var hubUri = navigationManager?.ToAbsoluteUri("/DataSync/ArticleCategory")
                         ?? throw new InvalidOperationException("NavigationManager not initialized yet!");

            return new ArticleCategoryHubConnectionService(hubUri);
        }
    }

    public class VendorHubConnectionService(Uri uri)
    {
        private readonly HubConnection _connection = new HubConnectionBuilder()
            .WithUrl(uri)
            .WithAutomaticReconnect()
            .Build();

        public async Task StartAsync()
            => await _connection.StartAsync();

        public async ValueTask StopAsync()
        {
            await _connection.DisposeAsync();
        }

        public IDisposable RegisterOnAdded(Action<VendorAddedArgs> handler)
        {
            return _connection.On("Added", handler);
        }

        public IDisposable RegisterOnUpdated(Action<VendorUpdatedArgs> handler)
        {
            return _connection.On("Updated", handler);
        }

        public IDisposable RegisterOnRemoved(Action<VendorRemovedArgs> handler)
        {
            return _connection.On("Removed", handler);
        }
    }

    public class ArticleHubConnectionService(Uri uri)
    {
        private readonly HubConnection _connection = new HubConnectionBuilder()
            .WithUrl(uri)
            .WithAutomaticReconnect()
            .Build();

        public async Task StartAsync()
            => await _connection.StartAsync();

        public async ValueTask StopAsync()
            => await _connection.DisposeAsync();

        public IDisposable RegisterOnAdded(Action<ArticleAddedInfo> handler)
        {
            return _connection.On("Added", handler);
        }

        public IDisposable RegisterOnUpdated(Action<ArticleUpdatedInfo> handler)
        {
            return _connection.On("Updated", handler);
        }

        public IDisposable RegisterOnRemoved(Action<ArticleRemovedInfo> handler)
        {
            return _connection.On("Removed", handler);
        }

        public IDisposable RegisterOnStatusChanged(Action<ArticleStatusChangedInfo> handler)
        {
            return _connection.On("StatusChanged", handler);
        }
    }

    public class ManufacturerHubConnectionService(Uri uri)
    {
        private readonly HubConnection _connection = new HubConnectionBuilder()
            .WithUrl(uri)
            .WithAutomaticReconnect()
            .Build();

        public async Task StartAsync()
            => await _connection.StartAsync();

        public async ValueTask StopAsync()
            => await _connection.DisposeAsync();

        public IDisposable RegisterOnAdded(Action handler)
        {
            return _connection.On("Added", handler);
        }

        public IDisposable RegisterOnUpdated(Action<string> handler)
        {
            return _connection.On("Updated", handler);
        }

        public IDisposable RegisterOnRemoved(Action<string> handler)
        {
            return _connection.On("Removed", handler);
        }
    }

    public class ArticleCategoryHubConnectionService(Uri uri)
    {
        private readonly HubConnection _connection = new HubConnectionBuilder()
            .WithUrl(uri)
            .WithAutomaticReconnect()
            .Build();

        public async Task StartAsync()
            => await _connection.StartAsync();

        public async ValueTask StopAsync()
            => await _connection.DisposeAsync();

        public IDisposable RegisterOnAdded(Action handler)
        {
            return _connection.On("Added", handler);
        }

        public IDisposable RegisterOnUpdated(Action<string> handler)
        {
            return _connection.On("Updated", handler);
        }

        public IDisposable RegisterOnRemoved(Action<string> handler)
        {
            return _connection.On("Removed", handler);
        }
    }
}
