 using System;

namespace FactoryPattern.SimpleFactory;

public class CheesePizza : SimplePizza
{
    public CheesePizza()
    {
        Name = "Margharita";
        Dough = "Cienkie";
        Sauce = "Pomidorowy";
        Toppings = ["Ser"];
    }
}

public class PepperoniPizza: SimplePizza
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
    public SimplePizza? CreatePizza(String type)
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

    public SimplePizza? OrderPizza(string value)
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
