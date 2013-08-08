//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DialogMessage.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Messages
{
    using Cirrious.MvvmCross.Plugins.Messenger;

    using Lbk.Mobile.Model.Enums;

    public class DialogMessage : MvxMessage
    {
        public DialogMessage(object sender)
            : base(sender)
        {
        }

        public DialogResult Result { get; set; }
    }
}