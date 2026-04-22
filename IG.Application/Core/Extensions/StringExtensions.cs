using System;

namespace IG.Application.Core.Extensions
{
    public static class StringExtensions
    {
        public static string UppercaseFirstWord(this string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            var chars = value.Trim().ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (char.IsLetter(chars[i]))
                {
                    chars[i] = char.ToUpperInvariant(chars[i]);
                    break;
                }
            }

            return new string(chars);
        }
    }
}