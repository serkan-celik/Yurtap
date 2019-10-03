using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Yurtap.Core.Utilities.ExtensionMethods
{
    public static class ExtensionManager
    {
        public static string ToTitleCase(this string text)
        {
            CultureInfo cultureInfo = new CultureInfo("tr-TR", false);
            return cultureInfo.TextInfo.ToTitleCase(text.ToLower(cultureInfo));
        }

        public static string ConvertTRCharToENChar(this string text)
        {
            return String.Join("", text.Normalize(NormalizationForm.FormD)
            .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }

        public static string RemoveEmpty(this string text)
        {
            return text.Replace(" ","");
        }
    }
}
