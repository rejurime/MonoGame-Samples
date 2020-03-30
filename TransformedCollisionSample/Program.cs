using System;

namespace TransformedCollisionSample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new TransformedCollisionGame())
                game.Run();
        }
    }
}