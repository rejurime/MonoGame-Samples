#region File Description

//-----------------------------------------------------------------------------
// Game1.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateObjectWindows
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        private BasicEffect basicEffect;

        private VertexDeclaration vertexDeclaration;
        private VertexBuffer vertexBuffer;
        private const int number_of_vertices = 6;

        private RasterizerState rsCullNone;

        private bool changeState = false;

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void CreateEffect()
        {
            basicEffect = new BasicEffect(GraphicsDevice);
        }

        private void CreateVertexBuffer()
        {
            vertexDeclaration = new VertexDeclaration(new VertexElement[1]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0)
                }
            );

            vertexBuffer = new VertexBuffer(
                GraphicsDevice,
                vertexDeclaration,
                number_of_vertices,
                BufferUsage.None
                );

            Vector3[] vertices = new Vector3[number_of_vertices];
            vertices[0] = new Vector3(-1, 0, 0); // cw
            vertices[1] = new Vector3(0, 1, 0);
            vertices[2] = new Vector3(0, 0, 0);
            vertices[3] = new Vector3(0, 0, 0); // ccw
            vertices[4] = new Vector3(1, 0, 0);
            vertices[5] = new Vector3(0, 1, 0);

            vertexBuffer.SetData(vertices);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            EffectPass pass = basicEffect.CurrentTechnique.Passes[0];
            if (pass != null)
            {
                pass.Apply();

                GraphicsDevice.DrawPrimitives(
                    PrimitiveType.TriangleList, // primitive type to draw
                    0, // start vertex
                    2 // number of primitives to draw
                );
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            CreateEffect();
            CreateVertexBuffer();

            rsCullNone = new RasterizerState();
            rsCullNone.CullMode = CullMode.None;
            rsCullNone.FillMode = FillMode.WireFrame;
            rsCullNone.MultiSampleAntiAlias = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private KeyboardState current = Keyboard.GetState();
        private KeyboardState last = Keyboard.GetState();
        private bool keyboardChangeState = false;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            last = current;
            current = Keyboard.GetState();

            // Exit the game from a GamePad
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            // Exit the game from a Keyboard
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                changeState = true;

            if (current.IsKeyDown(Keys.A) && last.IsKeyUp(Keys.A))
                changeState = true;

            if ((changeState) && (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released
                || (current.IsKeyDown(Keys.A) && last.IsKeyUp(Keys.A))))
            {
                if (GraphicsDevice.RasterizerState.CullMode == CullMode.None)
                    GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
                else if (GraphicsDevice.RasterizerState.CullMode == CullMode.CullCounterClockwiseFace)
                    GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
                else if (GraphicsDevice.RasterizerState.CullMode == CullMode.CullClockwiseFace)
                    GraphicsDevice.RasterizerState = rsCullNone;

                changeState = false;
            }
            base.Update(gameTime);
        }
    }
}