using Bogus;
using FluentAssertions;
using Recognizer.Core;
using Xunit.Abstractions;

namespace Recognizer.Tests;

public class NameRecognizerTests
{
    private readonly Faker _faker = new();
    private readonly NameRecognizer _nameRecognizer = new();
    private readonly ITestOutputHelper _outputHelper;
    
    public NameRecognizerTests(ITestOutputHelper testOutputHelper)
    {
        _outputHelper = testOutputHelper;
    }
    
    [Fact]
    public void GivenTextWithNumeric_IsValid_ShouldReturnFalse()
    {
        var name = _faker.Random.Replace("###****** ******");
        _outputHelper.WriteLine(name);
        
        var result = _nameRecognizer.IsValid(name);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void GivenNameWithAllLettersLowerCase_IsValid_ShouldReturnFalse()
    {
        var name = _faker.Random.Replace("??????? ?????").ToLower();
        _outputHelper.WriteLine(name);
        
        var result = _nameRecognizer.IsValid(name);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void GivenNameWithAllLettersUpperCase_IsValid_ShouldReturnFalse()
    {
        var name = _faker.Random.Replace("??????? ?????");
        _outputHelper.WriteLine(name);
        
        var result = _nameRecognizer.IsValid(name);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void GivenNameValidName_IsValid_ShouldReturnTrue()
    {
        var first = _faker.Random.Replace(new string('?', _faker.Random.Number(2, 15)));
        var last = _faker.Random.Replace(new string('?', _faker.Random.Number(2, 15)));
        var name = $"{first[0] + first.Substring(1).ToLower()} {last[0] + last.Substring(1).ToLower()}";
        _outputHelper.WriteLine(name);
        
        var result = _nameRecognizer.IsValid(name);
        
        result.Should().BeTrue();
    }
}