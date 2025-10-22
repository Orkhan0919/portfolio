
using System.Net.Http.Headers;
using _11_AbstractClassPolymorphismForEach.Vehicle;

#region Objects

Car car1 = new("Mercedes", "E200", 2023,"00AA001",4, 500,true, 220);

Car car2 = new("BMW", "320i", 2022,"00AA002",4, 480,true, 235);

Car car3 = new("Toyota", "Carmy", 2021,"00AA003",4, 524,true, 210);

Motorcycle moto1= new("Yamaha","R1",2023,"998cc", 299,
    false, "Sport","00AA005",299);

Motorcycle moto2= new("Harley","Davidson",2022,"1868cc", 180,
    true, "Cruiser","00AA006",299);
    
Truck truck1=new("MAN","TGX",2020,"00aa007",18,3,12,110);
Truck truck2=new("Volvo","FH16",2021,"00aa007",25,4,18,110);

#endregion

Car[] cars={car1,car2,car3};
Truck[] trucks={truck1,truck2};
Motorcycle[] motos={moto1,moto2};
Vehicle[] vehicles={car1,car2,car3,moto1,moto2,truck1,truck2};
double sumSpeed = 0;
#region Processes

double maxFuelCost = 0;


foreach (Car car in cars )
{
    car.ShowCarInfo();
    double a =car.CalculateFuelCost();
    Console.WriteLine("Your fuel cost :"+a);
}

foreach (Truck truck in trucks )
{
    truck.ShowTruckInfo();
}

foreach (Motorcycle moto in motos )
{
    moto.ShowMotorcycleInfo();
   
}

foreach (Truck truck in trucks)
{
    Console.WriteLine($"Enter current load for {truck.Model}:");
    double a = truck.CalculateFuelCost();
    Console.WriteLine("Your fuel cost :" + a);
}

foreach (Motorcycle moto in motos)
{
        Console.WriteLine($"Enter current load for  {moto.Model}:");
        double a = moto.CalculateFuelCost();
        Console.WriteLine("Your fuel cost :" + a);
}
foreach (Vehicle vehicle in vehicles)
{
    
    sumSpeed += vehicle.Maxspeed;
    
}
double avgSpeed = sumSpeed / vehicles.Length;
Console.WriteLine("Average speed for all vehicles:"+avgSpeed);

foreach (Car car in cars)
{
    double fuelCost = car.CalculateFuelCost(); 
    if (fuelCost > maxFuelCost)
    {
        maxFuelCost = fuelCost;
    }
}
foreach (Car car in cars)
{
    if (car.CalculateFuelCost() == maxFuelCost)
    {
        Console.WriteLine($"Max fuel cost is {maxFuelCost} and the car is {car.Model}");
    }
}

#endregion