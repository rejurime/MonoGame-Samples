using System;

namespace FarseerSamples
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new FarseerPhysicsGame())
                game.Run();
        }
    }
}