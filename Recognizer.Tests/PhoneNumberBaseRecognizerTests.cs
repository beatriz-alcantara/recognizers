using Bogus;
using FluentAssertions;
using Recognizer.Core;
using Xunit.Abstractions;

namespace Recognizer.Tests;

public class PhoneNumberBaseRecognizerTests(ITestOutputHelper outputHelper)
{
    private readonly PhoneNumberBaseRecognizer _recognizer = new PhoneNumberBaseRecognizer();
    private readonly Faker _faker = new Faker();

    [Theory]
    [InlineData("## 9####-####")]
    [InlineData("(##)9####-####")]
    [InlineData("(##)9########")]
    [InlineData("##9####-####")]
    [InlineData("##9########")]
    public void GivenPhoneNumberInInvalidFormat_IsValid_ShouldReturnFalse(string format)
    {
        var phoneNumber = _faker.Phone.PhoneNumber(format);
        outputHelper.WriteLine("PhoneNumber: {0}", phoneNumber);
        
        var result = _recognizer.IsValid(phoneNumber);

        result.Should().BeFalse();
    }
    
    [Fact]
    public void GivenPhoneNumberStatingWithNumberDifferentOf9_IsValid_ShouldReturnFalse()
    {
        var firstNumber = _faker.Random.Int(1, 8);
        var phoneNumber = _faker.Phone.PhoneNumber($"(##) {firstNumber}####-####");
        outputHelper.WriteLine("PhoneNumber: {0}", phoneNumber);
        
        var result = _recognizer.IsValid(phoneNumber);

        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("## 9########")]
    [InlineData("(##) 9####-####")]
    [InlineData("(##) 9########")]
    public void GivenPhoneNumberWithValidFormat_IsValid_ShouldReturnTrue(string format)
    {
        var phoneNumber = _faker.Phone.PhoneNumber(format);
        outputHelper.WriteLine("PhoneNumber: {0}", phoneNumber);
        
        var result = _recognizer.IsValid(phoneNumber);

        result.Should().BeTrue();
    }
}