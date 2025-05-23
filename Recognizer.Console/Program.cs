﻿// See https://aka.ms/new-console-template for more information

using Recognizer.Core;

Console.WriteLine("Hello, World!");
var validationType = string.Empty;
do
{
    Console.WriteLine("\n =====================================");
    Console.WriteLine("Se deseja sair dessa execução não digite nenhum numero, e aperte enter!");
    Console.WriteLine("Escolha um tipo de validação: ");
    Console.WriteLine("1 - Nome e sobrenome");
    Console.WriteLine("2 - CPF");
    Console.WriteLine("3 - E-mail");
    Console.WriteLine("4 - Senha");
    Console.WriteLine("5 - Número de telefone");
    
    BaseRecognizer? recognizer = null;
    bool result = false;
    while (recognizer == null)
    {
        Console.Write("Informe um dos valores listados acima: ");
        validationType = Console.ReadLine();
    
        if (string.IsNullOrWhiteSpace(validationType))
            return;
        
        if (int.TryParse(validationType, out var type) is false)
            continue;
        
        recognizer = FactoryRecognizer(type);
        if (recognizer == null)
            continue;
        
        Console.Write("Digite o texto que deseja validar: ");
        
        var text = Console.ReadLine() ?? string.Empty;
        result = recognizer.IsValid(text);
    }
    
    Console.WriteLine("O texto e {0}!", result ? "valido" : "invalido");
} while(string.IsNullOrWhiteSpace(validationType) is false);

BaseRecognizer? FactoryRecognizer(int validation)
{
    return validation switch
    {
        1 => new NameRecognizer(),
        2 => new CpfBaseRecognizer(),
        3 => new EmailBaseRecognizer(),
        4 => new PasswordBaseRecognizer(),
        5 => new PhoneNumberBaseRecognizer(),
        _ => null
    };
}