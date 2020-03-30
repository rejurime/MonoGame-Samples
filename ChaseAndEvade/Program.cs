using System;

namespace ChaseAndEvade
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new ChaseAndEvadeGame())
                game.Run();
        }
    }
}