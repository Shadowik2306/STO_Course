using STO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace STOView
{
	internal static class Program
	{
		private static ServiceProvider? _serviceProvider;

		public static ServiceProvider? ServiceProvider => _serviceProvider;

		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font;
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			var services = new ServiceCollection();
			ConfigureServices(services);
			_serviceProvider = services.BuildServiceProvider();
            System.Windows.Forms.Application.Run(_serviceProvider.GetRequiredService<Form1>());
		}

		private static void ConfigureServices(ServiceCollection services)
		{
			services.AddLogging(option =>
			{
				option.SetMinimumLevel(LogLevel.Information);
				option.AddNLog("nlog.config");
			});

            services.AddTransient<Form1>();
        }
	}
}