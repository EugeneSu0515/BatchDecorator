using System;
using System.Collections.Generic;
using BatchDecorator.API.Enums;
using BatchDecorator.API.Models;
using BatchDecorator.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;

namespace BatchDecorator.API.Decorators
{
    public class DecoratorFactory
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public IBatchProcess Component
        { get; private set; }

        public List<DecoratorType> DecoratorsSequence
        { get; set; }


        public DecoratorFactory(IBatchProcess component)
        {
            Component = component;
        }

        public IBatchProcess GetBatchProcess()
        {
            IBatchProcess result = Component;
            if (DecoratorsSequence != null)
            {
                foreach (var decorator in DecoratorsSequence)
                {
                    _logger.Info($"[DecoratorFactory:GetBatchProcess] {JsonConvert.SerializeObject(decorator)}");
                    switch (decorator)
                    {
                        case DecoratorType.FileDownload:
                            result = result.Decorate<FileDownloadDecorator>(decorator);
                            break;
                        case DecoratorType.ExecuteSP:
                            result = result.Decorate<ExecuteSPDecorator>(decorator);
                            break;
                        case DecoratorType.FileUpload:
                            result = result.Decorate<FileUploadDecorator>(decorator);
                            break;
                    }
                }
            }

            return result;
        }
    }


    internal static class FileDecoratorHelper
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
