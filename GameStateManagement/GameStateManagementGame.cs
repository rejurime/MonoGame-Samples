#region File Description

//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using GameStateManagement.Screens;
using Microsoft.Xna.Framework;

#endregion Using Statements

namespace GameStateManagement
{
    /// <summary>
    /// Sample showing how to manage different game states, with transitions
    /// between menu screens, a loading screen, the game itself, and a pause
    /// menu. This main game class is extremely simple: all the interesting
    /// stuff happens in the ScreenManager component.
    /// </summary>
    public class GameStateManagementGame : Game
    {
        #region Fields

        private GraphicsDeviceManager graphics;
        private ScreenManager.ScreenManager screenManager;

        private int BufferWidth = 272;
        private int BufferHeight = 480;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// The main game constructor.
        /// </summary>
        public GameStateManagementGame()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = BufferWidth;
            graphics.PreferredBackBufferHeight = BufferHeight;

            // Create the screen manager component.
            screenManager = new ScreenManager.ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        #endregion Initialization

        #region Draw

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }

        #endregion Draw
    }
}