using ConstructionProblem.Pizza;
// Problem teleskopowy
Console.WriteLine("Problem wielu konstruktorów - telescoping");

PizzaTelescoping pizzaTelescoping = new PizzaTelescoping("large", false, true, true, false, true);
Console.WriteLine(pizzaTelescoping);

// Problem Mutowalnosci w Object Initializer
Console.WriteLine("Problem mutowalności - obj initializer");
PizzaMutable pizzaMutable = new PizzaMutable { Size = "large", Cheese = true };
Console.WriteLine(pizzaMutable);
pizzaMutable.Bacon = true;
Console.WriteLine(pizzaMutable);


// Rozwiązanie - builder
Console.WriteLine("Builder do tworzenia");

Pizza pizza = new Pizza.Builder("Large")
    .WithBacon()
    .WithCheese()
    .Build();

Console.WriteLine(pizza);

