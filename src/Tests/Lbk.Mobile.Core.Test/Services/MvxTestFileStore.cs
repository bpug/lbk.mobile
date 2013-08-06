namespace Lbk.Mobile.Core.Test.Services
{
    using System;
    using System.IO;

    using Cirrious.MvvmCross.Plugins.File;

    public class MvxTestFileStore : MvxFileStore
    {
        protected override string FullPath(string path)
        {
            var execPath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(execPath, path.Replace('/', '\\'));
            return fullPath;
        }
    }
}
