namespace Recognizer.Core;

public class PasswordBaseRecognizer : BaseRecognizer
{
    protected override string Pattern => @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8}$";
}