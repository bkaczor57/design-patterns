using System;

namespace FactoryPattern.FactoryMethod;

public abstract class FactoryMethodPizza
{
    public String Name {get;set;} = String.Empty;
    public String Dough {get;set;} = String.Empty;
    public String Sauce {get;set;} = String.Empty;
    public List<string> Toppings { get; protected set; } = [];
    public virtual void Prepare()
    {
        Console.WriteLine($"Nazwa: {Name}");
        Console.WriteLine($"Ciasto: {Dough}");
        Console.WriteLine($"Sos: {Sauce}");
        Console.WriteLine($"Dodatki: {string.Join(", ",Toppings)}");
    }
    public virtual void Bake() => Console.WriteLine($"Pieczenie {Name}");
    public virtual void Cut() => Console.WriteLine($"Krojenie {Name}");
}
