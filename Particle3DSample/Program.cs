using System;

namespace Particle3DSample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new Particle3DSampleGame())
                game.Run();
        }
    }
}