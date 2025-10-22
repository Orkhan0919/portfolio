namespace _11_AbstractClassPolymorphismForEach.Vehicle;

public class Truck:Vehicle
{
    public double CargoCapacity { get; set; }
    public int AxleCount { get; set; }
    public double CurrnetLoad { get; set; }
    public int Maxspeed {get; set;}

    public Truck(string brand, string model, int year, string plateNumber,
        double cargoCapacity,int axleCount,double currnetLoad,int maxspeed, double fuelLevel = 100)
        :base(brand,model,year,plateNumber,maxspeed,fuelLevel)
    {
        CargoCapacity = cargoCapacity;
        AxleCount = axleCount;
        CurrnetLoad = currnetLoad;
        Maxspeed = maxspeed;
        
    }

    public void ShowTruckInfo()
    {
        string a=GetVehicleInfo();
        Console.WriteLine(a);
    }

    public override string GetVehicleInfo()
    {
        return $"Brand: {Brand}, Model: {Model}, Year: {Year}, Cargo capacity :{CargoCapacity}, " +
               $"Axle Count: {AxleCount}, Current Load: {CurrnetLoad}, MaxSpeed: {Maxspeed}";

    }

    public override double CalculateFuelCost()
    {

        int a = 100;
        int currentLoad = int.Parse(Console.ReadLine());
        double result=(a/100)*(25+currentLoad)*1.80;
        return result; 
    }
}
