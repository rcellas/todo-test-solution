using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TodoApp.Utilities
{
    public class S3FileStorage : IFileStorage
    {
        private readonly IAmazonS3 s3Client;
        private readonly IConfiguration configuration;
        private readonly string bucketName;

        public S3FileStorage(IAmazonS3 s3Client, IConfiguration configuration)
        {
            this.s3Client = s3Client;
            this.configuration = configuration;
            this.bucketName = configuration["AWS:BucketName"];
        }

        public async Task<string> Storage(string container, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var key = $"{container}/{fileName}";

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = file.ContentType
                };
                await s3Client.PutObjectAsync(putRequest);
            }

            // Construir la URL pública del archivo en S3
            var fileUrl = $"https://{bucketName}.s3.{configuration["AWS:Region"]}.amazonaws.com/{key}";
            return fileUrl;
        }

        public async Task Delete(string? route, string container)
        {
            if (string.IsNullOrEmpty(route))
            {
                return;
            }

            var fileName = Path.GetFileName(route);
            var key = $"{container}/{fileName}";

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            await s3Client.DeleteObjectAsync(deleteRequest);
        }
    }
}
