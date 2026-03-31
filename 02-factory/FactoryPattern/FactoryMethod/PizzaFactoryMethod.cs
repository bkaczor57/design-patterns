using System;
using FactoryPattern.SimpleFactory;

namespace FactoryPattern.FactoryMethod;


public abstract class PizzaStore
{
    // Metoda wytwórcza - dzięki temu tu nic nie zmieniamy
    protected abstract FactoryMethodPizza CreatePizza(string type);

    // pizza jest już wybrana w PizzaStore nic już nie zmieniamy
    public FactoryMethodPizza OrderPizza(string type)
    {
        var pizza = CreatePizza(type);
        pizza.Prepare();
        pizza.Bake();
        pizza.Cut();
        return pizza;
    }
    
}

// Creator przekazuje odpowiednie Create Pizza
// Tu już korzysta z Simple Factory wewnątrz - tutaj w razie nowej pizzy trzeba zmieniać lub dodawać nowe rzeczy
// Pokazuje to że wystarczy dorobić nową metodę która overriduje CreatePizza i wykorzystuje własne rozwiązania
public class NewYorkPizzaStore : PizzaStore
{
    protected override FactoryMethodPizza CreatePizza(string type) => type.ToLower() switch
    {
        "cheese" => new NYCheesePizza(),
        "pepperoni" => new NYPepperoniPizza(),
        _ => throw new ArgumentException($"NY nie zna pizzy {type}") 
        
    };
}
public class ChicagoPizzaStore : PizzaStore
{
    protected override FactoryMethodPizza CreatePizza(string type) => type.ToLower() switch
    {
        "cheese" => new ChicagoCheesePizza(),
        "pepperoni" => new ChicagoPepperoniPizza(),
        _ => throw new ArgumentException($"NY nie zna pizzy {type}") 
    };
    
}



public class NYCheesePizza : FactoryMethodPizza
{
    public NYCheesePizza()
    {
        Name = "New York Cheese Pizza";
        Dough = "Cienkie";
        Sauce = "Pomidorowy";
        Toppings = ["Ser Mozarella"];
    }

    public override void Bake() => Console.Write("Pieczenie w 250 stopniach");

}

public class NYPepperoniPizza: FactoryMethodPizza
{    public NYPepperoniPizza()
    {
        Name = "New York Pepperoni Pizza";
        Dough = "Cienkie";
        Sauce = "Pomidorowy";
        Toppings = ["Ser Mozarella","Pepperoni"];
    }

    public override void Bake() => Console.Write("Pieczenie w 250 stopniach");
    
}

public class ChicagoCheesePizza : FactoryMethodPizza
{
    public ChicagoCheesePizza()
    {
        Name = "New York Cheese Pizza";
        Dough = "Grube";
        Sauce = "Pomidorowy";
        Toppings = ["Ser Mozarella"];
    }

    public override void Bake() => Console.Write("Pieczenie wolno 180 stopniach");

}

public class ChicagoPepperoniPizza: FactoryMethodPizza
{    public ChicagoPepperoniPizza()
    {
        Name = "Chicago Pepperoni Pizza";
        Dough = "Grube";
        Sauce = "Pomidorowy";
        Toppings = ["Ser Mozarella","Pepperoni"];
    }

    public override void Bake() => Console.Write("Pieczenie wolno w 180 stopniach");
    
}


