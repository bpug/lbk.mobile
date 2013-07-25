using System;
using System.Collections.Generic;

namespace MvvmCore.Test.Interfaces.First
{
    public interface IFirstService
    {
        void GetItems(string key, Action<List<SimpleItem>> onSuccess, Action<FirstServiceError> onError);
    }
}
