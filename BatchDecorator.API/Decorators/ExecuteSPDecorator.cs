using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using BatchDecorator.API.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NLog;

namespace BatchDecorator.API.Decorators
{
    public class ExecuteSPDecorator : BaseDecorator
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _connStr;
        public ExecuteSPDecorator(IBatchProcess batchProcess, IConfiguration config)
        : base(batchProcess)
        {
            _connStr = config.GetConnectionString("DB");
        }


        public override void DoWork()
        {
            _batchProcess.DoWork();

            _logger.Info($"[ExecuteSPDecorator:ConnectionString] {_connStr}");
            _logger.Info($"ExecuteSPDecorator:DoWork!");
            //using (var _conn = new SqlConnection(_connStr))
            //{
            //    _conn.Execute("spName", new { }, commandType: CommandType.StoredProcedure);
            //}
        }
    }
}
