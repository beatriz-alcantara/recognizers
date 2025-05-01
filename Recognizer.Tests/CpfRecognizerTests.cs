using Bogus;
using FluentAssertions;
using Recognizer.Core;

namespace Recognizer.Tests;

public class CpfRecognizerTests
{
    private readonly Faker _faker = new();
    private readonly CpfRecognizer _recognizer = new();
    
    [Fact]
    public void GivenTextWithMoreThan14Digits_IsValid_ShouldReturnFalse()
    {
        var randomNumbers = () => new string('#', _faker.Random.Int(4, 7));
        var cpf = _faker.Random.Replace($"{randomNumbers}.{randomNumbers}.{randomNumbers}-{randomNumbers}");

        var result = _recognizer.IsValid(cpf);
        
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenTextWith14DigitsButIsNotInCorrectFormat_IsValid_ShouldReturnFalse()
    {
        var cpf = _faker.Random.Replace("##############");

        var result = _recognizer.IsValid(cpf);
        
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
        
        var result = _recognizer.IsValid(cpf);
        
        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("???.???.???-??")]
    [InlineData("?*?.?*?.?*?-??")]
    public void GivenTextWithValidFormatWithLetters_IsValid_ShouldReturnFalse(string format)
    {
        var cpf = _faker.Random.Replace(format);
        
        var result = _recognizer.IsValid(cpf);
        
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenTextWithValidCpf_IsValid_ShouldReturnTrue()
    {
        var cpf = _faker.Random.Replace("###.###.###-##");

        var result = _recognizer.IsValid(cpf);
        
        result.Should().BeTrue();
    }
}