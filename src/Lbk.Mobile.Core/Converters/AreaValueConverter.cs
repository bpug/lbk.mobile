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

    public class AreaValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int? || value is string))
                return value;

            var format = parameter ?? "{0} m²";
            double intValue;
            if (value is int?)
            {
                intValue = (int)value;
            }
            else
            {
                double.TryParse((string)value, out intValue);
            }

            return string.Format((string)format, intValue);
        }
    }
}