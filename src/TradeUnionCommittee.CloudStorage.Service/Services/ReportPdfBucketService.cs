using Minio;
using RestSharp.Extensions;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.CloudStorage.Service.Interfaces;
using TradeUnionCommittee.CloudStorage.Service.Model;
using TradeUnionCommittee.DAL.CloudStorage.EF;
using TradeUnionCommittee.DAL.CloudStorage.Entities;

namespace TradeUnionCommittee.CloudStorage.Service.Services
{
    public class ReportPdfBucketService : IReportPdfBucketService, IDisposable
    {
        private const string BUCKET_NAME = "pdf-bucket";
        private const string EXTENSION_FILE = ".pdf";
        private const string CONTENT_TYPE = "application/pdf";

        private readonly MinioClient _minioClient;
        private readonly TradeUnionCommitteeCloudStorageContext _context;

        public ReportPdfBucketService(MinioClient minioClient, TradeUnionCommitteeCloudStorageContext context)
        {
            _minioClient = minioClient;
            _context = context;
        }

        public async Task<byte[]> GetObject(long id)
        {
            var model = _context.ReportPdfBucket.Find(id);
            var result = new byte[] { };
            await _minioClient.GetObjectAsync(BUCKET_NAME, $"{model.IdEmployee}/{model.FileName}{EXTENSION_FILE}", stream =>
            {
                result = stream.ReadAsBytes();
            });
            return result;
        }

        public async Task PutPdfObject(ReportPdfBucketModel model)
        {
            await CheckBucketExists();
            await _minioClient.PutObjectAsync(BUCKET_NAME, $"{model.IdEmployee}/{model.FileName}{EXTENSION_FILE}", model.Data, model.Data.Length, CONTENT_TYPE);
            await _context.ReportPdfBucket.AddAsync(new ReportPdfBucket
            {
                IdEmployee = model.IdEmployee,
                FileName = model.FileName,
                DateCreated = DateTime.Now,
                EmailUser = model.EmailUser,
                IpUser = model.IpUser,
                TypeReport = model.TypeReport,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            await _context.SaveChangesAsync();
        }

        private async Task CheckBucketExists()
        {
            var found = await _minioClient.BucketExistsAsync(BUCKET_NAME);
            if (!found)
            {
                await _minioClient.MakeBucketAsync(BUCKET_NAME);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}