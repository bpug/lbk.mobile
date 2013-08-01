//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Trace.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Infrastructure
{
    using Cirrious.CrossCore.Platform;

    public class Trace
    {
        private const string Tag = "Lbk";

        public static void Error(string message, params object[] args)
        {
            MvxTrace.TaggedTrace(MvxTraceLevel.Error, Tag, message, args);
        }

        public static void Info(string message, params object[] args)
        {
            MvxTrace.TaggedTrace(MvxTraceLevel.Diagnostic, Tag, message, args);
        }

        public static void Warn(string message, params object[] args)
        {
            MvxTrace.TaggedTrace(MvxTraceLevel.Warning, Tag, message, args);
        }
    }
}