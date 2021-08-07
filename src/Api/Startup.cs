using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(OpenNasa.Apod.Api.Startup))]

namespace OpenNasa.Apod.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IApodPicturesData, ApodPicturesData>();
        }
    }
}
