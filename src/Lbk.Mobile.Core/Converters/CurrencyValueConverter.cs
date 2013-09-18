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

    public class CurrencyValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double || value is string))
                return value;

            var format = parameter ?? "{0:N2} €";
            double doubleValue;
            if (value is double)
            {
                doubleValue = (double)value;
            }
            else
            {
                double.TryParse((string)value, out doubleValue);
            }

            return string.Format((string)format, doubleValue);
        }
    }
}