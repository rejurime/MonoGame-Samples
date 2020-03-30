using System;

namespace LensFlare
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new LensFlareGame())
                game.Run();
        }
    }
}