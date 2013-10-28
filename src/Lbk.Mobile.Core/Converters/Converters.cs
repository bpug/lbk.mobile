//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Converters.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Converters
{
    public class Converters
    {
        public readonly CurrencyValueConverter Currency = new CurrencyValueConverter();
        public readonly AreaValueConverter Area = new AreaValueConverter();
        public readonly YoutubeImageValueConverter YoutubeImage = new YoutubeImageValueConverter();
        public readonly ToLetterConverter ToLetter = new ToLetterConverter();
        public readonly QuizCategoryImageConverter QuizCategoryImage = new QuizCategoryImageConverter();
        public readonly DistanceValueConverter Distance = new DistanceValueConverter();
        public readonly SeatsNumberValueConverter SeatsNumber = new SeatsNumberValueConverter();
        
        //public readonly InvertedIsATrueValueValueConverter InvertedIsATrueValue = new InvertedIsATrueValueValueConverter();
        //public readonly IsATrueValueValueConverter IsATrueValue = new IsATrueValueValueConverter();
    }
}