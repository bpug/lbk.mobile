//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DistanceValueConverter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Converters
{
    using System;
    using System.Globalization;

    using Cirrious.CrossCore.Converters;

    using Lbk.Mobile.Common.Extensions;

    public class DistanceValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double))
            {
                return value;
            }

            double doubleValue = (double)value;
            string result = doubleValue.DistanceToString();

            return result;
        }
    }
}