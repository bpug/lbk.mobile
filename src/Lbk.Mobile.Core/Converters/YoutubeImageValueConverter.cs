//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="YoutubeImageValueConverter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Converters
{
    using System;
    using System.Globalization;

    using Cirrious.CrossCore.Converters;

    using Lbk.Mobile.Common.Utils;

    public class YoutubeImageValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return null;
            }

            var quality = parameter ?? "default";

            string formatter;
            switch (((string)quality).ToLower())
            {
                case "hq":
                    formatter = "http://img.youtube.com/vi/{0}/hqdefault.jpg";
                    break;
                case  "mq":
                     formatter = "http://img.youtube.com/vi/{0}/mqdefault.jpg";
                    break;
                 case "sd":
                     formatter = "http://img.youtube.com/vi/{0}/sddefault.jpg";
                    break;
                default:
                    formatter = "http://img.youtube.com/vi/{0}/default.jpg";
                    break;
            }

            string url = (string)value;
            string imageUrl = string.Empty;

            string id = Utility.GetYuotubeVideoId(url);
            if (!string.IsNullOrEmpty(id))
            {
                imageUrl = string.Format(formatter, id);
            }

            return imageUrl;
        }
    }
}