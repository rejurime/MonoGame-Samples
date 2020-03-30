using System;

namespace CollisionSample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new CollisionSample())
                game.Run();
        }
    }
}