using System;
using System.IO;
using System.Net;
using BatchDecorator.API.Models;
using BatchDecorator.API.Services;
using FluentFTP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;

namespace BatchDecorator.API.Decorators
{
    public class FileDownloadDecorator : BaseDecorator
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly FTPConfigBaseModel _ftpConfigModel;
        private readonly string _fileName;
        private readonly string _remotePath;
        private readonly string _localPath;
        public FileDownloadDecorator(IBatchProcess batchProcess
        , IOptions<FTPConfigModel> options
        , IHostEnvironment env) : base(batchProcess)
        {
            _ftpConfigModel = options.Value.Download;
            _fileName = $"download_{DateTime.Now:yyyyMMdd}.txt";
            _remotePath = Path.Combine(_ftpConfigModel.RemotePath, _fileName);
            _localPath = Path.Combine(env.ContentRootPath, _ftpConfigModel.LocalPath, _fileName);
        }

        public override void DoWork()
        {
            _logger.Info($"[FileDownloadDecorator:FtpConfig] {JsonConvert.SerializeObject(_ftpConfigModel)}");
            _logger.Info($"FileDownloadDecorator:DoWork!");
            //FtpClient client = new FtpClient(_ftpConfigModel.Server);
            //client.Credentials = new NetworkCredential(_ftpConfigModel.Account, _ftpConfigModel.Password);
            //client.Connect();
            //if (client.FileExists(_remotePath))
            //    client.DownloadFile(_localPath, _remotePath, FtpLocalExists.Overwrite);
            //client.Disconnect();
        }
    }
}
