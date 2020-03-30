#region File Description

//-----------------------------------------------------------------------------
// Projectile.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Microsoft.Xna.Framework;
using System;

#endregion Using Statements

namespace Particle3DSample
{
    /// <summary>
    /// This class demonstrates how to combine several different particle systems
    /// to build up a more sophisticated composite effect. It implements a rocket
    /// projectile, which arcs up into the sky using a ParticleEmitter to leave a
    /// steady stream of trail particles behind it. After a while it explodes,
    /// creating a sudden burst of explosion and smoke particles.
    /// </summary>
    internal class Projectile
    {
        #region Constants

        private const float trailParticlesPerSecond = 200;
        private const int numExplosionParticles = 30;
        private const int numExplosionSmokeParticles = 50;
        private const float projectileLifespan = 1.5f;
        private const float sidewaysVelocityRange = 60;
        private const float verticalVelocityRange = 40;
        private const float gravity = 15;

        #endregion Constants

        #region Fields

        private ParticleSystem explosionParticles;
        private ParticleSystem explosionSmokeParticles;
        private ParticleEmitter trailEmitter;

        private Vector3 position;
        private Vector3 velocity;
        private float age;

        private static Random random = new Random();

        #endregion Fields

        /// <summary>
        /// Constructs a new projectile.
        /// </summary>
        public Projectile(ParticleSystem explosionParticles,
                          ParticleSystem explosionSmokeParticles,
                          ParticleSystem projectileTrailParticles)
        {
            this.explosionParticles = explosionParticles;
            this.explosionSmokeParticles = explosionSmokeParticles;

            // Start at the origin, firing in a random (but roughly upward) direction.
            position = Vector3.Zero;

            velocity.X = (float)(random.NextDouble() - 0.5) * sidewaysVelocityRange;
            velocity.Y = (float)(random.NextDouble() + 0.5) * verticalVelocityRange;
            velocity.Z = (float)(random.NextDouble() - 0.5) * sidewaysVelocityRange;

            // Use the particle emitter helper to output our trail particles.
            trailEmitter = new ParticleEmitter(projectileTrailParticles,
                                               trailParticlesPerSecond, position);
        }

        /// <summary>
        /// Updates the projectile.
        /// </summary>
        public bool Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Simple projectile physics.
            position += velocity * elapsedTime;
            velocity.Y -= elapsedTime * gravity;
            age += elapsedTime;

            // Update the particle emitter, which will create our particle trail.
            trailEmitter.Update(gameTime, position);

            // If enough time has passed, explode! Note how we pass our velocity
            // in to the AddParticle method: this lets the explosion be influenced
            // by the speed and direction of the projectile which created it.
            if (age > projectileLifespan)
            {
                for (int i = 0; i < numExplosionParticles; i++)
                    explosionParticles.AddParticle(position, velocity);

                for (int i = 0; i < numExplosionSmokeParticles; i++)
                    explosionSmokeParticles.AddParticle(position, velocity);

                return false;
            }

            return true;
        }
    }
}