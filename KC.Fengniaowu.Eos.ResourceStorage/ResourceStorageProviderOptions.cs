// ***********************************************************************
// Solution         : KC.Fengniaowu.Eos
// Project          : KC.Fengniaowu.Eos.ResourceStorage
// File             : ResourceStorageProviderOptions.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using Microsoft.Extensions.Options;

namespace KC.Fengniaowu.Eos.ResourceStorage
{
    public class ResourceStorageProviderOptions : IOptions<ResourceStorageProviderOptions>
    {
        public string StorageConnectionString { get; set; }

        public string BlobContainerName { get; set; }

        #region IOptions<ResourceStorageProviderOptions> Members

        ResourceStorageProviderOptions IOptions<ResourceStorageProviderOptions>.Value => this;

        #endregion
    }
}