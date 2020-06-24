namespace Core.Model
{
    public static class StringExtensions
    {
        public static string Sluggify(this string str)
        {
            str = str.Replace(":", "_");
            str = str.Replace("/", "_");

            return str;
        }
    }
}
