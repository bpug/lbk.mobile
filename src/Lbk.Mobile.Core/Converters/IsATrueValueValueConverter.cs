//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TimeAgoConverter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Converters
{
    using System;
    using System.Globalization;

    using Cirrious.CrossCore.Converters;

    using Lbk.Mobile.Common.Extensions;

    public class IsATrueValueValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enabled = value.IsATrueValue(parameter, true);
            return enabled;
        }
    }
}