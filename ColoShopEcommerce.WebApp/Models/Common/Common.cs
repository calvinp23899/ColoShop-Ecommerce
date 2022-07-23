using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ColoShopEcommerce.WebApp.Models.Common
{
    public class Common
    {
        public static string FormatCurrency(object value, int comma = 2)
        {
            bool isNumber = IsNumeric(value);
            decimal GT = 0;
            if (isNumber)
            {
                GT = Convert.ToDecimal(value);
            }
            string str = "";
            string thapphan = "";
            for (int i = 0; i < comma; i++)
            {
                thapphan += "#";
            }
            if (thapphan.Length > 0)
                thapphan = "." + thapphan;
            string snumformat = string.Format("0:#,##0{0}", thapphan);
            str = string.Format("{"+ snumformat+"}", GT);
            return str;
        }

        private static bool IsNumeric(object value)
        {
            return value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is decimal
                || value is double;
        }
    }
}