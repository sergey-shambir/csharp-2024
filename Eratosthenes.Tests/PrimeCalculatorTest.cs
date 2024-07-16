namespace Eratosthenes.Tests;

public class PrimeCalculatorTest
{
    [Fact]
    public void TestPrimeNumbersUpTo100()
    {
        Assert.Equal(Array.Empty<uint>(), PrimeCalculator.ListPrimeNumbers(1));
        Assert.Equal(new uint[] { 2 }, PrimeCalculator.ListPrimeNumbers(2));
        Assert.Equal(new uint[] { 2, 3, 5 }, PrimeCalculator.ListPrimeNumbers(5));
        Assert.Equal(new uint[] { 2, 3, 5 }, PrimeCalculator.ListPrimeNumbers(6));
        Assert.Equal(new uint[] { 2, 3, 5, 7 }, PrimeCalculator.ListPrimeNumbers(7));
        Assert.Equal(new uint[] { 2, 3, 5, 7 }, PrimeCalculator.ListPrimeNumbers(10));

        Assert.Equal(
            new uint[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 },
            PrimeCalculator.ListPrimeNumbers(100)
        );
    }
}
