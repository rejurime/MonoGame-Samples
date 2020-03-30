using System;

namespace Tile_Engine
{
    [Serializable]
    public class MapSquare
    {
        #region Declarations

        public int[] LayerTiles = new int[3];
        public string CodeValue = "";
        public bool Passable = true;

        #endregion Declarations

        #region Constructor

        public MapSquare(
            int background,
            int interactive,
            int foreground,
            string code,
            bool passable)
        {
            LayerTiles[0] = background;
            LayerTiles[1] = interactive;
            LayerTiles[2] = foreground;
            CodeValue = code;
            Passable = passable;
        }

        #endregion Constructor

        #region Public Methods

        public void TogglePassable()
        {
            Passable = !Passable;
        }

        #endregion Public Methods
    }
}