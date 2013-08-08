//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Message .cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Messages
{
    using Cirrious.MvvmCross.Plugins.Messenger;

    public class Message : MvxMessage
    {
        public Message(object sender)
            : base(sender)
        {
        }
    }
}