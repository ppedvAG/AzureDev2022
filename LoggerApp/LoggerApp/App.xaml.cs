using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LoggerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {


            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Debug()
                            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Minute)
                            .WriteTo.Seq("http://20.113.5.155/")
                            .CreateLogger();


            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal("Crash {@Error}", e.ExceptionObject);
            Log.CloseAndFlush();
        }
    }
}
