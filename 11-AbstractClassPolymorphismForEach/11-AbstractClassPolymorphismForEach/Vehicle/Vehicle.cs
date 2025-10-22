namespace _11_AbstractClassPolymorphismForEach.Vehicle;

public abstract class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }

    public int Year { get; set; }
       
    public string PlateNumber{ get; set; }
    public double FuelLevel { get; set; }
    public int Maxspeed { get; set; }
    
    
    public Vehicle(string brand, string model, int year, string plateNumber, int maxspeed,double fuelLevel=100)
    {
        Brand=brand;
        Model=model;
        Year = year;
        PlateNumber = plateNumber;
        FuelLevel=fuelLevel;
        Maxspeed=maxspeed;
    }
    public abstract string GetVehicleInfo();

    public virtual void ShowBasicInfo()
    {
        Console.WriteLine($"Brand: {Brand}\nModel: {Model}\nYear: {Year}");
    }
    public abstract double CalculateFuelCost();
}

