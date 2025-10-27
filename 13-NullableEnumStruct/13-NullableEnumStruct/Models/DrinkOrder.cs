using _13_NullableEnumStruct.Enums;

namespace _13_NullableEnumStruct.Models;

public class DrinkOrder
{
    public int OrderNumber { get; set; }
    public string CustomerName  { get; set; }
    public DrinkType Drink { get; set; }
    public DrinkSize Size { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.New;
    public decimal Price { get; set; }

    public DrinkOrder(int orderNumber, string customerName,DrinkType drink,DrinkSize size)
    {
        OrderNumber = orderNumber;
        CustomerName = customerName;
        Drink = drink;
        Size = size;
        Price = CalculatePrice();
        
    }

    #region CalculatePrice

    public decimal CalculatePrice()
    {
        decimal price = 0;

        switch (Drink)
        {
            case DrinkType.Coffee:
                switch (Size)
                {
                    case DrinkSize.Small: price = 3; break;
                    case DrinkSize.Medium: price = 4; break;
                    case DrinkSize.Large: price = 5; break;
                }
                break;

            case DrinkType.Tea:
                switch (Size)
                {
                    case DrinkSize.Small: price = 2; break;
                    case DrinkSize.Medium: price = 3; break;
                    case DrinkSize.Large: price = 4; break;
                }
                break;

            case DrinkType.Juice:
                switch (Size)
                {
                    case DrinkSize.Small: price = 4; break;
                    case DrinkSize.Medium: price = 5; break;
                    case DrinkSize.Large: price = 6; break;
                }
                break;

            case DrinkType.Water:
                switch (Size)
                {
                    case DrinkSize.Small: price = 1; break;
                    case DrinkSize.Medium: price = 1.5m; break;
                    case DrinkSize.Large: price = 2; break;
                }
                break;
        }

        Price = price;
        return Price;
    }

    #endregion

    #region UpdateStatus

    public void UpdateStatus(OrderStatus newStatus)
    {
        Status = newStatus;
        Console.WriteLine($"Sifariş no : {OrderNumber} status: {newStatus}");
    }
    
    #endregion

    #region DisplayOrder

    public void DisplayOrder()
    {
        Console.WriteLine($"----Order data----:");
        Console.WriteLine($"No: {OrderNumber}");
        Console.WriteLine($"Customer: {CustomerName}");
        Console.WriteLine($"Drink: {Drink}");
        Console.WriteLine($"Size: {Size}");
        Console.WriteLine($"Status: {Status}");
        Console.WriteLine($"Order price: {Price} AZN");
    }
    
    #endregion
}