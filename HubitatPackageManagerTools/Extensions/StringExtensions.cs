using System;
using System.Collections.Generic;
using System.Text;

namespace HubitatPackageManagerTools.Extensions
{
    public static class StringExtensions
    {
        public static bool IsSpecified(this string str)
        {
            if (str != null && str != "null")
                return true;
            return false;
        } 
        public static bool IsNullValue(this string str)
        {
            return str == "null";
        }
    }
}
