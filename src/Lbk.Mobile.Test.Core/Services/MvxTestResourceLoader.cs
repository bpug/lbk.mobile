using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbk.Mobile.Test.Core.Services
{
    using System.IO;

    using Cirrious.MvvmCross.Plugins.ResourceLoader;

    public class MvxTestResourceLoader : MvxResourceLoader
    {
        #region Implementation of IMvxResourceLoader

        public override void GetResourceStream(string resourcePath, Action<Stream> streamAction)
        {
            var execPath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(execPath, resourcePath.Replace('/', '\\'));
            var stream = new StreamReader(fullPath);
            streamAction(stream.BaseStream);
        }

        #endregion
    }
}
