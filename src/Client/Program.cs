using Client.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using System.Globalization;
using Microsoft.JSInterop;
using Client.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Add your Syncfusion license key for Blazor platform with corresponding Syncfusion NuGet version referred in project. For more information about license key see https://help.syncfusion.com/common/essential-studio/licensing/license-key.
            // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Add your license key here");
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDU1NTc3QDMxMzkyZTMxMmUzMGp6Uzc0bnVGcUdObDNIOVFBRlpXa3hWY0xlb0QwWVJGb3NGZ3dRN3IzUzg9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

            // Set the default culture of the application
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

                        // Get the modified culture from culture switcher
                        var host = builder.Build();
                        var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
                        var result = await jsInterop.InvokeAsync<string>("cultureInfo.get");
                        if (result != null)
                        {
                            // Set the culture from culture switcher
                            var culture = new CultureInfo(result);
                            CultureInfo.DefaultThreadCurrentCulture = culture;
                            CultureInfo.DefaultThreadCurrentUICulture = culture;
                        }

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
