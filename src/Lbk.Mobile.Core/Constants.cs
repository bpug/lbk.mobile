//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Constants.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core
{
    public class Constants
    {
        public const string GeneralNamespace = "Lbk";
        public const string ShareType = "Share";
        public const string ErrorType = "Error";
        public const string HostReachableName = "www.google.com";

        public const double LbkLatitude = 48.147849;
        public const double LbkLongitude = 11.558634;
        public const string LbkPhone = "+498954726690";
        public const string LbkEmail = "info@loewenbraeukeller.com";

        public const string SdcardRootFolder = "/sdcard/lbk/";
        public const string LocalMenuFilePath = SdcardRootFolder + "menu.pdf";// "/sdcard/menu.pdf";

        public const string MenuUrl = "http://lbkmobile.loewenbraeukeller.com/media/speisekarte/speisekarte.pdf";

        public class UserSettings
        {
            public const string PdfLastUpdate = "PdfLastUpdate";
        }
    }
}