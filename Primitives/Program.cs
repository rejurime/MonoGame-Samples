using System;

namespace Primitives
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new PrimitivesSampleGame())
                game.Run();
        }
    }
}