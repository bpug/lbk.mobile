//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NotificationEventArgs.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common
{
    using System;

    /// <summary>
    ///     Notification with or without a string message
    /// </summary>
    public class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs()
        {
        }

        public NotificationEventArgs(string message)
        {
            this.Message = message;
        }

        // Message
        public string Message { get; protected set; }
    }

    /// <summary>
    ///     Notification with outgoing data
    /// </summary>
    /// <typeparam name="TOutgoing">Outgoing data type</typeparam>
    public class NotificationEventArgs<TOutgoing> : NotificationEventArgs
    {
        public NotificationEventArgs()
        {
        }

        public NotificationEventArgs(string message)
            : base(message)
        {
        }

        public NotificationEventArgs(string message, TOutgoing data)
            : this(message)
        {
            this.Data = data;
        }

        // Data
        public TOutgoing Data { get; protected set; }
    }

    /// <summary>
    ///     Notification with outgoing and incoming data
    /// </summary>
    /// <typeparam name="TOutgoing">Outgoing data type</typeparam>
    /// <typeparam name="TIncoming">Incoming data type</typeparam>
    public class NotificationEventArgs<TOutgoing, TIncoming> : NotificationEventArgs<TOutgoing>
    {
        public NotificationEventArgs()
        {
        }

        public NotificationEventArgs(string message)
            : base(message)
        {
        }

        public NotificationEventArgs(string message, TOutgoing data)
            : base(message, data)
        {
        }

        public NotificationEventArgs(string message, TOutgoing data, Action<TIncoming> completed)
            : this(message, data)
        {
            this.Completed = completed;
        }

        // Completion callback
        public Action<TIncoming> Completed { get; protected set; }
    }
}