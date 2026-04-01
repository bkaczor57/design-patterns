 using System;

namespace FactoryPattern.SimpleFactory;

public abstract class Pizza
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


public class CheesePizza : Pizza
{
    public CheesePizza()
    {
        Name = "Margharita";
        Dough = "Cienkie";
        Sauce = "Pomidorowy";
        Toppings = ["Ser"];
    }
}

public class PepperoniPizza: Pizza
{
    public PepperoniPizza()
    {
        Name = "Pepperoni";
        Dough = "Grube";
        Sauce = "Pomidorowy";
        Toppings = ["Ser","Pepperoni"];
    }
}

public class PizzaSimpleFactory
{
    public Pizza? CreatePizza(String type)
    {
        switch (type.ToLower())
        {
            case "cheese": 
                return new CheesePizza();
            case "pepperoni":
                return new PepperoniPizza();
            default:
                return null;
        }
    }
}


public class PizzaSimpleStore
{
    public readonly PizzaSimpleFactory _pizzaFactory;

    public PizzaSimpleStore(PizzaSimpleFactory pizzaFactory)
    {
        _pizzaFactory = pizzaFactory;
    }

    public Pizza? OrderPizza(string value)
    {
        var pizza = _pizzaFactory.CreatePizza(value);
        if(pizza is null)
        {
            Console.WriteLine($"Nie mamy pizzy {value}!");
            return null;
        }

        pizza.Prepare();
        pizza.Bake();
        pizza.Cut();
        
        return pizza;

    }
}
