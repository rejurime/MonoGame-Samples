using System;

namespace GameStateManagement
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new GameStateManagementGame())
                game.Run();
        }
    }
}