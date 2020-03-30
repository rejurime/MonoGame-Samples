using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace XNA2DShaderExamples
{
    public class ShaderTest : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private KeyboardState PreviousKeyState;
        private KeyboardState CurrentKeyState;

        private Texture2D background;
        private Texture2D surge;

        private List<Effect> shaderEffects;
        private int shaderEffectIdx;

        public ShaderTest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            shaderEffectIdx = 0;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = Content.Load<Texture2D>(@"images\bg5");
            surge = Content.Load<Texture2D>(@"images\surge");

            shaderEffects = new List<Effect>();
            shaderEffects.Add(Content.Load<Effect>(@"effects\NoEffect"));
            shaderEffects.Add(Content.Load<Effect>(@"effects\HighContrast"));
            shaderEffects.Add(Content.Load<Effect>(@"effects\Bevels"));
            shaderEffects.Add(Content.Load<Effect>(@"effects\Grayscale"));
            shaderEffects.Add(Content.Load<Effect>(@"effects\ColorFlip"));
            shaderEffects.Add(Content.Load<Effect>(@"effects\Invert"));
            shaderEffects.Add(Content.Load<Effect>(@"effects\BlackOut"));
            shaderEffects.Add(Content.Load<Effect>(@"effects\RainbowH"));
        }

        protected override void Update(GameTime gameTime)
        {
            PreviousKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();

            if (CurrentKeyState.IsKeyDown(Keys.Escape)) this.Exit();

            if (CurrentKeyState.IsKeyDown(Keys.Up) && PreviousKeyState.IsKeyUp(Keys.Up))
            {
                shaderEffectIdx++;
                if (shaderEffectIdx >= shaderEffects.Count()) shaderEffectIdx = 0;
            }

            if (CurrentKeyState.IsKeyDown(Keys.Down) && PreviousKeyState.IsKeyUp(Keys.Down))
            {
                shaderEffectIdx--;
                if (shaderEffectIdx < 0) shaderEffectIdx = shaderEffects.Count() - 1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Draw(background, new Vector2(-200, -200), Color.White);
            shaderEffects[shaderEffectIdx].CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(surge, new Vector2(300, 200), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}