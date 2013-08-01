using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbk.Mobile.Test.Core.Services
{
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
