List<object> forks = [];
bool stopped = false;

Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) =>
{
    e.Cancel = true;
    Console.WriteLine("Stopping...");
    stopped = true;
};

for (int i = 0; i < 5; ++i)
{
    forks.Add(new object());
}

List<Thread> threads = [];
for (int i = 0; i < forks.Count; ++i)
{
    threads.Add(StartPhilosopherThread(i));
}
Console.WriteLine("Simulation started");
foreach (var thread in threads)
{
    thread.Join();
}
Console.WriteLine("Simulation completed");

Thread StartPhilosopherThread(int index)
{
    Console.WriteLine($"Starting philosopher #{index}");
    var philosopher = Philosopher.Create(index, forks.Count);
    Thread thread = new(DoPhilosopherThread);
    thread.Start(philosopher);

    return thread;
}

void DoPhilosopherThread(object? obj)
{
    if (obj is Philosopher philosopher)
    {
        while (!stopped)
        {
            philosopher.Dinner(forks, TimeSpan.FromMilliseconds(300));
        }
    }
}

class Philosopher(string name, int firstFork, int secondFork)
{
    private readonly string name = name;
    private readonly int firstFork = firstFork;
    private readonly int secondFork = secondFork;

    public static Philosopher Create(int index, int forkCount)
    {
        int firstFork = index;
        int secondFork = (index + 1) % forkCount;
        if (index % 2 == 1)
        {
            (firstFork, secondFork) = (secondFork, firstFork);
        }
        return new Philosopher($"Philosopher #{index}", firstFork, secondFork);
    }

    public void Dinner(List<object> forks, TimeSpan duration)
    {
        lock (forks[firstFork])
        {
            lock (forks[secondFork])
            {
                Console.WriteLine($"{name} dinner started");
                Thread.Sleep(duration);
                Console.WriteLine($"{name} dinner ended");
            }
        }
    }

    public void Think(TimeSpan duration)
    {
        Console.WriteLine($"{name} thinking started");
        Thread.Sleep(duration);
        Console.WriteLine($"{name} thinking ended");
    }
}
