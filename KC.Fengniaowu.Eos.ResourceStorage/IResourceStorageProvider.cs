// ***********************************************************************
// Solution         : KC.Fengniaowu.Eos
// Project          : KC.Fengniaowu.Eos.ResourceStorage
// File             : IResourceStorageProvider.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KC.Fengniaowu.Eos.ResourceStorage
{
    public interface IResourceStorageProvider
    {
        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="uri">资源完整路径。</param>
        Task<FileInfo> GetResourceAsync(Uri uri);

        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="path">资源相对路径。</param>
        Task<FileInfo> GetResourceAsync(string path);

        /// <summary>
        ///     查询指定前置路径下的所有资源。
        /// </summary>
        /// <param name="prefixPath">资源前置路径</param>
        Task<List<FileInfo>> GetResourcesAsync(string prefixPath);

        /// <summary>
        ///     查询资源根目录。
        /// </summary>
        /// <returns></returns>
        Task<string> GetResourceBasePathAsync();

        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="uri">资源完整路径。</param>
        Task<Stream> GetResourceStreamAsync(Uri uri);

        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="path">资源相对路径。</param>
        Task<Stream> GetResourceStreamAsync(string path);

        /// <summary>
        ///     查询资源。
        /// </summary>
        /// <param name="path">资源相对路径。</param>
        Task<string> GetResourceStringAsync(string path);

        /// <summary>
        ///     保存资源到Blob。
        /// </summary>
        /// <param name="file">资源。</param>
        /// <param name="prefixPath">资源路径。</param>
        /// <param name="fileName">资源完整名称，含路径。</param>
        /// <param name="contentType">资源类型。</param>
        /// <param name="kind">资源种类。</param>
        Task<FileInfo> SaveResourceAsync(Stream file, string prefixPath, string fileName, string contentType, string kind = "others");

        /// <summary>
        ///     保存资源到Blob。
        /// </summary>
        /// <param name="file">资源。</param>
        /// <param name="prefixPath">资源路径。</param>
        /// <param name="fileName">资源完整名称，含路径。</param>
        /// <param name="contentType">资源类型。</param>
        /// <param name="kind">资源种类。</param>
        Task<FileInfo> SaveResourceAsync(string file, string prefixPath, string fileName, string contentType, string kind = "others");

        /// <summary>
        ///     删除指定资源。
        /// </summary>
        /// <param name="uri">资源完整路径。</param>
        Task DeleteResourceAsync(Uri uri);
    }
}