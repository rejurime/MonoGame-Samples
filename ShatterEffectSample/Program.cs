using System;

namespace ShatterEffectSample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new ShatterEffectGame())
                game.Run();
        }
    }
}