//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LocalizedStrings.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Localization
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
            
        }

        private static readonly Strings LocalizedStringsResources = new Strings();

        public Strings Strings
        {
            get
            {
                return LocalizedStringsResources;
            }
        }
    }
}