using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ColoShopEcommerce.WebApp.Models.Common
{
    public static class Common
    {
        public static string ToFormattedCurrencyString(this decimal currencyAmount,string isoCurrencyCode,CultureInfo userCulture)
        {
            var userCurrencyCode = new RegionInfo(userCulture.Name).ISOCurrencySymbol;

            if (userCurrencyCode == isoCurrencyCode)
            {
                return currencyAmount.ToString("C", userCulture);
            }
            return string.Format(
                "{0} {1}",
                isoCurrencyCode,
                currencyAmount.ToString("N2", userCulture));
        }
    }
}