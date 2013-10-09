//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxAppSettings.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.AppSettings.Droid
{
    public class MvxAppSettings : IAppSettings
    {
        public string MenuFilePath
        {
            get
            {
                //return this.SharedRootFolder + "triumphator.pdf";
                return "/triumphator.pdf";
            }
        }

        public string SharedRootFolder
        {
            get
            {
                return "/sdcard/lbk/";
            }
        }
    }
}