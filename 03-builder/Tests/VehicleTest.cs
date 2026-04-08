using GOFStructure.Vehicles;
using Xunit.Abstractions;

namespace Tests
{
    public class VehicleTest()
    {
        [Fact]
        public void Vehicle_Have_All_Parts()
        {
            ArgumentDirector director = new ArgumentDirector();
            TruckBuilder builder = new TruckBuilder();
            director.Construct(builder);

            var truck = builder.GetResult();

            List<String> truck_items = new List<String>
            {
                "Ladder Frame - Reinforced Steel",
                "6.7L Diesel 400 KM",
                "6 x 315"
            };

            Assert.Equal(truck_items, truck.Parts);
        }

    }
}