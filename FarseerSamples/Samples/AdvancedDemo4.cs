﻿using FarseerSamples.Samples.Prefabs;
using FarseerSamples.ScreenSystem;
using Microsoft.Xna.Framework;
using System.Text;

namespace FarseerSamples.Samples
{
    internal class AdvancedDemo4 : PhysicsGameScreen, IDemoScreen
    {
        private Border _border;
        private Spiderweb _spiderweb;

        #region IDemoScreen Members

        public string GetTitle()
        {
            return "Advanced dynamics";
        }

        public string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TODO: Add sample description!");
            sb.AppendLine(string.Empty);
            sb.AppendLine("GamePad:");
            sb.AppendLine("  - Move cursor: left thumbstick");
            sb.AppendLine("  - Grab object (beneath cursor): A button");
            sb.AppendLine("  - Drag grabbed object: left thumbstick");
            sb.AppendLine("  - Exit to menu: Back button");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Keyboard:");
            sb.AppendLine("  - Exit to menu: Escape");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Mouse / Touchscreen");
            sb.AppendLine("  - Grab object (beneath cursor): Left click");
            sb.AppendLine("  - Drag grabbed object: move mouse / finger");
            return sb.ToString();
        }

        #endregion IDemoScreen Members

        public override void LoadContent()
        {
            base.LoadContent();

            World.Gravity = new Vector2(0, 9.82f);

            _border = new Border(World, this, ScreenManager.GraphicsDevice.Viewport);

            _spiderweb = new Spiderweb(World, Vector2.Zero, ConvertUnits.ToSimUnits(12), 5, 12);

            _spiderweb.LoadContent(ScreenManager.Content);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin(0, null, null, null, null, null, Camera.View);
            _spiderweb.Draw(ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.End();
            _border.Draw();
            base.Draw(gameTime);
        }
    }
}