using NLog;

namespace BatchDecorator.API.Services
{
    public class BatchProcess : IBatchProcess
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void DoWork()
        {
            _logger.Info("BatchProcess:DoWork!");
        }
    }
}
