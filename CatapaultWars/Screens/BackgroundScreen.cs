#region File Description

//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using CatapaultGame.ScreenManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#endregion Using Statements

namespace CatapaultGame.Screens
{
    internal class BackgroundScreen : GameScreen
    {
        #region Fields

        private Texture2D background;

        #endregion Fields

        #region Initialization

        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion Initialization

        #region Loading

        public override void LoadContent()
        {
            background = Load<Texture2D>("Textures/Backgrounds/title_screen");
        }

        #endregion Loading

        #region Render

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            // Draw Background
            spriteBatch.Draw(background, new Vector2(0, 0),
                 new Color(255, 255, 255, TransitionAlpha));

            spriteBatch.End();
        }

        #endregion Render
    }
}