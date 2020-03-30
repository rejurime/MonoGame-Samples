using System;

namespace Waypoint
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new WaypointSample())
                game.Run();
        }
    }
}