//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ErrorMessage.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Messages
{
    using Cirrious.MvvmCross.Plugins.Messenger;

    public class ErrorMessage : MvxMessage
    {
        public ErrorMessage(object sender, string message)
            : base(sender)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
}