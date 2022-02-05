using System.Diagnostics;

uint userInput = 0;
uint userInputSqrt = Convert.ToUInt32(Math.Sqrt(userInput));
List<uint> PrimeNumbers = new List<uint>();
List<uint> NonPrimeNumbers = new List<uint>();
List<uint> SkipNumbers = new List<uint>();
Stopwatch sw = new Stopwatch();

    // this asks the user to enter a max value.
    // the value cannot be negative or higher than
    // the max value of uInt32.
try
{
    Console.Write("Enter a max value:");
    userInput = Convert.ToUInt32(Console.ReadLine());
}
catch
{
    Console.WriteLine("Please enter a valid number");
    Environment.Exit(0);
}
    // after the user wrote the max value, the program
    // starts a stopwatch to check for how long the
    // program executed for.
sw.Start();

    // here the program checks every number from
    // 1(x) to 'userInput'.
    // then it checks every number from 2(y) to x.
    // and it checks if dividing them would result in
    // a remainder. if the result does not have a
    // remainder add that number to NonPrime list.
    // else add that number to Prime list.
for (uint x = 1; x <= userInput; x++)
{
    bool Prime = true;

    if (!SkipNumbers.Contains(x))
    {
        for (uint y = 2; y < x; y++)
        {
            if (x % y == 0)
            {
                Prime = false;
                break;
            }  
        }

        if (Prime)
        {
            PrimeNumbers.Add(x);

    // Prime * Prime = Non Prime
    // SkipNumbers contains all squared Prime numbers.
    // numbers in SkipNumbers won't be checked(x).
            ulong xsqrt = x * x;
            if (xsqrt <= userInputSqrt)
            {
                SkipNumbers.Add(Convert.ToUInt32(xsqrt));
            }
        }
        else
        {
            NonPrimeNumbers.Add(x);
        }
    }
}

    // this prints all the Prime numbers
Console.WriteLine("Prime numbers:");
foreach (uint num in PrimeNumbers)
{
    Console.Write("{0}, ",num);
}

Console.WriteLine("\n");

    // adds SkipNumbers list to NonPrime list.
foreach (uint num in SkipNumbers)
{
    NonPrimeNumbers.Add(num);
}
    // this Sort() might hurt performance.
NonPrimeNumbers.Sort();

    // prints all Non prime numbers.
Console.WriteLine("NonPrime numbers:");
foreach (uint num in NonPrimeNumbers)
{
    Console.Write("{0}, ",num);
}

    // stops the stopwatch and prints for how
    // long the program executed for.
sw.Stop();
Console.WriteLine("\nElapsed time: " + sw.Elapsed);