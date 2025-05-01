namespace Recognizer.Core;

public class EmailBaseRecognizer : BaseRecognizer
{
    protected override string Pattern => @"[a-z]+@[a-z]+\.br";
}