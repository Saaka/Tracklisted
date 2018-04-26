using System;
using System.Threading;

namespace Tracklisted.Commands.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan sleepTime = TimeSpan.FromSeconds(10);
            int counter = 1;
            while (true)
            {
                Console.WriteLine($"{DateTime.UtcNow.ToString()}: Message #{counter}");

                Thread.Sleep(sleepTime);
                counter++;
            }
        }
    }
}
