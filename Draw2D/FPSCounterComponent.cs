using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Draw2D
{
    public class FPSCounterComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        private int frameRate = 0;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        public FPSCounterComponent(Game game, SpriteBatch Batch, SpriteFont Font)
            : base(game)
        {
            spriteFont = Font;
            spriteBatch = Batch;
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            frameCounter++;

            string fps = string.Format("fps: {0} mem : {1}", frameRate, GC.GetTotalMemory(false));

            spriteBatch.DrawString(spriteFont, fps, new Vector2(1, 1), Color.Black);
            spriteBatch.DrawString(spriteFont, fps, new Vector2(0, 0), Color.White);
        }
    }
}