namespace Eratosthenes;

using System.Collections.Generic;
using System.Collections;

public static class PrimeCalculator
{
    public static List<uint> ListPrimeNumbers(uint maxNumber)
    {
        var results = new List<uint>();
        if (maxNumber < 1)
        {
            return results;
        }

        BitArray sieve = new((int)maxNumber, true);
        int maxNumberRoot = (int)Math.Sqrt(maxNumber);
        for (int number = 2; number <= maxNumberRoot; ++number)
        {
            int index = number - 1;
            if (sieve[index])
            {
                results.Add((uint)number);
            }
            for (; index < maxNumber; index += number)
            {
                sieve[index] = false;
            }
        }
        for (int index = maxNumberRoot; index < maxNumber; ++index)
        {
            if (sieve[index])
            {
                results.Add((uint)(index + 1));
            }
        }

        return results;
    }
}
