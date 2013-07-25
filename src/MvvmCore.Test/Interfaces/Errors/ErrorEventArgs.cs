using System;

namespace MvvmCore.Test.Interfaces.Errors
{
    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public ErrorEventArgs(string message)
        {
            Message = message;
        }
    }
}