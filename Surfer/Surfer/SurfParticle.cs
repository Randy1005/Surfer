using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Surfer
{
    public class SurfParticle : Particle
    {

        public new bool isVisible = false;

        // when we start surfing, set as active

        // three kinds of stopping conditions
        // 1. bump into a platform
        // 2. limited lifespan
        // 3. user switches waveform
        // encounter any of these, set as inactives

        public bool isActive = false;

        // this documents where the last position this sprite stopped 
        public Vector2 finalPos;


        // user can only surf for a limited timespan
        public const float particleLifeSpan = 2.5f;
        public float remainingLifeSpan;


        public SurfParticle(string path, Vector2 pos, Vector2 dims, float horizontalspeed) : base(path, pos, dims, horizontalspeed)
        {
            

        }


        public override void Update(GameTime gameTime)
        {

            if (isActive)
            {
                oscillationCenter = Globals.spirit.position.Y;

                travel(Globals.colorIndex, gameTime);
                
            } 
            else
            {
                Velocity = new Vector2(0f, 0f);
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            if (isVisible)
                base.Draw();
        }

        public override void travel(int waveMode, GameTime gameTime)
        {
            switch (waveMode)
            {
                case 0:
                    Velocity = new Vector2(position.X + horizontalSpeed,
                                           (oscillationCenter - Amplitude[0] - (float)Math.Cos((position.X + horizontalSpeed) / 8f) * Amplitude[0])
                                           ) - position;
                    break;
                case 1:
                    Velocity = new Vector2(position.X + horizontalSpeed,
                                           (oscillationCenter - Amplitude[1] - (float)Math.Cos((position.X + horizontalSpeed) / 12f) * Amplitude[1])
                                           ) - position;

                    break;
                case 2:
                    Velocity = new Vector2(position.X + horizontalSpeed,
                                           (oscillationCenter - Amplitude[2] - (float)Math.Cos((position.X + horizontalSpeed) / 16f) * Amplitude[2])
                                           ) - position;
                    break;

            }


            position += Velocity;
        }

        public override void setParticleColor(int colorIndex)
        {
            base.setParticleColor(colorIndex);
        }


        public override bool isTouchingLeft(Rectangle collidingRect)
        {
            return (this.ObjectRect.Right + this.Velocity.X  > collidingRect.Left) &&
                   (this.ObjectRect.Left < collidingRect.Left) &&
                   (this.ObjectRect.Bottom > collidingRect.Top) &&
                   (this.ObjectRect.Top < collidingRect.Bottom);
        }

        public override bool isTouchingRight(Rectangle collidingRect)
        {
            return (this.ObjectRect.Left + this.Velocity.X  < collidingRect.Right) &&
                   (this.ObjectRect.Right > collidingRect.Right) &&
                   (this.ObjectRect.Bottom > collidingRect.Top) &&
                   (this.ObjectRect.Top < collidingRect.Bottom);
        }

        public override bool isTouchingTop(Rectangle collidingRect)
        {
            return (this.ObjectRect.Bottom + this.Velocity.Y  > collidingRect.Top) &&
                   (this.ObjectRect.Top < collidingRect.Top) &&
                   (this.ObjectRect.Right > collidingRect.Left) &&
                   (this.ObjectRect.Left < collidingRect.Right);
        }


        public override bool isTouchingBottom(Rectangle collidingRect)
        {
            return (this.ObjectRect.Top + this.Velocity.Y  < collidingRect.Bottom) &&
                   (this.ObjectRect.Bottom > collidingRect.Bottom) &&
                   (this.ObjectRect.Right > collidingRect.Left) &&
                   (this.ObjectRect.Left < collidingRect.Right);
        }
    }
}
