// See https://aka.ms/new-console-template for more information
using FactoryPattern.FactoryMethod;
using FactoryPattern.SimpleFactory;

public static class Program
{
    public static void Main(string[] args)
    {
        PizzaSimpleFactory pizzaFactory= new PizzaSimpleFactory();
        SimplePizzaStore pizzaStore = new SimplePizzaStore(pizzaFactory);
        ChicagoPizzaStore chickagoPizzaStore= new ChicagoPizzaStore();

        pizzaStore.OrderPizza("cheese");
        pizzaStore.OrderPizza("pepperoni");
        pizzaStore.OrderPizza("Hawaii");
    }
}



