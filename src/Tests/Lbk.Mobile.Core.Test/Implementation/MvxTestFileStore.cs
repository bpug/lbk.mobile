//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxTestFileStore.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Implementation
{
    using System;
    using System.IO;

    using Cirrious.MvvmCross.Plugins.File;

    public class MvxTestFileStore : MvxFileStore
    {
        protected override string FullPath(string path)
        {
            string execPath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(execPath, path.Replace('/', '\\'));
            return fullPath;
        }
    }
}