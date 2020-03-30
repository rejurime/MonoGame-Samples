using System;

namespace ReachGraphicsDemo
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new DemoGame())
                game.Run();
        }
    }
}