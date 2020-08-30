using BatchDecorator.API.Decorators;
using BatchDecorator.API.Enums;
using BatchDecorator.API.Services;
using System.Collections.Generic;

namespace BatchDecorator.API.Workers
{
    public class BatchWorker : IBatchProcess
    {
        private readonly IBatchProcess _batchProcess;

        public BatchWorker(IBatchProcess batchProcess)
        {
            _batchProcess = batchProcess;
        }
        public void DoWork()
        {
            var decoraFactory = new DecoratorFactory(_batchProcess);
            decoraFactory.DecoratorsSequence = new List<DecoratorType>()
            {
                DecoratorType.FileDownload,
                DecoratorType.ExecuteSP,
                DecoratorType.FileUpload
            };
            decoraFactory.DoWork();
        }
    }
}
