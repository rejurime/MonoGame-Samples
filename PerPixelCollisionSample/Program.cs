using System;

namespace PerPixelCollisionSample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new PerPixelCollisionGame())
                game.Run();
        }
    }
}