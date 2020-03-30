using System;

namespace PerformanceMeasuring
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new PerformanceMeasuringGame())
                game.Run();
        }
    }
}