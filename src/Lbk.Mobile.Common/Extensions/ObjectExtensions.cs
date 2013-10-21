//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ObjectExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsATrueValue(this object value, object parameter, bool defaultValue)
        {
            if (value == null)
            {
                return false;
            }

            if (value is bool)
            {
                return (bool)value;
            }

            if (value is int)
            {
                if (parameter == null)
                {
                    return (int)value > 0;
                }
                else
                {
                    return (int)value > int.Parse(parameter.ToString());
                }
            }

            if (value is double)
            {
                return (double)value > 0;
            }

            if (value is string)
            {
                return !string.IsNullOrWhiteSpace(value as string);
            }

            return defaultValue;
        }
    }
}