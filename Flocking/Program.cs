﻿using System;

namespace Flocking
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new FlockingSample())
                game.Run();
        }
    }
}