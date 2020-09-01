using System;
using BatchDecorator.API.Decorators;
using BatchDecorator.API.Enums;
using BatchDecorator.API.Services;
using System.Collections.Generic;

namespace BatchDecorator.API.Workers
{
    public class BatchWorker : IBatchProcess
    {
        public void DoWork()
        {
            var decoraFactory = new DecoratorFactory(new BatchProcess());
            decoraFactory.DecoratorsSequence = new List<DecoratorType>()
            {
                DecoratorType.FileDownload,
                DecoratorType.ExecuteSP,
                DecoratorType.FileUpload
            };
            decoraFactory.GetBatchProcess().DoWork();
        }
    }
}
