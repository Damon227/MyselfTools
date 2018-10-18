// ***********************************************************************
// Solution         : KC.Fengniaowu.Eos
// Project          : KC.Fengniaowu.Eos.ResourceStorage
// File             : AzureBlobStorageProvider.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KC.Foundation;
using KC.Foundation.Sys;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace KC.Fengniaowu.Eos.ResourceStorage
{
    public class AzureBlobStorageProvider : IResourceStorageProvider
    {
        private readonly CloudBlobContainer _blobContainer;

        public AzureBlobStorageProvider(IOptions<ResourceStorageProviderOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            ResourceStorageProviderOptions providerOptions = options.Value;

            CloudStorageAccount account = CloudStorageAccount.Parse(providerOptions.StorageConnectionString);
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            blobClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(3.Seconds(), 5);
            _blobContainer = blobClient.GetContainerReference(providerOptions.BlobContainerName);
            _blobContainer.CreateIfNotExistsAsync().Wait();
        }

        #region IResourceStorageProvider Members

        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="uri">资源完整路径。</param>
        public async Task<FileInfo> GetResourceAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            string path = uri.AbsolutePath.Remove("/" + _blobContainer.Name + "/");

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(path);
            if (!await blob.ExistsAsync())
            {
                return new NotFoundFileInfo(path);
            }

            CloudBlockBlob ss = await blob.CreateSnapshotAsync();
            Console.WriteLine(ss.SnapshotQualifiedStorageUri);

            string[] paths = path.Split('/');
            string fileNameBase64 = paths[paths.Length - 1];
            //string signature = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            //{
            //    Permissions = SharedAccessBlobPermissions.Read,
            //    SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(30),
            //    SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-2)
            //});

            return new FileInfo
            {
                Name = fileNameBase64,
                Kind = blob.Metadata.TryGetValue("Kind", out string kind) ? kind : null,
                Exists = true,
                IsDirectory = false,
                LastModified = blob.Properties.LastModified.GetValueOrDefault(DateTimeOffset.UtcNow),
                Length = blob.Properties.Length,
                Path = blob.Uri.PathAndQuery,
                TempPath = null
            };
        }

        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="path">资源相对路径。</param>
        public async Task<FileInfo> GetResourceAsync(string path)
        {
            if (path.IsNullOrEmpty())
            {
                throw new ArgumentException(Resource.Argument_EmptyOrNullString, nameof(path));
            }

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(path);
            if (!await blob.ExistsAsync())
            {
                return new NotFoundFileInfo(path);
            }

            await blob.FetchAttributesAsync();

            string[] paths = path.Split('/');
            string fileNameBase64 = paths[paths.Length - 1];
            //string signature = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            //{
            //    Permissions = SharedAccessBlobPermissions.Read,
            //    SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(30),
            //    SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-2)
            //});

            return new FileInfo
            {
                Name = fileNameBase64,
                Kind = blob.Metadata.TryGetValue("Kind", out string kind) ? kind : null,
                Exists = true,
                IsDirectory = false,
                LastModified = blob.Properties.LastModified.GetValueOrDefault(DateTimeOffset.UtcNow),
                Length = blob.Properties.Length,
                Path = blob.Uri.PathAndQuery,
                TempPath = null
            };
        }

        /// <summary>
        ///     查询指定前置路径下的所有资源。
        /// </summary>
        /// <param name="prefixPath">资源前置路径</param>
        public async Task<List<FileInfo>> GetResourcesAsync(string prefixPath)
        {
            if (prefixPath.IsNullOrEmpty())
            {
                throw new ArgumentException(Resource.Argument_EmptyOrNullString, nameof(prefixPath));
            }

            CloudBlobDirectory dir = _blobContainer.GetDirectoryReference(prefixPath);
            BlobResultSegment blobResult = await dir.ListBlobsSegmentedAsync(true, BlobListingDetails.All, null, null, null, null);

            List<FileInfo> fileInfos = new List<FileInfo>();

            foreach (IListBlobItem item in blobResult.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    //string signature = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
                    //{
                    //    Permissions = SharedAccessBlobPermissions.Read,
                    //    SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(30),
                    //    SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-2)
                    //});

                    FileInfo fileInfo = new FileInfo
                    {
                        Name = blob.Name,
                        Kind = blob.Metadata.TryGetValue("Kind", out string kind) ? kind : null,
                        Exists = true,
                        IsDirectory = false,
                        LastModified = blob.Properties.LastModified.GetValueOrDefault(DateTimeOffset.UtcNow),
                        Length = blob.Properties.Length,
                        Path = blob.Uri.PathAndQuery,
                        TempPath = null
                    };

                    fileInfos.Add(fileInfo);
                }
            }

            return fileInfos.OrderBy(t => t.LastModified).ToList();
        }

        /// <summary>
        ///     查询资源根目录。
        /// </summary>
        /// <returns></returns>
        public Task<string> GetResourceBasePathAsync()
        {
            return Task.FromResult(_blobContainer.ServiceClient.BaseUri.ToString());
        }

        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="uri">资源完整路径。</param>
        public async Task<Stream> GetResourceStreamAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            string path = uri.AbsolutePath.Remove("/" + _blobContainer.Name + "/");

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(path);
            if (!await blob.ExistsAsync())
            {
                return null;
            }

            MemoryStream stream = new MemoryStream();
            await blob.DownloadToStreamAsync(stream);
            return stream;
        }

        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="path">资源相对路径。</param>
        public async Task<Stream> GetResourceStreamAsync(string path)
        {
            if (path.IsNullOrEmpty())
            {
                throw new ArgumentException(Resource.Argument_EmptyOrNullString, nameof(path));
            }

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(path);
            if (!await blob.ExistsAsync())
            {
                return null;
            }

            MemoryStream stream = new MemoryStream();
            await blob.DownloadToStreamAsync(stream);
            return stream;
        }

        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="path">资源相对路径。</param>
        public async Task<string> GetResourceStringAsync(string path)
        {
            if (path.IsNullOrEmpty())
            {
                throw new ArgumentException(Resource.Argument_EmptyOrNullString, nameof(path));
            }

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(path);
            if (!await blob.ExistsAsync())
            {
                return null;
            }

            return await blob.DownloadTextAsync();
        }

        /// <summary>
        ///     保存资源到Blob。
        /// </summary>
        /// <param name="file">资源。</param>
        /// <param name="prefixPath">资源路径。</param>
        /// <param name="fileName">资源完整名称，含路径。</param>
        /// <param name="contentType">资源类型。</param>
        /// <param name="kind">资源种类。</param>
        public async Task<FileInfo> SaveResourceAsync(Stream file, string prefixPath, string fileName, string contentType, string kind = "others")
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (fileName.IsNullOrEmpty())
            {
                throw new ArgumentException(Resource.Argument_EmptyOrNullString, nameof(fileName));
            }

            string fullName = prefixPath.IsNullOrEmpty() ? fileName : prefixPath.Last() == '/' ? prefixPath + fileName : prefixPath + "/" + fileName;

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(fullName); //public path
            blob.Properties.CacheControl = "no-cache";
            if (contentType.IsNotNullOrEmpty())
            {
                blob.Properties.ContentType = contentType;
            }
            blob.Metadata["FileName"] = Convert.ToBase64String(fileName.GetBytesOfUTF8());
            blob.Metadata["Kind"] = kind;

            await blob.UploadFromStreamAsync(file);
            await blob.FetchAttributesAsync();

            //string signature = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            //{
            //    Permissions = SharedAccessBlobPermissions.Read,
            //    SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(30),
            //    SharedAccessStartTime = DateTimeOffset.UtcNow
            //});

            return new FileInfo
            {
                Name = fileName,
                Kind = kind,
                Exists = true,
                IsDirectory = false,
                LastModified = blob.Properties.LastModified.GetValueOrDefault(DateTimeOffset.UtcNow),
                Length = blob.Properties.Length,
                Path = blob.Uri.PathAndQuery,
                TempPath = null
            };
        }

        /// <summary>
        ///     保存资源到Blob。
        /// </summary>
        /// <param name="file">资源。</param>
        /// <param name="prefixPath">资源路径。</param>
        /// <param name="fileName">资源完整名称，含路径。</param>
        /// <param name="contentType">资源类型。</param>
        /// <param name="kind">资源种类。</param>
        public async Task<FileInfo> SaveResourceAsync(string file, string prefixPath, string fileName, string contentType, string kind = "others")
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (fileName.IsNullOrEmpty())
            {
                throw new ArgumentException(Resource.Argument_EmptyOrNullString, nameof(fileName));
            }

            string fullName = prefixPath.IsNullOrEmpty() ? fileName : prefixPath.Last() == '/' ? prefixPath + fileName : prefixPath + "/" + fileName;

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference("{0}".FormatWith(fullName));
            blob.Properties.CacheControl = "no-cache";
            if (contentType.IsNotNullOrEmpty())
            {
                blob.Properties.ContentType = contentType;
            }
            blob.Metadata["FileName"] = Convert.ToBase64String(fileName.GetBytesOfUTF8());
            blob.Metadata["Kind"] = kind;

            await blob.UploadFromFileAsync(file);
            await blob.FetchAttributesAsync();
            //string signature = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            //{
            //    Permissions = SharedAccessBlobPermissions.Read,
            //    SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(30),
            //    SharedAccessStartTime = DateTimeOffset.UtcNow
            //});

            return new FileInfo
            {
                Name = fileName,
                Kind = kind,
                Exists = true,
                IsDirectory = false,
                LastModified = blob.Properties.LastModified.GetValueOrDefault(DateTimeOffset.UtcNow),
                Length = blob.Properties.Length,
                Path = blob.Uri.PathAndQuery,
                TempPath = null
            };
        }

        /// <summary>
        ///     删除指定资源。
        /// </summary>
        /// <param name="uri">资源完整路径。</param>
        public async Task DeleteResourceAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            string path = uri.AbsolutePath.Remove("/" + _blobContainer.Name + "/");

            CloudBlob blob = _blobContainer.GetBlobReference(path);
            await blob.DeleteIfExistsAsync();
        }

        #endregion
    }
}