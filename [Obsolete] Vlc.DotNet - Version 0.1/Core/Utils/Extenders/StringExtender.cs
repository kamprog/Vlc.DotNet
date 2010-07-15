namespace Vlc.DotNet.Core.Utils.Extenders
{
    public static class StringExtender
    {
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static string FormatIt(this string source, params object[] args)
        {
            return string.Format(source, args);
        }

        public static string ConcatIt(this string source, params string[] args)
        {
            return string.Concat(source, string.Concat(args));
        }
    }
}