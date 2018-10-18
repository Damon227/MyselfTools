// ***********************************************************************
// Solution         : KC.Fengniaowu.Eos
// Project          : KC.Fengniaowu.Eos.ResourceStorage
// File             : FileInfo.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Diagnostics.CodeAnalysis;

namespace KC.Fengniaowu.Eos.ResourceStorage
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class FileInfo
    {
        /// <summary>
        ///     True if resource exists in the underlying storage system.
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        ///     The length of the file in bytes, or -1 for a directory or non-existing files.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        ///     文件地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     The path to the file, including the file name. Return null if the file is not directly accessible.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     The temp path to the file, including the file name. Return null if the file is not directly accessible.
        /// </summary>
        public string TempPath { get; set; }

        /// <summary>
        ///     The name of the file or directory, not including any path.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     When the file was last modified
        /// </summary>
        public DateTimeOffset LastModified { get; set; }

        /// <summary>
        ///     True for the case TryGetDirectoryContents has enumerated a sub-directory
        /// </summary>
        public bool IsDirectory { get; set; }

        /// <summary>
        ///     资源种类。
        /// </summary>
        public string Kind { get; set; }
    }
}