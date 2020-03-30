#region File Description

//-----------------------------------------------------------------------------
// FlockingSample.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Flocking.Animals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;

#endregion Using Statements

namespace Flocking
{
    #region FlockingAIParameters

    public struct AIParameters
    {
        /// <summary>
        /// how far away the animals see each other
        /// </summary>
        public float DetectionDistance;

        /// <summary>
        /// seperate from animals inside this distance
        /// </summary>
        public float SeparationDistance;

        /// <summary>
        /// how much the animal tends to move in it's previous direction
        /// </summary>
        public float MoveInOldDirectionInfluence;

        /// <summary>
        /// how much the animal tends to move with animals in it's detection distance
        /// </summary>
        public float MoveInFlockDirectionInfluence;

        /// <summary>
        /// how much the animal tends to move randomly
        /// </summary>
        public float MoveInRandomDirectionInfluence;

        /// <summary>
        /// how quickly the animal can turn
        /// </summary>
        public float MaxTurnRadians;

        /// <summary>
        /// how much each nearby animal influences it's behavior
        /// </summary>
        public float PerMemberWeight;

        /// <summary>
        /// how much dangerous animals influence it's behavior
        /// </summary>
        public float PerDangerWeight;
    }

    #endregion FlockingAIParameters

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FlockingSample : Microsoft.Xna.Framework.Game
    {
        #region Constants

        // X location to start drawing the HUD from
        private const int hudLocX = 200;

        // Y location to start drawing the HUD from
        private const int hudLocY = 30;

        // Min value for the distance sliders
        private const float sliderMin = 0.0f;

        // Max value for the distance sliders
        private const float sliderMax = 100f;

        // Width of the slider button
        private const int sliderButtonWidth = 10;

        // Default value for the AI parameters
        private const float detectionDefault = 70.0f;

        private const float separationDefault = 50.0f;
        private const float moveInOldDirInfluenceDefault = 1.0f;
        private const float moveInFlockDirInfluenceDefault = 1.0f;
        private const float moveInRandomDirInfluenceDefault = 0.05f;
        private const float maxTurnRadiansDefault = 6.0f;
        private const float perMemberWeightDefault = 1.0f;
        private const float perDangerWeightDefault = 50.0f;

        #endregion Constants

        #region Fields

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private InputState inputState;
        private SpriteFont hudFont;

        // Do we need to update AI parameers this Update
        private bool aiParameterUpdate = false;

        private bool moveCat = false;

        private Texture2D bButton;
        private Texture2D xButton;
        private Texture2D yButton;
        private Texture2D onePixelWhite;

        private Texture2D birdTexture;
        private Texture2D catTexture;

        private Cat cat;
        private Flock flock;

        private AIParameters flockParams;

        // Definte the dimensions of the controls
        private Rectangle barDetectionDistance = new Rectangle(205, 45, 85, 40);

        private Rectangle barSeparationDistance = new Rectangle(205, 125, 85, 40);
        private Rectangle buttonResetDistance = new Rectangle(105, 205, 140, 40);
        private Rectangle buttonResetFlock = new Rectangle(105, 285, 140, 40);
        private Rectangle buttonToggleCat = new Rectangle(105, 365, 140, 40);

        private int selectionNum;

        #endregion Fields

        #region Initialization

        public FlockingSample()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            inputState = new InputState();

            flock = null;
            cat = null;

            flockParams = new AIParameters();
            ResetAIParams();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting
        /// to run. This is where it can query for any required services and load any
        /// non-graphic related content.  Calling base.Initialize will enumerate
        /// through any components and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Enable the gestures we care about. you must set EnabledGestures before
            // you can use any of the other gesture APIs.
            TouchPanel.EnabledGestures =
                GestureType.Tap |
                GestureType.FreeDrag;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            catTexture = Content.Load<Texture2D>("cat");
            birdTexture = Content.Load<Texture2D>("mouse");

            bButton = Content.Load<Texture2D>("xboxControllerButtonB");
            xButton = Content.Load<Texture2D>("xboxControllerButtonX");
            yButton = Content.Load<Texture2D>("xboxControllerButtonY");

            hudFont = Content.Load<SpriteFont>("HUDFont");

            onePixelWhite = new Texture2D(
                GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            // TODO onePixelWhite.SetData<Color>(new Color[] { Color.White });
        }

        #endregion Initialization

        #region Handle Input

        /// <summary>
        /// Handles input for quitting the game.
        /// </summary>
        private void HandleInput()
        {
            inputState.Update();

            // Check for exit.
            if (inputState.Exit)
            {
                Exit();
            }

            float dragDelta = 0f;

            // Check to see whether the user wants to modify their currently selected
            // weight.
            if (inputState.Up)
            {
                selectionNum--;
                if (selectionNum < 0)
                    selectionNum = 1;
            }

            if (inputState.Down)
            {
                selectionNum = (selectionNum + 1) % 2;
            }

            // Update move for the cat
            if (cat != null)
            {
                cat.HandleInput(inputState);
            }

            // Turn the cat on or off
            if (inputState.ToggleCatButton)
            {
                ToggleCat();
            }

            // Resets flock parameters back to default
            if (inputState.ResetDistances)
            {
                ResetAIParams();
                aiParameterUpdate = true;
            }

            // Resets the location and orientation of the members of the flock
            if (inputState.ResetFlock)
            {
                flock.ResetFlock();
                aiParameterUpdate = true;
            }

            dragDelta = inputState.SliderMove;

            // Apply to the changeAmount to the currentlySelectedWeight
            switch (selectionNum)
            {
                case 0:
                    flockParams.DetectionDistance += dragDelta;
                    break;

                case 1:
                    flockParams.SeparationDistance += dragDelta;
                    break;

                default:
                    break;
            }

            if (dragDelta != 0f)
                aiParameterUpdate = true;

            // By default we can move the cat but if a touch registers against a control do not move the cat
            moveCat = true;

            TouchCollection rawTouch = TouchPanel.GetState();

            // Use raw touch for the sliders
            if (rawTouch.Count > 0)
            {
                // Only grab the first one
                TouchLocation touchLocation = rawTouch[0];

                // Create a collidable rectangle to determine if we touched the controls
                Rectangle touchRectangle = new Rectangle((int)touchLocation.Position.X,
                                                         (int)touchLocation.Position.Y, 20, 20);

                // Have the sliders rely on the raw touch to function properly
                SliderInputHelper(touchRectangle);
            }

            // Next we handle all of the gestures. since we may have multiple gestures available,
            // we use a loop to read in all of the gestures. this is important to make sure the
            // TouchPanel's queue doesn't get backed up with old data
            while (TouchPanel.IsGestureAvailable)
            {
                // Read the next gesture from the queue
                GestureSample gesture = TouchPanel.ReadGesture();

                // Create a collidable rectangle to determine if we touched the controls
                Rectangle touch = new Rectangle((int)gesture.Position.X, (int)gesture.Position.Y, 20, 20);

                // We can use the type of gesture to determine our behavior
                switch (gesture.GestureType)
                {
                    case GestureType.Tap:

                        if (buttonResetDistance.Intersects(touch))
                        {
                            // Resets flock parameters back to default
                            ResetAIParams();
                            aiParameterUpdate = true;
                            moveCat = false;
                        }
                        else if (buttonResetFlock.Intersects(touch))
                        {
                            // Resets the location and orientation of the members of the flock
                            flock.ResetFlock();
                            aiParameterUpdate = true;
                            moveCat = false;
                        }
                        else if (buttonToggleCat.Intersects(touch))
                        {
                            ToggleCat();
                            moveCat = false;
                        }

                        break;
                }

                // Check if we can move the cat
                if (cat != null && moveCat)
                {
                    // If we did not touch any controls then move the cat
                    cat.Location = gesture.Position;
                }
            }

            // Clamp the slider values
            flockParams.DetectionDistance = MathHelper.Clamp(flockParams.DetectionDistance, sliderMin, sliderMax);
            flockParams.SeparationDistance = MathHelper.Clamp(flockParams.SeparationDistance, sliderMin, sliderMax);

            if (aiParameterUpdate)
            {
                flock.FlockParams = flockParams;
            }
        }

        /// <summary>
        /// Helper function that handles Slider interaction logic
        /// </summary>
        /// <param name="touchRectangle">Rectangle representing a touch</param>
        private void SliderInputHelper(Rectangle touchRectangle)
        {
            if (barDetectionDistance.Intersects(touchRectangle))
            {
                selectionNum = 0;
                aiParameterUpdate = true;
                moveCat = false;
                flockParams.DetectionDistance = touchRectangle.X - barDetectionDistance.X;
            }
            else if (barSeparationDistance.Intersects(touchRectangle))
            {
                selectionNum = 1;
                aiParameterUpdate = true;
                moveCat = false;
                flockParams.SeparationDistance = touchRectangle.X - barDetectionDistance.X;
            }
        }

        #endregion Handle Input

        #region Update and Draw

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            HandleInput();

            if (cat != null)
            {
                cat.Update(gameTime);
            }

            if (flock != null)
            {
                flock.Update(gameTime, cat);
            }
            else
            {
                SpawnFlock();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (flock != null)
            {
                flock.Draw(spriteBatch, gameTime);
            }

            if (cat != null)
            {
                cat.Draw(spriteBatch, gameTime);
            }

            // Draw all the HUD elements
            DrawBar(barDetectionDistance, flockParams.DetectionDistance / 100f,
                    "Detection Distance:", gameTime, selectionNum == 0);

            DrawBar(barSeparationDistance, flockParams.SeparationDistance / 100f,
                    "Separation  Distance:", gameTime, selectionNum == 1);

            spriteBatch.Draw(bButton,
                new Vector2(hudLocX + 110.0f, hudLocY), Color.White);
            spriteBatch.Draw(xButton,
                new Vector2(hudLocX + 110.0f, hudLocY + 20.0f), Color.White);
            spriteBatch.Draw(yButton,
                new Vector2(hudLocX + 110.0f, hudLocY + 40.0f), Color.White);

            spriteBatch.DrawString(hudFont, "Reset Distances",
                new Vector2(hudLocX + 135.0f, hudLocY), Color.White);
            spriteBatch.DrawString(hudFont, "Reset flock",
                new Vector2(hudLocX + 135.0f, hudLocY + 20.0f), Color.White);
            spriteBatch.DrawString(hudFont, "Spawn/remove cat",
                new Vector2(hudLocX + 135.0f, hudLocY + 40.0f), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Helper function used by Draw. It is used to draw the buttons
        /// </summary>
        /// <param name="button"></param>
        /// <param name="label"></param>
        private void DrawButton(Rectangle button, string label)
        {
            spriteBatch.Draw(onePixelWhite, button, Color.Orange);
            spriteBatch.DrawString(hudFont, label, new Vector2(button.Left + 10, button.Top + 10), Color.Black);
        }

        /// <summary>
        /// Helper function used by Draw. It is used to draw the slider bars
        /// </summary>
        private void DrawBar(Rectangle bar, float barWidthNormalized, string label, GameTime gameTime, bool highlighted)
        {
            Color tintColor = Color.White;

            // If the bar is highlighted, we want to make it pulse with a red tint.
            if (highlighted)
            {
                // To do this, we'll first generate a value t, which we'll use to
                // determine how much tint to have.
                float t = (float)Math.Sin(10 * gameTime.TotalGameTime.TotalSeconds);

                // Sin varies from -1 to 1, and we want t to go from 0 to 1, so we'll
                // scale it now.
                t = .5f + .5f * t;

                // Finally, we'll calculate our tint color by using Lerp to generate
                // a color in between Red and White.
                tintColor = new Color(Vector4.Lerp(
                    Color.Red.ToVector4(), Color.White.ToVector4(), t));
            }

            // Calculate how wide the bar should be, and then draw it.
            bar.Height /= 2;
            spriteBatch.Draw(onePixelWhite, bar, Color.White);

            // Draw the slider
            spriteBatch.Draw(onePixelWhite, new Rectangle(bar.X + (int)(bar.Width * barWidthNormalized),
                             bar.Y - bar.Height / 2, sliderButtonWidth, bar.Height * 2), Color.Orange);

            // Finally, draw the label to the left of the bar.
            Vector2 labelSize = hudFont.MeasureString(label);
            Vector2 labelPosition = new Vector2(bar.X - 5 - labelSize.X, bar.Y);
            spriteBatch.DrawString(hudFont, label, labelPosition, tintColor);
        }

        #endregion Update and Draw

        #region Methods

        /// <summary>
        /// Create the bird flock
        /// </summary>
        /// <param name="theNum"></param>
        protected void SpawnFlock()
        {
            if (flock == null)
            {
                flock = new Flock(birdTexture, GraphicsDevice.Viewport.TitleSafeArea.Width,
                                  GraphicsDevice.Viewport.TitleSafeArea.Height, flockParams);
            }
        }

        /// <summary>
        /// Reset flock AI parameters
        /// </summary>
        private void ResetAIParams()
        {
            flockParams.DetectionDistance = detectionDefault;
            flockParams.SeparationDistance = separationDefault;
            flockParams.MoveInOldDirectionInfluence = moveInOldDirInfluenceDefault;
            flockParams.MoveInFlockDirectionInfluence = moveInFlockDirInfluenceDefault;
            flockParams.MoveInRandomDirectionInfluence = moveInRandomDirInfluenceDefault;
            flockParams.MaxTurnRadians = maxTurnRadiansDefault;
            flockParams.PerMemberWeight = perMemberWeightDefault;
            flockParams.PerDangerWeight = perDangerWeightDefault;
        }

        /// <summary>
        /// Create or remove the cat
        /// </summary>
        protected void ToggleCat()
        {
            if (cat == null)
            {
                cat = new Cat(catTexture, GraphicsDevice.Viewport.TitleSafeArea.Width,
                              GraphicsDevice.Viewport.TitleSafeArea.Height);
            }
            else
            {
                cat = null;
            }
        }

        #endregion Methods
    }
}