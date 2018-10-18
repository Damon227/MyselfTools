// ***********************************************************************
// Solution         : KC.Fengniaowu.Eos
// Project          : KC.Fengniaowu.Eos.ResourceStorage
// File             : NotFoundFileInfo.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace KC.Fengniaowu.Eos.ResourceStorage
{
    public class NotFoundFileInfo : FileInfo
    {
        public NotFoundFileInfo()
        {
            Exists = false;
            IsDirectory = false;
            LastModified = DateTimeOffset.MinValue;
            Length = -1;
            Path = null;
            TempPath = null;
        }

        public NotFoundFileInfo(string name)
        {
            Name = name;
        }

        public string PhysicalPath => null;
    }
}