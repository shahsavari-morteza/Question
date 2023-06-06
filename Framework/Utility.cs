using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static class Utility
    {
        public static bool IsValidFileName(this string FileName)
        {
            var fn = FileName.ToLower();
            if (fn.Contains(".php") || fn.Contains(".asp") || fn.Contains(".rb") || fn.Contains(".exe"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string ToUniqueFileName(this string FileName)
        {
            var str = Guid.NewGuid().ToString().Replace("-", "_");
            return $"{str}_{FileName}";
        }
    }
}
