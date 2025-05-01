namespace Recognizer.Core;

public class NameRecognizer : BaseRecognizer
{
    protected override string Pattern => @"[A-Z]{1}[a-z]+\s[A-Z]{1}[a-z]+";
}