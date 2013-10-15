//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="VoucherUsedMessage.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Messages
{
    using Cirrious.MvvmCross.Plugins.Messenger;

    public class VoucherActivatedMessage : MvxMessage
    {
        public VoucherActivatedMessage(object sender)
            : base(sender)
        {
        }
    }
}