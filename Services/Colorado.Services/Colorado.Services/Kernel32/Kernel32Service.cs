using Microsoft.Extensions.DependencyInjection;
using System;

namespace Colorado.Services.Kernel32
{
    public interface IKernel32Service
    {
        IntPtr LoadLibrary(string libraryName);
    }

    public class Kernel32Service : IKernel32Service
    {
        public static IKernel32Service Instance => Host.Instance.ServiceProvider.GetService<IKernel32Service>();

        public IntPtr LoadLibrary(string libraryName)
        {
            return Kernel32LibraryAPI.LoadLibrary(libraryName);
        }
    }
}
