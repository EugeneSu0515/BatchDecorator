using System.Collections.Generic;
using BatchDecorator.API.Enums;
using BatchDecorator.API.Services;

namespace BatchDecorator.API.Decorators
{
    public class DecoratorFactory
    {
        public IBatchProcess Component
        { get; private set; }

        public List<DecoratorType> DecoratorsSequence
        { get; set; }


        public DecoratorFactory(IBatchProcess component)
        {
            Component = component;
        }

        public void DoWork()
        {
            IBatchProcess result = Component;
            result.DoWork();
            if (DecoratorsSequence != null)
            {
                foreach (var decorator in DecoratorsSequence)
                {
                    switch (decorator)
                    {
                        case DecoratorType.FileDownload:
                            result = result.Decorate<FileDownloadDecorator>();
                            result.DoWork();
                            break;
                        case DecoratorType.ExecuteSP:
                            result = result.Decorate<ExecuteSPDecorator>();
                            result.DoWork();
                            break;
                        case DecoratorType.FileUpload:
                            result = result.Decorate<FileUploadDecorator>();
                            result.DoWork();
                            break;
                    }
                }
            }
        }
    }


    internal static class FileDecoratorHelper
    {
        public static BaseDecorator Decorate<T>(this IBatchProcess component) where T : BaseDecorator
        {
            var instance = Startup.ServiceProvider.GetService(typeof(T));
            return instance as BaseDecorator;
        }
    }
}
