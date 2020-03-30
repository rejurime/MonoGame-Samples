using System;

namespace ParticleSample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new ParticleSampleGame())
                game.Run();
        }
    }
}