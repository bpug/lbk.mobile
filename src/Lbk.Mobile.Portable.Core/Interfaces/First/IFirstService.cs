namespace Lbk.Mobile.Portable.Core.Interfaces.First
{
    using System;
    using System.Collections.Generic;

    public interface IFirstService
    {
        void GetItems(string key, Action<List<SimpleItem>> onSuccess, Action<FirstServiceError> onError);
    }
}
