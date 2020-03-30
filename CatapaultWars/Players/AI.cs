#region File Description

//-----------------------------------------------------------------------------
// AI.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region File Information

//-----------------------------------------------------------------------------
// AI.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Information

#region Using Statements

using CatapaultGame.Catapult;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#endregion Using Statements

namespace CatapaultGame.Players
{
    internal class AI : Player
    {
        #region Fields

        private Random random;

        #endregion Fields

        #region Initialization

        public AI(Game game)
            : base(game)
        {
        }

        public AI(Game game, SpriteBatch screenSpriteBatch)
            : base(game, screenSpriteBatch)
        {
            Catapult = new Catapult.Catapult(game, screenSpriteBatch,
                            "Textures/Catapults/Red/redIdle/redIdle",
                            new Vector2(600, 332), SpriteEffects.FlipHorizontally, true);
        }

        public override void Initialize()
        {
            //Initialize randomizer
            random = new Random();

            Catapult.Initialize();

            base.Initialize();
        }

        #endregion Initialization

        #region Update

        public override void Update(GameTime gameTime)
        {
            // Check if it is time to take a shot
            if (Catapult.CurrentState == CatapultState.Aiming &&
                !Catapult.AnimationRunning)
            {
                // Fire at a random strength
                float shotVelocity =
                    random.Next((int)MinShotStrength, (int)MaxShotStrength);

                Catapult.ShotStrength = (shotVelocity / MaxShotStrength);
                Catapult.ShotVelocity = shotVelocity;
            }
            base.Update(gameTime);
        }

        #endregion Update
    }
}