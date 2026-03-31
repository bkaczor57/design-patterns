namespace FactoryPattern.SimpleFactory;

public abstract class SimplePizza
{
    public String Name {get;set;} = String.Empty;
    public String Dough {get;set;} = String.Empty;
    public String Sauce {get;set;} = String.Empty;
    public List<string> Toppings { get; protected set; } = [];
    public  void Prepare()
    {
        Console.WriteLine($"Nazwa: {Name}");
        Console.WriteLine($"Ciasto: {Dough}");
        Console.WriteLine($"Sos: {Sauce}");
        Console.WriteLine($"Dodatki: {string.Join(", ",Toppings)}");
    }
    public void Bake() => Console.WriteLine($"Pieczenie {Name}");
    public void Cut() => Console.WriteLine($"Krojenie {Name}");
}
