using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SakuraUI.Utilities
{
    public static class HtmlStringExtension
    {
        private static readonly Regex Html = new Regex(@"<style.*?/style>|(<[^>]+>)|(\s*&\w+;\s*)+", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex Space = new Regex(@"(\s{2,})", RegexOptions.Multiline | RegexOptions.Singleline);

        public static string RemoveHtml(this string input)
        {
            return string.IsNullOrEmpty(input) ? String.Empty : Html.Replace(input, string.Empty);
        }

        public static string RemoveHtmlWithoutSpace(this string input)
        {
            return Space.Replace(RemoveHtml(input), " ").TrimStart(' ');
        }

        public static string ConvertHtmlTag(this string input)
        {
            return input.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\r", "<br>");
        }

        public static string ToExtendedAscii(this string input)
        {
            var str = new StringBuilder();

            foreach (var t in input)
            {
                var c = t;
                if (Convert.ToInt32(c) > 127)
                {
                    str.Append("&#" + Convert.ToInt32(c) + ";");
                }
                else
                {
                    str.Append(c);
                }
            }

            return str.ToString();
        }
    }
}
