using System;

namespace XNAPacMan
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new XNAPacMan())
                game.Run();
        }
    }
}