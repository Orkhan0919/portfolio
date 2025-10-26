using _12_InterfaceAbstraction;

Console.WriteLine("Sum>>1   Substraction>>2   Multiplication>>3   Divide>>4");
ICalculation opr1= new Calculation();
int opr = int.Parse(Console.ReadLine());
switch (opr)
{
    case 1:
        double sum =opr1.Sum();
        Console.WriteLine($"Your result : {sum:F3}");
        break;
    case 2:
        double sub = opr1.Substraction();
        Console.WriteLine($"Your result : {sub:F3}");
        break;
    case 4:
        double div = opr1.Division();
        Console.WriteLine($"Your result : {div:F3}");
        break;
    case 3:
        double mul = opr1.Substraction();
        Console.WriteLine($"Your result : {mul:F3}");
        break;
}