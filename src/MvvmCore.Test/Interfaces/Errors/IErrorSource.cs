using System;

namespace MvvmCore.Test.Interfaces.Errors
{
    public interface IErrorSource
    {
        event EventHandler<ErrorEventArgs> ErrorReported;
    }
}