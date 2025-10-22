namespace _11_AbstractClassPolymorphismForEach.Vehicle;

internal class Car : Vehicle
{
    public int DoorsCount{ get; set; }
    public int TrunkCapacity{ get; set; }
    public bool IsAutomatic { get; set; }
    public int MaxSpeed{ get; set; }

    public Car(string brand, string model, int year, string plateNumber,
        int doorsCount, int trunkCapacity, bool isAutomatic, int maxspeed):base(brand,model,year,plateNumber,maxspeed)
    {
        DoorsCount = doorsCount;
        TrunkCapacity = trunkCapacity;
        IsAutomatic = isAutomatic;
        MaxSpeed = maxspeed;
    }
    public override string GetVehicleInfo()
    {
        return $"Brand: {Brand},Model: {Model},Year: {Year},Doors: {DoorsCount},runk: {TrunkCapacity}  T, " +
               $"Automatic: {IsAutomatic}, MaxSpeed: {MaxSpeed}";
    }
    
    
    public void ShowCarInfo()
    {
        string a=GetVehicleInfo();
        Console.WriteLine(a);
    }

    public override double CalculateFuelCost()
    {
        int a = 500;
        double result=(a/100)*8*1.50;
        return result; 
    }
}