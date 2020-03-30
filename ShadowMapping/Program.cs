using System;

namespace ShadowMapping
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new ShadowMappingGame())
                game.Run();
        }
    }
}