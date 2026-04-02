using System;

namespace GOFStructure.Vehicles;

public class Vehicle
{
    private readonly string _vehicleType;
    private readonly List<string> _parts = new();

    public Vehicle(string vehicleType) => _vehicleType = vehicleType;

    public void AddPart(string part) => _parts.Add(part);
    public void Show()
    {
        Console.WriteLine($"type: {_vehicleType}, Parts: {String.Join(", ",_parts)}");
    }
}

public interface IBuilder
{
    void BuildFrame();
    void BuildEngine();
    void BuildWheels();
    Vehicle GetResult();
}

public class CarBuilder : IBuilder
{
    private Vehicle _vehicle;
    public CarBuilder() => _vehicle = new Vehicle("car");
    public void BuildFrame() => _vehicle.AddPart("Car Frame");
    public void BuildEngine() => _vehicle.AddPart("V8 Engine");   
    public void BuildWheels() => _vehicle.AddPart("4 Car Wheels");   
    public Vehicle GetResult() => _vehicle;
}

public class MotorBuilder : IBuilder
{
    private Vehicle _vehicle;
    public MotorBuilder() => _vehicle = new Vehicle("Motor");
    public void BuildFrame() => _vehicle.AddPart("Motor Frame");
    public void BuildEngine() => _vehicle.AddPart("Motor Engine");   
    public void BuildWheels() => _vehicle.AddPart("2 Motor Wheels");   
    public Vehicle GetResult() => _vehicle;

}

/*
Dyrektorzy są ważni gdy potrzebujemy odpowiednią kolejność wywołania obiektu
*/
public class Director
{
    private readonly IBuilder _builder;
    public Director(IBuilder builder)
    {
        _builder = builder;
    }

    public void Construct()
    {
        _builder.BuildFrame();
        _builder.BuildEngine();
        _builder.BuildWheels();
    }
}

public class ArgumentDirector
{
    public void Construct(IBuilder builder)
    {
        builder.BuildFrame();
        builder.BuildFrame();
        builder.BuildFrame();
    }
}