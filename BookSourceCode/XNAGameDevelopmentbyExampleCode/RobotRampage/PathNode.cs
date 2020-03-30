﻿using Microsoft.Xna.Framework;

namespace Robot_Rampage
{
    internal class PathNode
    {
        #region Declarations

        public PathNode ParentNode;
        public PathNode EndNode;
        private Vector2 gridLocation;
        public float TotalCost;
        public float DirectCost;

        #endregion Declarations

        #region Properties

        public Vector2 GridLocation
        {
            get { return gridLocation; }
            set
            {
                gridLocation = new Vector2(
                    (float)MathHelper.Clamp(value.X, 0f, (float)TileMap.MapWidth),
                    (float)MathHelper.Clamp(value.Y, 0f, (float)TileMap.MapHeight));
            }
        }

        public int GridX
        {
            get { return (int)gridLocation.X; }
        }

        public int GridY
        {
            get { return (int)gridLocation.Y; }
        }

        #endregion Properties

        #region Constructor

        public PathNode(
            PathNode parentNode,
            PathNode endNode,
            Vector2 gridLocation,
            float cost)
        {
            ParentNode = parentNode;
            GridLocation = gridLocation;
            EndNode = endNode;
            DirectCost = cost;
            if (!(endNode == null))
            {
                TotalCost = DirectCost + LinearCost();
            }
        }

        #endregion Constructor

        #region Helper Methods

        public float LinearCost()
        {
            return (
                Vector2.Distance(
                EndNode.GridLocation,
                this.GridLocation));
        }

        #endregion Helper Methods

        #region Public Methods

        public bool IsEqualToNode(PathNode node)
        {
            return (GridLocation == node.GridLocation);
        }

        #endregion Public Methods
    }
}