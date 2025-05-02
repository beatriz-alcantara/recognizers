using System.Text.RegularExpressions;

namespace Recognizer.Core;

public abstract class BaseRecognizer
{
    protected virtual string Pattern => "";

    public bool IsValid(string text)
    {
        var match = Regex.Match(text, Pattern);
        return match.Success && match.Value == text;
    }
}