using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace StencilsCratersTutorial
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private const int PlanetDataSize = 256;

        private bool firstTime = true;
        private Vector2 craterPosition;
        private Vector2 planetPosition;
        private Texture2D drawingTexture;
        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Random random;

        private Texture2D planetTexture;
        private Texture2D craterTexture;

        private RenderTarget2D renderTargetA;
        private RenderTarget2D renderTargetB;
        private RenderTarget2D activeRenderTarget;
        private RenderTarget2D textureRenderTarget;

        private AlphaTestEffect alphaTestEffect;
        private DepthStencilState stencilAlways;
        private DepthStencilState stencilKeepIfZero;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            random = new Random();
        }

        protected override void Initialize()
        {
            base.Initialize();

            // initialize keyboard state
            keyboardState = Keyboard.GetState();
            previousKeyboardState = keyboardState;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load assets
            planetTexture = Content.Load<Texture2D>(@"planet");
            craterTexture = Content.Load<Texture2D>(@"crater");

            // set up alpha test effect
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, PlanetDataSize, PlanetDataSize, 0, 0, 1);
            Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);

            alphaTestEffect = new AlphaTestEffect(GraphicsDevice);
            alphaTestEffect.VertexColorEnabled = true;
            alphaTestEffect.DiffuseColor = Color.White.ToVector3();
            alphaTestEffect.AlphaFunction = CompareFunction.Equal;
            alphaTestEffect.ReferenceAlpha = 0;
            alphaTestEffect.World = Matrix.Identity;
            alphaTestEffect.View = Matrix.Identity;
            alphaTestEffect.Projection = halfPixelOffset * projection;

            // set up stencil state to always replace stencil buffer with 1
            stencilAlways = new DepthStencilState();
            stencilAlways.StencilEnable = true;
            stencilAlways.StencilFunction = CompareFunction.Always;
            stencilAlways.StencilPass = StencilOperation.Replace;
            stencilAlways.ReferenceStencil = 1;
            stencilAlways.DepthBufferEnable = false;

            // set up stencil state to pass if the stencil value is 0
            stencilKeepIfZero = new DepthStencilState();
            stencilKeepIfZero.StencilEnable = true;
            stencilKeepIfZero.StencilFunction = CompareFunction.Equal;
            stencilKeepIfZero.StencilPass = StencilOperation.Keep;
            stencilKeepIfZero.ReferenceStencil = 0;
            stencilKeepIfZero.DepthBufferEnable = false;

            // create render targets
            renderTargetA = new RenderTarget2D(GraphicsDevice, PlanetDataSize, PlanetDataSize,
             false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8,
             0, RenderTargetUsage.DiscardContents);

            renderTargetB = new RenderTarget2D(GraphicsDevice, PlanetDataSize, PlanetDataSize,
                                               false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8,
                                               0, RenderTargetUsage.DiscardContents);

            // set up the ping-pong texture pointers
            activeRenderTarget = renderTargetA;
            textureRenderTarget = renderTargetB;
            drawingTexture = planetTexture;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // check for exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // update keyboard state
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            // calculate planet position, centered in screen
            planetPosition = new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth * 0.5f - PlanetDataSize * 0.4f,
GraphicsDevice.PresentationParameters.BackBufferHeight * 0.5f - PlanetDataSize * 0.4f);

            // add a random crater if space bar is pressed
            if ((keyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space)))
            {
                craterPosition = new Vector2(random.Next(PlanetDataSize), random.Next(PlanetDataSize));
            }

            base.Update(gameTime);
        }

        public void AddCrater(Vector2 position)
        {
            // set up rendering to the active render target
            GraphicsDevice.SetRenderTarget(activeRenderTarget);

            // clear the render target to opaque black,
            // and initialize the stencil buffer with all zeroes
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.Stencil,
new Color(0, 0, 0, 1), 0, 0);

            // draw the new craters into the stencil buffer
            // stencilAlways makes sure we'll always write a 1
            // to the stencil buffer wherever we draw the alphaTestEffect
            // is set up to only write a pixel if the alpha value
            // of the source texture is zero
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque,
null, stencilAlways, null, alphaTestEffect);

            Vector2 origin = new Vector2(craterTexture.Width * 0.5f,
                                         craterTexture.Height * 0.5f);

            float rotation = (float)random.NextDouble() * MathHelper.TwoPi;
            Rectangle r = new Rectangle((int)position.X, (int)position.Y, 50, 50);

            spriteBatch.Draw(craterTexture, r, null, Color.White, rotation,
                             origin, SpriteEffects.None, 0);

            spriteBatch.End();

            // now draw the latest planet texture, excluding the stencil
            // buffer, resulting in the new craters being excluded from
            // the drawing the first time through we don't have a latest
            // planet texture, so draw from the original texture
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque,
null, stencilKeepIfZero, null, null);

            if (firstTime)
            {
                spriteBatch.Draw(planetTexture, Vector2.Zero, Color.White);
                firstTime = false;
            }
            else
                spriteBatch.Draw(textureRenderTarget, Vector2.Zero, Color.White);

            spriteBatch.End();

            // restore main render target - this lets us get at the render target we just drew and use it as a texture
            GraphicsDevice.SetRenderTarget(null);

            //// save image for testing
            //using (FileStream f = new FileStream("planet.png", FileMode.Create))
            //{
            //  activeRenderTarget.SaveAsPng(f, 256, 256);
            //}

            // swap render targets, so the next time our source texture is the render target we just drew,
            // and the one we'll be drawing on is the one we just used as our source texture this time
            RenderTarget2D t = activeRenderTarget;
            activeRenderTarget = textureRenderTarget;
            textureRenderTarget = t;

            drawingTexture = textureRenderTarget;
        }

        protected override void Draw(GameTime gameTime)
        {
            // we have to draw render targets first
            if (craterPosition != Vector2.Zero)
            {
                AddCrater(craterPosition);
                craterPosition = Vector2.Zero;
            }

            // draw the cratered planet texture
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(drawingTexture, planetPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}