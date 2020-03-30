using System;

namespace XNA2DShaderExamples
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new ShaderTest())
                game.Run();
        }
    }
}