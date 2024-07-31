namespace DeVesen.Bazaar.Server.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static async Task SetupAppEnvironment(this IServiceProvider services)
        {
            CheckDataDirectory();

            await Task.CompletedTask;
        }

        private static void CheckDataDirectory()
        {
            if (Directory.Exists(AppEnvironment.DataDirectory) is false)
            {
                Directory.CreateDirectory(AppEnvironment.DataDirectory);
            }
        }
    }
}
