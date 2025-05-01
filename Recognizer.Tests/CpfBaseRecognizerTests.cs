using Bogus;
using FluentAssertions;
using Recognizer.Core;
using Xunit.Abstractions;

namespace Recognizer.Tests;

public class CpfBaseRecognizerTests
{
    private readonly Faker _faker = new();
    private readonly CpfBaseRecognizer _baseRecognizer = new();
    private readonly ITestOutputHelper _outputHelper;

    public CpfBaseRecognizerTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [Fact]
    public void GivenTextWithMoreThan14Digits_IsValid_ShouldReturnFalse()
    {
        var cpf = _faker.Random.Replace($"{GetRandomNumber()}.{GetRandomNumber()}.{GetRandomNumber()}-{GetRandomNumber()}");
        _outputHelper.WriteLine(cpf);
        var result = _baseRecognizer.IsValid(cpf);
        
        result.Should().BeFalse();
        
        string GetRandomNumber() => new ('#', _faker.Random.Int(4, 7));
    }

    [Fact]
    public void GivenTextWith14DigitsButIsNotInCorrectFormat_IsValid_ShouldReturnFalse()
    {
        var cpf = _faker.Random.Replace("##############");

        var result = _baseRecognizer.IsValid(cpf);
        
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("##.###.###-###")]
    [InlineData("###.##.###-###")]
    [InlineData("##.####.###-##")]
    [InlineData("##-###.###.###")]
    public void GivenTextWithInvalidFormat_IsValid_ShouldReturnFalse(string format)
    {
        var cpf = _faker.Random.Replace(format);
        
        var result = _baseRecognizer.IsValid(cpf);
        
        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("???.???.???-??")]
    [InlineData("?*?.?*?.?*?-??")]
    public void GivenTextWithValidFormatWithLetters_IsValid_ShouldReturnFalse(string format)
    {
        var cpf = _faker.Random.Replace(format);
        
        var result = _baseRecognizer.IsValid(cpf);
        
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenTextWithValidCpf_IsValid_ShouldReturnTrue()
    {
        var cpf = _faker.Random.Replace("###.###.###-##");

        var result = _baseRecognizer.IsValid(cpf);
        
        result.Should().BeTrue();
    }
}