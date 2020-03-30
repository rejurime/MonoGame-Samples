#region File Description

//-----------------------------------------------------------------------------
// CatapultGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using CatapaultGame.Screens;
using CatapaultGame.Utility;
using Microsoft.Xna.Framework;
using System;

#endregion Using Statements

namespace CatapaultGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CatapultGame : Game
    {
        #region Fields

        private GraphicsDeviceManager graphics;
        private ScreenManager.ScreenManager screenManager;

        #endregion Fields

        #region Initialization Methods

        public CatapultGame()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            //Create a new instance of the Screen Manager
            screenManager = new ScreenManager.ScreenManager(this);
            Components.Add(screenManager);
            IsMouseVisible = true;

            //Add two new screens
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);

            AudioManager.Initialize(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        #endregion Initialization Methods

        #region Loading

        protected override void LoadContent()
        {
            AudioManager.LoadSounds();
            base.LoadContent();
        }

        #endregion Loading
    }
}