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
        private const float spawnParticleIntrl = 0.01f;
        private float remainingIntrl;


        // particle collection
        public List<Particle> particles;
        public List<Particle> killList;

        public Spirit(string path, Vector2 pos, Vector2 dims, float speed) : base(path, pos, dims)
        {
            Speed = speed;
            Velocity = new Vector2(0f, 0f);


            // spawn particles
            remainingIntrl = spawnParticleIntrl;
            particles = new List<Particle>();
            killList = new List<Particle>();

            

        }



        #region basic update and draw
        public override void Update(GameTime gameTime)
        {
            // particle travel
            particlesTravel(Globals.colorIndex);


            // spirit movement
            Move(Globals.keyState);


            // gravity: but it's just constant speed along the Y-axis
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Velocity.Y += Globals.acceleration * 0.5f;

            // collisions
            spiritCollision();
            particlesCollision();


            // had to reset the velocity as it keeps going through the floor
            position += Velocity;
            Velocity = new Vector2(0f, 0f);


            // receive signal to destroy particle as its lifespan expires
            foreach (var particle in particles)
                if (particle.destroyParticle)
                    killList.Add(particle);

            foreach (Particle p in killList)
                particles.Remove(p);

            // spawn particle between intervals
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingIntrl -= timer;
            if (remainingIntrl <= 0)
            {
                // spawn new particle
                particles.Add(new Particle("particle", position, new Vector2(6f, 6f), 1.5f));

                // reset interval
                remainingIntrl = spawnParticleIntrl;
            }




            base.Update(gameTime);
            

        }

        public override void Draw()
        {
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
            else if (state.IsKeyDown(Keys.P))
            {
                
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


        #region collision callback for spirit and particles
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
        #endregion


    }
}
