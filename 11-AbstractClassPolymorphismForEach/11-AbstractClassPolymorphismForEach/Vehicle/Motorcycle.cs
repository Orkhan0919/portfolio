namespace _11_AbstractClassPolymorphismForEach.Vehicle;

internal class Motorcycle:Vehicle
{
    public string EngineCapacity { get; set; }
    public bool HasSidecar { get; set; }
    public string Type { get; set; }

    public Motorcycle(string brand, string model, int year,string engineCapacity,int maxSpeed,
    bool hasSidecar, string type,string plateNumber,int maxspeed,double fuelLevel=100)
        :base(brand,model,year,plateNumber,maxspeed,fuelLevel)

    {
        EngineCapacity = engineCapacity;
        HasSidecar = hasSidecar;
        Type = type;
        
    }
    public override string GetVehicleInfo()
    {
        return
            $"Brand: {Brand}, Model: {Model}, Year: {Year},Engine Capacity: {EngineCapacity},Type: {Type}," +
            $"Has sidecar:{HasSidecar}Max Speed: {Maxspeed}";

    }
    public override double CalculateFuelCost()
    {
        
        int a = 300;
        int currentLoad = int.Parse(Console.ReadLine());
        double result=(a/100)*(25+currentLoad)*1.80;
        return result; 
    }

    public void ShowMotorcycleInfo()
    {
        string a=GetVehicleInfo();
        Console.WriteLine(a);
    }
}