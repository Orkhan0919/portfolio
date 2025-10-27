// See https://aka.ms/new-console-template for more information

using _13_NullableEnumStruct.Enums;
using _13_NullableEnumStruct.Models;

DrinkOrder order1 = new DrinkOrder(101, "Ali", DrinkType.Coffee, DrinkSize.Medium);
DrinkOrder order2 =new (102, "Leyla", DrinkType.Tea, DrinkSize.Large);
DrinkOrder order3 = new DrinkOrder(103, "Vüqar", DrinkType.Juice, DrinkSize.Small);

DrinkOrder[] drinks = {order1, order2, order3};
Console.WriteLine("---Drinks---");
foreach (DrinkType drink in Enum.GetValues(typeof(DrinkType)))
{
    Console.WriteLine(drink);
}
Console.WriteLine("---Size of drinks---");

foreach (DrinkSize size in Enum.GetValues(typeof(DrinkSize)))
{
    Console.WriteLine(size);
}
Console.WriteLine("---Order's status---");

foreach (OrderStatus status in Enum.GetValues(typeof(OrderStatus)))
{
    Console.WriteLine(status);
}

Console.WriteLine(DrinkType.Coffee.ToString());
Console.WriteLine(DrinkSize.Large.ToString());



string drinkStr = "Tea";
DrinkType drink4 = (DrinkType)Enum.Parse(typeof(DrinkType), drinkStr);

string sizeStr = "Medium";
DrinkSize size2 = (DrinkSize)Enum.Parse(typeof(DrinkSize), sizeStr);

Console.WriteLine("-------\n------- ");
Console.WriteLine(drink4); 
Console.WriteLine(size2);

Console.WriteLine(order1.Price);
Console.WriteLine(order2.Price);
Console.WriteLine(order3.Price);
decimal finalPrice = 0;
foreach (DrinkOrder drink in drinks)
{
    
    finalPrice += drink.CalculatePrice();
}

Console.WriteLine(finalPrice);


