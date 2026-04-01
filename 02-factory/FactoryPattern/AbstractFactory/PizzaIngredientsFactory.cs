using System;

namespace FactoryPattern.AbstractFactory;

// Abstract products
public interface IDough { String Description {get;} }
public interface ISauce { String Description {get;} }
public interface ICheese{ String Description {get;} }
public interface IMeat  { String Description {get;} }

// NY Products
public class ThinCrustDough : IDough { public String Description => "Thin Crusty Dough"; }
public class ThickCrustDough: IDough { public String Description => "Thick Crusty Dough"; }
public class Pomodoro : ISauce { public String Description => "Pomodoro Sauce"; }
public class Creamy : ISauce { public String Description => "Creamy Sauce"; }
public class Mozarella : ICheese { public String Description => "Mozzarella"; }
public class Parmeggiano : ICheese {public String Description => "Parmeggiano Cheese";}
public class Pepperoni : IMeat { public String Description => "Pepperoni"; }
public class Ham : IMeat { public String Description => "Ham";}

//Abstract Factory - fabryka całej rodziny produktów
public interface IPizzaIngredientFactory
{
    IDough CreateDough();
    ISauce CreateSauce();
    ICheese CreateCheese();
    IMeat CreateMeat();
}

// Konkretna fabryka 
public class NYPizzaIngredientFactory : IPizzaIngredientFactory
{
    public IDough CreateDough() => new ThinCrustDough();
    public ISauce CreateSauce() => new Pomodoro();
    public ICheese CreateCheese() => new Mozarella();
    public IMeat CreateMeat() => new Pepperoni();
}

public class NapoliPizzaIngredientFactory : IPizzaIngredientFactory
{
    public IDough CreateDough() => new ThinCrustDough();
    public ISauce CreateSauce() => new Pomodoro();
    public ICheese CreateCheese() => new Parmeggiano();
    public IMeat CreateMeat() => new Ham();
}

// Klient 


public abstract class Dish
{
    public String Name {get;set;} = String.Empty;
    public virtual void Serve() => Console.WriteLine($"Serving {Name}");
}

public abstract class Pizza : Dish{

    protected IDough? Dough;
    protected ISauce? Sauce;
    protected ICheese? Cheese;
    protected IMeat? Meat;
    public abstract void Prepare();
    public virtual void Bake()  => Console.WriteLine("  Bake 25 min at 200°C ");
    public virtual void Cut()   => Console.WriteLine("  Cut into diagonal slices ");

    public override string ToString() => Name;
}

public class CheesePizza : Pizza
{
    private readonly IPizzaIngredientFactory _factory;
    public CheesePizza(IPizzaIngredientFactory factory) => _factory = factory;

    public override void Prepare()
    {
        Console.WriteLine($"Preparing {Name}");
        Dough  = _factory.CreateDough();
        Sauce  = _factory.CreateSauce();
        Cheese = _factory.CreateCheese();
        Console.WriteLine($"  Dough : {Dough.Description}");
        Console.WriteLine($"  Sauce : {Sauce.Description}");
        Console.WriteLine($"  Cheese: {Cheese.Description}");
    }
}

public class MeatPizza : Pizza
{
    private readonly IPizzaIngredientFactory _factory;
    public MeatPizza(IPizzaIngredientFactory factory) => _factory = factory;

    public override void Prepare()
    {
        Console.WriteLine($"Preparing {Name}");
        Dough  = _factory.CreateDough();
        Sauce  = _factory.CreateSauce();
        Cheese = _factory.CreateCheese();
        Meat  = _factory.CreateMeat();
        Console.WriteLine($"  Dough : {Dough.Description}");
        Console.WriteLine($"  Sauce : {Sauce.Description}");
        Console.WriteLine($"  Cheese: {Cheese.Description}");
        Console.WriteLine($"  Clams : {Meat.Description}");
    }
}


// ----- PizzaStore (używa fabryki) ------------------------------------

public abstract class PizzaStore
{
    protected abstract Pizza CreatePizza(string type);

    public Pizza OrderPizza(string type)
    {
        Console.WriteLine($"\n=== {GetType().Name}: Zamówienie '{type}' ===");
        var pizza = CreatePizza(type);
        pizza.Prepare();
        pizza.Bake();
        pizza.Cut();
        Console.WriteLine($"  Gotowe: {pizza}");
        return pizza;
    }
}

public class NYPizzaStore : PizzaStore
{
    private readonly IPizzaIngredientFactory _factory = new NYPizzaIngredientFactory();

    protected override Pizza CreatePizza(string type)
    {
        Pizza pizza = type.ToLower() switch
        {
            "cheese" => new CheesePizza(_factory),
            "meat"   => new MeatPizza(_factory),
            _ => throw new ArgumentException($"Nieznany typ: {type}")
        };
        pizza.Name = $"New York Style {type} Pizza";
        return pizza;
    }
}

public class ChicagoPizzaStore : PizzaStore
{
    private readonly IPizzaIngredientFactory _factory = new NapoliPizzaIngredientFactory();

    protected override Pizza CreatePizza(string type)
    {
        Pizza pizza = type.ToLower() switch
        {
            "cheese" => new CheesePizza(_factory),
            "meat"   => new MeatPizza(_factory),
            _ => throw new ArgumentException($"Nieznany typ: {type}")
        };
        pizza.Name = $"Chicago Style {type} Pizza";
        return pizza;
    }
}

