using System;

namespace Graphics3DSample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new Graphics3DSampleGame())
                game.Run();
        }
    }
}