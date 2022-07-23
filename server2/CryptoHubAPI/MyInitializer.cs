using Infrastructure.Setting;

namespace CryptoHubAPI
{
    public class MyInitializer : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Do your startup work here
            await Doppler.FetchSecretsAsync();

            return;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // We have to implement this method too, because it is in the interface

            return Task.CompletedTask;
        }
    }
}
