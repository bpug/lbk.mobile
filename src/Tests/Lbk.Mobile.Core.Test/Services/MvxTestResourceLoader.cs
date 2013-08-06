namespace Lbk.Mobile.Core.Test.Services
{
    using System;
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
