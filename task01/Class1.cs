namespace task01;

using System.Linq;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (string.IsNullOrEmpty(input)) return false;
	var filt_input = input.ToLower().Where(x => !char.IsPunctuation(x) && !char.IsWhiteSpace(x));
	return filt_input.SequenceEqual(filt_input.Reverse());
    }
}
