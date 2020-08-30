using BatchDecorator.API.Services;

namespace BatchDecorator.API.Decorators
{
    public abstract class BaseDecorator : IBatchProcess
    {
        protected readonly IBatchProcess _batchProcess;

        public BaseDecorator(IBatchProcess batchProcess)
        {
            _batchProcess = batchProcess;
        }
        public abstract void DoWork();
    }
}
