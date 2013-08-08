//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxTestResourceLoader.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Implementation
{
    using System;
    using System.IO;

    using Cirrious.MvvmCross.Plugins.ResourceLoader;

    public class MvxTestResourceLoader : MvxResourceLoader
    {
        public override void GetResourceStream(string resourcePath, Action<Stream> streamAction)
        {
            string execPath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(execPath, resourcePath.Replace('/', '\\'));
            var stream = new StreamReader(fullPath);
            streamAction(stream.BaseStream);
        }
    }
}