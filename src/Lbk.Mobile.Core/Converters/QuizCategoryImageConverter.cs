//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizCategoryImageConverter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Converters
{
    using System;
    using System.Globalization;

    using Cirrious.CrossCore.Converters;

    using Lbk.Mobile.Model.Enums;

    public class QuizCategoryImageConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is QuestionCategory))
                return value;

            var prefix = parameter ?? "quiz_";
            string category = ((QuestionCategory)value).ToString();

            string result = string.Format("{0}{1}", prefix, category);

            return result;
        }
    }
}