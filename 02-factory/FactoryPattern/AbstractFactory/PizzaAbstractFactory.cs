using System;

namespace FactoryPattern.AbstractFactory;

public interface IDish
{
    void Serve();
}

public interface IPizza : IDish
{
    void Dough();
    void Cut();
}

public interface IPasta : IDish
{
    void Boil();
    void MakeSauce();
}

public interface IPizzaFactory
{
    IPasta CreatePasta();
    IPizza CreatePizza();
}

public class NYAbstractFactory : IPizzaFactory
{
    public IPasta CreatePasta() => new NYPasta();
    public IPizza CreatePizza() => new NYPizza();
}

public class ChicagoAbstractFactory : IPizzaFactory
{
    public IPasta CreatePasta() => new ChicagoPasta();
    public IPizza CreatePizza() => new ChicagoPizza();
}

public class NYPizza : IPizza
{
    public void Dough() => Console.WriteLine("NY Pizza: Cienkie ciasto");
    public void Cut() => Console.WriteLine("NY Pizza: Krojenie na trójkąty");
    public void Serve() => Console.WriteLine("Podawanie NY Pizza");
}

public class NYPasta : IPasta
{
    public void Boil() => Console.WriteLine("NY Pasta: Gotowanie al dente");
    public void MakeSauce() => Console.WriteLine("NY Pasta: Sos marinara");
    public void Serve() => Console.WriteLine("Podawanie NY Pasta");
}

public class ChicagoPizza : IPizza
{
    public void Dough() => Console.WriteLine("Chicago Pizza: Grube ciasto deep dish");
    public void Cut() => Console.WriteLine("Chicago Pizza: Krojenie na kwadraty");
    public void Serve() => Console.WriteLine("Podawanie Chicago Pizza");
}

public class ChicagoPasta : IPasta
{
    public void Boil() => Console.WriteLine("Chicago Pasta: Gotowanie z masłem");
    public void MakeSauce() => Console.WriteLine("Chicago Pasta: Gęsty sos pomidorowy");
    public void Serve() => Console.WriteLine("Podawanie Chicago Pasta");
}
