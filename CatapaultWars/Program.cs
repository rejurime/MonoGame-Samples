using System;

namespace CatapaultGame
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new CatapultGame())
                game.Run();
        }
    }
}