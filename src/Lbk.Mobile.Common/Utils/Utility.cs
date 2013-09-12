//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Utility.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common.Utils
{
    using System.Text.RegularExpressions;

    public class Utility
    {
        public static string GetYuotubeVideoId(string url)
        {
            var yotubeVideoRegex =
                new Regex(
                    "^(?:https?\\:\\/\\/)?(?:www\\.)?(?:youtu\\.be\\/|youtube\\.com\\/(?:embed\\/|v\\/|watch\\?v\\=))([\\w-]{10,12})(?:[\\&\\?\\#].*?)*?(?:[\\&\\?\\#]t=([\\dhm]+s))?$",
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var youtubeMatch = yotubeVideoRegex.Match(url);

            string id = string.Empty;
            if (youtubeMatch.Success)
            {
                id = youtubeMatch.Groups[1].Value;
            }

            return id;
        }
    }
}