using BatchDecorator.API.Decorators;
using BatchDecorator.API.Enums;
using BatchDecorator.API.Models;
using BatchDecorator.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BatchDecorator.API.Helpers
{
    internal static class BatchDecoratorHelper
    {
        public static BaseDecorator Decorate<T>(this IBatchProcess component, DecoratorType decorator) where T : BaseDecorator
        {
            switch (decorator)
            {
                case DecoratorType.FileDownload:
                    return ActivatorUtilities.CreateInstance(Startup.ServiceProvider
                        , typeof(T), new object[]
                        {
                            component
                            , Startup.ServiceProvider.GetService<IOptions<FTPConfigModel>>()
                            , Startup.ServiceProvider.GetService<IHostEnvironment>()
                        }) as BaseDecorator;
                case DecoratorType.ExecuteSP:
                    return ActivatorUtilities.CreateInstance(Startup.ServiceProvider
                        , typeof(T), new object[]
                        {
                            component
                            , Startup.ServiceProvider.GetService<IConfiguration>()
                        }) as BaseDecorator;
                case DecoratorType.FileUpload:
                    return ActivatorUtilities.CreateInstance(Startup.ServiceProvider
                        , typeof(T), new object[]
                        {
                            component
                            , Startup.ServiceProvider.GetService<IOptions<FTPConfigModel>>()
                            , Startup.ServiceProvider.GetService<IConfiguration>()
                            , Startup.ServiceProvider.GetService<IHostEnvironment>()
                        }) as BaseDecorator;
                default:
                    return ActivatorUtilities.CreateInstance(Startup.ServiceProvider
                        , typeof(T), new object[]
                        {
                            component
                            , Startup.ServiceProvider.GetService<IOptions<FTPConfigModel>>()
                            , Startup.ServiceProvider.GetService<IHostEnvironment>()
                        }) as BaseDecorator;
            }

        }
    }
}
