// See https://aka.ms/new-console-template for more information
using GOFStructure.Vehicles;


CarBuilder carBuilder = new CarBuilder();
ArgumentDirector director = new ArgumentDirector();
director.Construct(carBuilder);
carBuilder.GetResult().Show();

MotorBuilder motorBuilder = new MotorBuilder();
director.Construct(motorBuilder);
motorBuilder.GetResult().Show();
