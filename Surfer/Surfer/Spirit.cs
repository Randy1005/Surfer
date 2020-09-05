using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Surfer
{
    public class Spirit : Basic2D
    {

        public Vector2 Velocity;
        public float Speed;
        private const double spawnParticleIntrl = 0.0000001f;
        private double remainingIntrl;
        public bool isVisible = true;


        // particle collection
        public List<Particle> particles;
        public List<Particle> killList;
        public SurfParticle surfP;

        public Spirit(string path, Vector2 pos, Vector2 dims, float speed) : base(path, pos, dims)
        {
            Speed = speed;
            Velocity = new Vector2(0f, 0f);


            // spawn particles
            remainingIntrl = spawnParticleIntrl;
            particles = new List<Particle>();
            killList = new List<Particle>();

            // spawn the surfing sprite, but it's invisible at first, will need to toggle visibility
            surfP = new SurfParticle("surfingPikachu", position, new Vector2(8f, 8f), 2f);

        }



        #region basic update and draw
        public override void Update(GameTime gameTime)
        {
            // particle travel
            particlesTravel(Globals.colorIndex);


            // spirit movement
            Move(Globals.keyState);


            // gravity: but it's just constant speed along the Y-axis
            if (!surfP.isActive)
                ApplyGravity(gameTime);

            // collisions
            spiritCollision();
            particlesCollision();
            surfParticleCollision();


            // had to reset the velocity as it keeps going through the floor
            position += Velocity;
            Velocity.X = 0;


            // receive signal to destroy particle as its lifespan expires
            foreach (var particle in particles)
                if (particle.destroyParticle)
                    killList.Add(particle);

            foreach (Particle p in killList)
                particles.Remove(p);

            // spawn particle between intervals
            var timer = (double)gameTime.ElapsedGameTime.TotalSeconds;
            remainingIntrl -= timer;
            if (remainingIntrl <= 0)
            {
                // spawn new particle
                particles.Add(new Particle("particle", position, new Vector2(6f, 6f), 1.5f));

                // reset interval
                remainingIntrl = spawnParticleIntrl;
            }

            // spawn the SurfParticle when space pressed
            if (Globals.keyState.IsKeyDown(Keys.Space))
            {
                // set the surfing sprite as visible and as active
                surfP.isVisible = true;
                surfP.isActive = true;

                // update its position to the spirit's position and set the spirit as invisible
                surfP.position = position;
                isVisible = false;

            }


            if (surfP.isActive)
            {

                // hide the particles
                foreach (Particle p in particles)
                    p.isVisible = false;
            }






            surfP.Update(gameTime);
            base.Update(gameTime);
            

        }

        public override void Draw()
        {
            surfP.Draw();

            if (isVisible)
                base.Draw();


        }

        #endregion


        #region spirit and particles movement
        public void Move(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.A))
            {
                Velocity.X = -Speed;
            } 
            else if (state.IsKeyDown(Keys.D))
            {
                Velocity.X = Speed;
            }

        }

        public void particlesTravel(int waveMode)
        {
            foreach (var particle in particles)
                particle.travel(waveMode);
        }
        #endregion

        #region collision bound test
        public bool isTouchingLeft(Rectangle collidingRect)
        {
            return (this.ObjectRect.Right + this.Velocity.X > collidingRect.Left) &&
                   (this.ObjectRect.Left < collidingRect.Left) &&
                   (this.ObjectRect.Bottom > collidingRect.Top) &&
                   (this.ObjectRect.Top < collidingRect.Bottom);

        }

        public bool isTouchingRight(Rectangle collidingRect)
        {
            return (this.ObjectRect.Left + this.Velocity.X < collidingRect.Right) &&
                   (this.ObjectRect.Right > collidingRect.Right) &&
                   (this.ObjectRect.Bottom > collidingRect.Top) &&
                   (this.ObjectRect.Top < collidingRect.Bottom);
        }

        public bool isTouchingTop(Rectangle collidingRect)
        {
            return (this.ObjectRect.Bottom + this.Velocity.Y > collidingRect.Top) &&
                   (this.ObjectRect.Top < collidingRect.Top) &&
                   (this.ObjectRect.Right > collidingRect.Left) &&
                   (this.ObjectRect.Left < collidingRect.Right);
        }

        public bool isTouchingBottom(Rectangle collidingRect)
        {
            return (this.ObjectRect.Top + this.Velocity.Y < collidingRect.Bottom) &&
                   (this.ObjectRect.Bottom > collidingRect.Bottom) &&
                   (this.ObjectRect.Right > collidingRect.Left) &&
                   (this.ObjectRect.Left < collidingRect.Right);
        }

        #endregion


        #region collision callback for spirit and particles and surfParticle
        public void spiritCollision()
        {
            // spirit collision with platforms
            foreach (var platform in Globals.platforms)
            {


                if (Velocity.X > 0 && isTouchingLeft(platform.ObjectRect) ||
                    Velocity.X < 0 && isTouchingRight(platform.ObjectRect))

                {
                    Velocity.X = 0;
                }

                if (Velocity.Y > 0 && isTouchingTop(platform.ObjectRect) ||
                   Velocity.Y < 0 && isTouchingBottom(platform.ObjectRect))
                {
                    Velocity.Y = 0;
                }
            }
        }

        public void particlesCollision()
        {
            // particle collision with platform
            foreach (var platform in Globals.platforms)
            {
                foreach (var particle in particles)
                {
                    if (particle.Velocity.X > 0 && particle.isTouchingLeft(platform.ObjectRect) ||
                        particle.Velocity.X < 0 && particle.isTouchingRight(platform.ObjectRect) ||
                        particle.Velocity.Y > 0 && particle.isTouchingTop(platform.ObjectRect) ||
                        particle.Velocity.Y < 0 && particle.isTouchingBottom(platform.ObjectRect))
                    {
                        // reset particle position to spirit position
                        particle.position = position;
                    }
                }

            }
        }

        public void surfParticleCollision()
        {

            foreach (var platform in Globals.platforms)
            {
               
                if (surfP.Velocity.X > 0 && surfP.isTouchingLeft(platform.ObjectRect) ||
                    surfP.Velocity.X < 0 && surfP.isTouchingRight(platform.ObjectRect) ||
                    surfP.Velocity.Y > 0 && surfP.isTouchingTop(platform.ObjectRect) ||
                    surfP.Velocity.Y < 0 && surfP.isTouchingBottom(platform.ObjectRect))
                {
                    

                    // store final position
                    if (surfP.isActive)
                        surfP.finalPos = surfP.position;

                    // set surfP as inactive and hide it
                    surfP.isActive = false;
                    surfP.isVisible = false;

                    // show spirit
                    if (surfP.Velocity.X > 0 && surfP.isTouchingLeft(platform.ObjectRect))
                        position = surfP.finalPos - new Vector2(20f, 0f);
                    else if (surfP.Velocity.X < 0 && surfP.isTouchingRight(platform.ObjectRect))
                        position = surfP.finalPos + new Vector2(20f, 0f);
                    else if (surfP.Velocity.Y > 0 && surfP.isTouchingTop(platform.ObjectRect))
                        position = surfP.finalPos - new Vector2(0f, 20f);
                    else
                        position = surfP.finalPos + new Vector2(0f, 20f);



                    isVisible = true;
                }

            }
        }
        #endregion

        #region Gravity
        private void ApplyGravity(GameTime gameTime)
        {

            float DeltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (EnableGravity)
            {
                this.Velocity.Y += (98f * DeltaSeconds) * GravityScale;
            }
        }
        #endregion
    }
}
