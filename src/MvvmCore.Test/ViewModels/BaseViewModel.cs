using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.ViewModels;
using MvvmCore.Test.Interfaces.Errors;

namespace MvvmCore.Test.ViewModels
{
    public class BaseViewModel
        : MvxViewModel
    {
        public void ReportError(string error)
        {
            this.GetService<IErrorReporter>().ReportError(error);
        }
    }
}