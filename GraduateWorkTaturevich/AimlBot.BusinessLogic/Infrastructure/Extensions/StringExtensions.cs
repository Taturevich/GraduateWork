using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLogic.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string[] ToWords(this string text)
        {
            var options = RegexOptions.None;
            var regex = new Regex("[ ]{2,}", options);
            var trimmedText = regex.Replace(text, " ");
            var words = trimmedText
                .Split(' ')
                .Select(x => x.ToLower())
                .ToArray();
            return words;
        }
    }
}
