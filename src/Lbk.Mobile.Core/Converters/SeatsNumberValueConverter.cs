//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IntegerValueConverter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Converters
{
    using System;
    using System.Globalization;

    using Cirrious.CrossCore.Converters;

    using Lbk.Mobile.Common.Extensions;

    public class SeatsNumberValueConverter : MvxValueConverter< int, string>
    {
        protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        protected override int ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = 0;
            if (!value.IsEmpty())
            {
                int.TryParse(value, out intValue);
            }
            return intValue;
        }
    }
}