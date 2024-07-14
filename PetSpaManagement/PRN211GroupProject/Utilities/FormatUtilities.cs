using System.Text.RegularExpressions;

namespace PRN211GroupProject.Utilities
{
    public class FormatUtilities
    {
        public static string TrimSpacesPreserveSingle(string input)
        {
            return Regex.Replace(input.Trim(), @"\s+", " ");
        }
    }
}
