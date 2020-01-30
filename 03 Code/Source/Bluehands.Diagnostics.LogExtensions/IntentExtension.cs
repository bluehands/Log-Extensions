namespace Bluehands.Diagnostics.LogExtensions
{
    internal static class IntentExtension
    {
        public static string ConvertIndentToWhiteSpaces(this int indent)
        {
            if (indent <= 0)
                return string.Empty;
            switch (indent)
            {
                case 0:
                    return "";
                case 1:
                    return "  ";
                case 2:
                    return "    ";
                case 3:
                    return "      ";
                case 4:
                    return "        ";
                case 5:
                    return "          ";
                case 6:
                    return "            ";
                case 7:
                    return "              ";
                case 8:
                    return "                ";
                case 9:
                    return "                  ";
                default:
                    return new string(' ', indent * 2);
            }
        }
    }
}