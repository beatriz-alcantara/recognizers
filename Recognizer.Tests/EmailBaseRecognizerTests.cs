using Bogus;
using FluentAssertions;
using Recognizer.Core;

namespace Recognizer.Tests;

public class EmailBaseRecognizerTests
{
    private readonly EmailBaseRecognizer _baseRecognizer = new();
    private readonly Faker _faker = new();
    
    [Theory]
    [InlineData("???@.br")]
    [InlineData("@.br")]
    [InlineData("??@??.??")]
    [InlineData("??.br")]
    [InlineData("????")]
    public void GivenEmailWithInvalidFormat_IsValid_ShouldReturnFalse(string format)
    {
        var email = _faker.Random.Replace(format);
        var result = _baseRecognizer.IsValid(email);
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenValidEmail_IsValid_ShouldReturnTrue()
    {
        var email = _faker.Random.Replace($"{GetRandomString()}@{GetRandomString()}.br").ToLower();
        
        var result = _baseRecognizer.IsValid(email);
        
        result.Should().BeTrue();

        string GetRandomString() => new string('?', _faker.Random.Int(3, 10));
    }
    
    [Theory]
    [InlineData("###@.br")]
    [InlineData("#*@#*.br")]
    [InlineData("##@##.##")]
    [InlineData("####.br")]
    [InlineData("####")]
    public void GivenEmailWithNumber_IsValid_ShouldReturnFalse(string format)
    {
        var email = _faker.Random.Replace(format);
        var result = _baseRecognizer.IsValid(email);
        result.Should().BeFalse();
    }
}