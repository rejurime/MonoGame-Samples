using System;

namespace Aiming
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new AimingGame())
                game.Run();
        }
    }
}