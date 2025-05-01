using System.Text.RegularExpressions;

namespace Recognizer.Core;

public class CpfRecognizer : IRecognizer
{
    private const string Pattern = @"\d{3}.\d{3}.\d{3}-\d{2}";
    public bool IsValid(string text) => Regex.IsMatch(text, Pattern);
}