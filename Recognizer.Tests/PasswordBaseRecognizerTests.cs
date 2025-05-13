using Bogus;
using FluentAssertions;
using Recognizer.Core;
using Xunit.Abstractions;

namespace Recognizer.Tests;

public class PasswordBaseRecognizerTests(ITestOutputHelper outputHelper)
{
    private readonly Faker _faker = new();
    private readonly PasswordBaseRecognizer _passwordBaseRecognizer = new();

    [Fact]
    public void GivenAPasswordWithoutNumber_IsValid_ShouldReturnFalse()
    {
        var text = _faker.Random.Replace("????????");
        var password = text[0] + text[1..].ToLower();
        outputHelper.WriteLine("Password: {0}", password);
        var result = _passwordBaseRecognizer.IsValid(password);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void GivenAPasswordWithoutLowerLetters_IsValid_ShouldReturnFalse()
    {
        var password = _faker.Random.Replace("?#******");
        outputHelper.WriteLine("Password: {0}", password);
        var result = _passwordBaseRecognizer.IsValid(password);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void GivenAPasswordWithoutUpperLetters_IsValid_ShouldReturnFalse()
    {
        var password = _faker.Random.Replace("?#******").ToLower();
        outputHelper.WriteLine("Password: {0}", password);
        var result = _passwordBaseRecognizer.IsValid(password);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void GivenAValidPassword_IsValid_ShouldReturnTrue()
    {
        var text = _faker.Random.Replace("???#****");
        var password = text[0] + text[1..].ToLower();
        outputHelper.WriteLine("Password: {0}", password);
        var result = _passwordBaseRecognizer.IsValid(password);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenAPasswordWithBlankSpace_IsValid_ShouldReturnFalse()
    {
        var text = _faker.Random.Replace("???# ****");
        var password = text[0] + text[1..].ToLower();
        outputHelper.WriteLine("Password: {0}", password);
        var result = _passwordBaseRecognizer.IsValid(password);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void GivenAPasswordLessThan8Chars_IsValid_ShouldReturnFalse()
    {
        var text = _faker.Random.Replace("???#**");
        var password = text[0] + text[1..].ToLower();
        outputHelper.WriteLine("Password: {0}", password);
        var result = _passwordBaseRecognizer.IsValid(password);
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenAnEmptyPassword_IsValid_ShouldReturnFalse()
    {
        var password = string.Empty;
        outputHelper.WriteLine("Password: {0}", password);
        var result = _passwordBaseRecognizer.IsValid(password);
        result.Should().BeFalse();
    }
}