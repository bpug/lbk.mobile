using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbk.Mobile.Core.Test.Mocks
{
    
    public class MockMvxViewDispatcherProvider : IMvxViewDispatcherProvider
    {
        #region IMvxViewDispatcherProvider implementation

        public IMvxViewDispatcher Dispatcher { get; set; }

        #endregion
    }
}
