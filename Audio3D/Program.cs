using System;

namespace Audio3D
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new Audio3DGame())
                game.Run();
        }
    }
}