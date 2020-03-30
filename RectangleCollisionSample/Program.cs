using System;

namespace RectangleCollisionSample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new RectangleCollisionGame())
                game.Run();
        }
    }
}