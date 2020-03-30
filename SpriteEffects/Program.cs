using System;

namespace SpriteEffects
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new SpriteEffectsGame())
                game.Run();
        }
    }
}