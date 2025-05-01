using System.Text.RegularExpressions;

namespace Recognizer.Core;

public class CpfBaseRecognizer : BaseRecognizer
{
    protected override string Pattern => @"\d{3}\.\d{3}\.\d{3}-\d{2}";
}