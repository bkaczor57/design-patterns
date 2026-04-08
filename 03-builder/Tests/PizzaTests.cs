using ConstructionProblem.Pizza;

namespace Tests;

public class PizzaTests
{
    [Fact]
    public void Pizza_Should_Have_Size()
    {
        //Arrange
        Pizza pizza = new Pizza.Builder("large")
        .Build();

        var size = pizza.Size;

        Assert.Equal("large",size);

    }
}