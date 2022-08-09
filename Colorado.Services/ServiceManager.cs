using Colorado.Services.Gdi32;
using Colorado.Services.Kernel32;
using Colorado.Services.Logger;
using Colorado.Services.Math;
using Colorado.Services.User32;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Windows;

namespace Colorado.Services
{
    public interface IServiceManager
    {
        IServiceProvider ServiceProvider { get; }

        void Dispose();
    }

    public class ServiceManager : IDisposable, IServiceManager
    {
        private static IServiceManager _host;
        private IServiceProvider _serviceProvider;

        private ServiceManager()
        {
            ServiceProvider = AddServices(new ServiceCollection()).BuildServiceProvider();

            AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandledException;
        }

        public static IServiceManager Instance
        {
            get
            {
                if (_host == null)
                {
                    _host = new ServiceManager();
                }

                return _host;
            }
        }

        public IServiceProvider ServiceProvider
        {
            get
            {
                EnsureNotDisposed();
                return _serviceProvider;
            }
            private set => _serviceProvider = value;
        }

        #region IDisposable

        private bool _isDisposed;

        private void EnsureNotDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(ServiceManager));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                AppDomain.CurrentDomain.UnhandledException -= OnAppDomainUnhandledException;

                if (ServiceProvider is IDisposable disposableServiceProvider)
                {
                    disposableServiceProvider.Dispose();
                }
                ServiceProvider = null;
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(((Exception)e.ExceptionObject).Message);
        }

        private IServiceCollection AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IUser32Service, User32Service>();
            serviceCollection.TryAddSingleton<IKernel32Service, Kernel32Service>();
            serviceCollection.TryAddSingleton<IGdi32Service, Gdi32Service>();
            serviceCollection.TryAddSingleton<ILoggerService, LoggerService>();
            serviceCollection.TryAddSingleton<IMathService, MathService>();

            return serviceCollection;
        }
    }
}
