//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Utility.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common.Utils
{
    using System;
    using System.Text;
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

        public static string GetRandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}