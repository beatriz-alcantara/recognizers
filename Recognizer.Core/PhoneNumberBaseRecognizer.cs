namespace Recognizer.Core;

public class PhoneNumberBaseRecognizer : BaseRecognizer
{
    protected override string Pattern => @"(\(\d\d\)\s9\d{4}-\d{4})|(\(\d\d\)\s9\d{8})|(\d\d\s9\d{8})";
}