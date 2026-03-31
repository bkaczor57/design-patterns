using System;

namespace FactoryPattern.SimpleFactory;

public interface IAnimal
{
    public void Speak();
}

public class Dog:IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Bark!");
    }
}

public class Cat : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Meow!");
    }
}

public static class AnimalFactory
{
    public static IAnimal Create(string type) => type.ToLower() switch {
        "cat" => new Cat(),
        "dog" => new Dog(),
        _ => throw new ArgumentException($"Unknown type: {type}")
    };
}
