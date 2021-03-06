#region File Description

//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using CatapaultGame.ScreenManager;
using Microsoft.Xna.Framework;
using System;

#endregion Using Statements

namespace CatapaultGame.Screens
{
    internal class MainMenuScreen : MenuScreen
    {
        #region Initialization

        public MainMenuScreen()
            : base(String.Empty)
        {
            IsPopup = true;

            // Create our menu entries.
            MenuEntry startGameMenuEntry = new MenuEntry("Play");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            startGameMenuEntry.Selected += StartGameMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(startGameMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        #endregion Initialization

        #region Overrides

        protected override void UpdateMenuEntryLocations()
        {
            base.UpdateMenuEntryLocations();

            foreach (var entry in MenuEntries)
            {
                Vector2 position = entry.Position;

                position.Y += 60;

                entry.Position = position;
            }
        }

        #endregion Overrides

        #region Event Handlers for Menu Items

        /// <summary>
        /// Handles "Play" menu item selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGameMenuEntrySelected(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new InstructionsScreen(), null);
        }

        /// <summary>
        /// Handles "Exit" menu item selection
        /// </summary>
        ///
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            ScreenManager.Game.Exit();
        }

        #endregion Event Handlers for Menu Items
    }
}