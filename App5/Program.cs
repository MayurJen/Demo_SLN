using System;

class App5
{
    static void Main()
    {
        string input = Console.ReadLine();
        double number = Convert.ToDouble(input);
        for (int i = 1; i < 10; i++)
        {
            double result = number * i;
            Console.WriteLine("{0} x {1,2} = {2,2}",number,i,result);
        }
    }
}