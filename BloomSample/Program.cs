using System;

namespace BloomSample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new BloomPostprocessGame())
                game.Run();
        }
    }
}