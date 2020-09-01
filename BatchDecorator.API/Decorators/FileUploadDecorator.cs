using System;
using System.IO;
using System.Net;
using BatchDecorator.API.Models;
using BatchDecorator.API.Services;
using Dapper;
using FluentFTP;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;

namespace BatchDecorator.API.Decorators
{
    public class FileUploadDecorator : BaseDecorator
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly FTPConfigBaseModel _ftpConfigModel;
        private readonly string _fileName;
        private readonly string _remotePath;
        private readonly string _localPath;
        private readonly string _connStr;
        public FileUploadDecorator(IBatchProcess batchProcess
            , IOptions<FTPConfigModel> options
            , IConfiguration config
            , IHostEnvironment env) :base(batchProcess)
        {
            _ftpConfigModel = options.Value.Download;
            _fileName = $"upload_{DateTime.Now:yyyyMMdd}.txt";
            _remotePath = Path.Combine(_ftpConfigModel.RemotePath, _fileName);
            _localPath = Path.Combine(env.ContentRootPath, _ftpConfigModel.LocalPath, _fileName);

            _connStr = config.GetConnectionString("DB");
        }

        public override void DoWork()
        {
            _batchProcess.DoWork();
            _logger.Info($"[FileUploadDecorator:FtpConfig] {JsonConvert.SerializeObject(_ftpConfigModel)}");
            _logger.Info($"FileUploadDecorator:DoWork!");
            //using (var _conn = new SqlConnection(_connStr))
            //{
            //    var result = _conn.Query(@"sql statement", new { });
            //    File.WriteAllText(_localPath, result.ToString());
            //    FtpClient client = new FtpClient(_ftpConfigModel.Server);
            //    client.Credentials = new NetworkCredential(_ftpConfigModel.Account, _ftpConfigModel.Password);
            //    client.Connect();
            //    client.UploadFile(_localPath, _remotePath, FtpRemoteExists.Overwrite);
            //    client.Disconnect();
            //}
        }
    }
}
