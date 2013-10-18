//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TimeOfDayConverter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Converters
{
    using System;
    using System.Globalization;

    using Cirrious.CrossCore.Converters;

    public class TimeOfDayConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
            {
                return value;
            }

            var timeOfDay = ((DateTime)value).TimeOfDay;

            return timeOfDay;
        }
    }
}