using FactoryPattern.FactoryMethod;
using FactoryPattern.SimpleFactory;
using FactoryPattern.AbstractFactory;

public static class Program
{
    public static void Main()
    {
        //Simple Factory 
        Console.WriteLine("=== Simple Factory ===");
        PizzaSimpleFactory pizzaFactory = new();
        PizzaSimpleStore pizzaSimpleStore = new(pizzaFactory);

        pizzaSimpleStore.OrderPizza("cheese");
        pizzaSimpleStore.OrderPizza("pepperoni");
        pizzaSimpleStore.OrderPizza("hawaii");

        //Factory Method
        Console.WriteLine("\n=== Factory Method ===");
        PizzaStore nyStore = new NewYorkPizzaStore();
        nyStore.OrderPizza("cheese");
        nyStore.OrderPizza("pepperoni");

        PizzaStore chicagoStore = new ChicagoPizzaStore();
        chicagoStore.OrderPizza("cheese");
        chicagoStore.OrderPizza("pepperoni");

        //Abstract Factory
        Console.WriteLine("\n=== Abstract Factory ===");
        IPizzaFactory nyFactory = new NYAbstractFactory();
        IPizza nyPizza = nyFactory.CreatePizza();
        nyPizza.Dough();
        nyPizza.Cut();
        nyPizza.Serve();

        IPasta nyPasta = nyFactory.CreatePasta();
        nyPasta.Boil();
        nyPasta.MakeSauce();
        nyPasta.Serve();

        IPizzaFactory chicagoFactory = new ChicagoAbstractFactory();
        IPizza chicagoPizza = chicagoFactory.CreatePizza();
        chicagoPizza.Dough();
        chicagoPizza.Cut();
        chicagoPizza.Serve();

        IPasta chicagoPasta = chicagoFactory.CreatePasta();
        chicagoPasta.Boil();
        chicagoPasta.MakeSauce();
        chicagoPasta.Serve();
    }
}
