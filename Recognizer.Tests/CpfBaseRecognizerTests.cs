using Bogus;
using FluentAssertions;
using Recognizer.Core;
using Xunit.Abstractions;

namespace Recognizer.Tests;

public class CpfBaseRecognizerTests(ITestOutputHelper outputHelper)
{
    private readonly Faker _faker = new();
    private readonly CpfBaseRecognizer _baseRecognizer = new();

    [Fact]
    public void GivenTextWithMoreThan14Digits_IsValid_ShouldReturnFalse()
    {
        var cpf = _faker.Random.Replace($"{GetRandomNumber()}.{GetRandomNumber()}.{GetRandomNumber()}-{GetRandomNumber()}");
        outputHelper.WriteLine("CPF: {0}", cpf);
        
        var result = _baseRecognizer.IsValid(cpf);
        
        result.Should().BeFalse();
        
        string GetRandomNumber() => new ('#', _faker.Random.Int(4, 7));
    }

    [Fact]
    public void GivenTextWith14DigitsButIsNotInCorrectFormat_IsValid_ShouldReturnFalse()
    {
        var cpf = _faker.Random.Replace("##############");
        outputHelper.WriteLine("CPF: {0}", cpf);
        
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
        outputHelper.WriteLine("CPF: {0}", cpf);
        
        var result = _baseRecognizer.IsValid(cpf);
        
        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("???.???.???-??")]
    [InlineData("?*?.?*?.?*?-??")]
    public void GivenTextWithValidFormatWithLetters_IsValid_ShouldReturnFalse(string format)
    {
        var cpf = _faker.Random.Replace(format);
        outputHelper.WriteLine("CPF: {0}", cpf);
        
        var result = _baseRecognizer.IsValid(cpf);
        
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenTextWithValidCpf_IsValid_ShouldReturnTrue()
    {
        var cpf = _faker.Random.Replace("###.###.###-##");
        outputHelper.WriteLine("CPF: {0}", cpf);

        var result = _baseRecognizer.IsValid(cpf);
        
        result.Should().BeTrue();
    }
}