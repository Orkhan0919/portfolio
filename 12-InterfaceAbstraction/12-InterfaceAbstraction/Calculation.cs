namespace _12_InterfaceAbstraction;

public class Calculation : ICalculation
{
    #region Sum
    
    public double Sum()
    {
        double result = 0;
        bool add = true;

        while (add)
        {
            Console.WriteLine("Enter the number :");
            string input = Console.ReadLine();
            input = input.Replace(',', '.');
            double b;

            if (double.TryParse(input, out b))
            {
                result += b;
            }
            else
            {
                Console.WriteLine("---Invalid input---");
                continue;
            }

            Console.WriteLine("1-Add more number   2-End process");
            int a = int.Parse(Console.ReadLine());

            if (a == 1)
                add = true;
            else if (a == 2)
                add = false;
            else
            {
                Console.WriteLine("---Invalid input---");
                add = false; 
            }
        }

        return result;
    }

    #endregion

    #region Substraction

    public double Substraction()
    {
        Console.WriteLine("Enter the first number:");
        double result;
        while (!double.TryParse(Console.ReadLine().Replace(',', '.'), out result))
        {
            Console.WriteLine("---Invalid input---");
        }

        bool subtract = true;

        while (subtract)
        {
            Console.WriteLine("Enter the number to subtract:");
            double b;
            while (!double.TryParse(Console.ReadLine().Replace(',', '.'), out b))
            {
                Console.WriteLine("---Invalid input---");
            }

            result -= b;

            Console.WriteLine("1-Subtract another number   2-End process");
            int a= int.Parse(Console.ReadLine());
            

            if (a == 1)
                subtract = true;
            else if (a == 2)
                subtract = false;
            else
            {
                Console.WriteLine("-----Invalid input-----");
                subtract = false;
            }
        }

        return result;
    }

    #endregion

    #region Division

    public double Division()
    {
        Console.WriteLine("Enter the first number:");
        double result;
        while (!double.TryParse(Console.ReadLine().Replace(',', '.'), out result))
        {
            Console.WriteLine("---Invalid input---");
        }

        bool divide = true;

        while (divide)
        {
            Console.WriteLine("Enter the number to divide:");
            double b;
            while (!double.TryParse(Console.ReadLine().Replace(',', '.'), out b))
            {
                Console.WriteLine("---Invalid input---");
            }

            if (b == 0)
            {
                Console.WriteLine("Can't divide by zero!");
                continue; 
            }

            result /= b;

            Console.WriteLine("1-Divide another number   2-End process");
            int a= int.Parse(Console.ReadLine());
            

            if (a == 1)
                divide = true;
            else if (a == 2)
                divide = false;
            else
            {
                Console.WriteLine("-----Invalid input-----");
                divide = false;
            }
        }

        return result;
    }

    #endregion

    #region Multiplication
    
    public double Multiplication()
    {
        Console.WriteLine("Enter the first number:");
        double result;
        while (!double.TryParse(Console.ReadLine().Replace(',', '.'), out result))
        {
            Console.WriteLine("---Invalid input---");
        }

        bool multiply = true;

        while (multiply)
        {
            Console.WriteLine("Enter the number to multiply:");
            double b=int.Parse(Console.ReadLine());
            

            result *= b;

            Console.WriteLine("1-Multiply another number   2-End process");
            int a=int.Parse(Console.ReadLine());
            

            if (a == 1)
                multiply = true;
            else if (a == 2)
                multiply = false;
            else
            {
                Console.WriteLine("-----Invalid input-----");
                multiply = false;
            }
        }

        return result;
    }

    #endregion

}