using System.Text.RegularExpressions;

namespace Recognizer.Core;

public abstract class BaseRecognizer
{
    protected virtual string Pattern => "";
    public bool IsValid(string text) => Regex.IsMatch(text, Pattern);
}