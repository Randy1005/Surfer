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
    public class Particle : Basic2D
    {      
        public Vector2 Velocity;
        public float horizontalSpeed;
        public List<float> Amplitude;
        public float oscillationCenter;
        Color[] textureColor;
        public bool isVisible = true;


        // indicating when this particle should be detroyed
        private const float particleLifeSpan = 2.5f;
        private float remainingLifeSpan;
        public bool destroyParticle = false;

        public Particle(string path, Vector2 pos, Vector2 dims, float horizontalspeed) : base(path, pos, dims)
        {
            horizontalSpeed = horizontalspeed;;
            Amplitude = new List<float>(3);
            Amplitude.Add(20f);
            Amplitude.Add(100f);
            Amplitude.Add(200f);

            oscillationCenter = position.Y;

            remainingLifeSpan = particleLifeSpan;

            // get the color pixels of this particle
            textureColor = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(textureColor);

        }

        public override void Update(GameTime gameTime)
        {

            // let this particle travel for a few seconds and delete it
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingLifeSpan -= timer;
            if (remainingLifeSpan <= 0)
            {
                destroyParticle = true;
                remainingLifeSpan = particleLifeSpan;
            }




            base.Update(gameTime);
        }

        public override void Draw()
        {
            if (isVisible)
                base.Draw();
        }



        public virtual void travel(int waveMode, GameTime gameTime)
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
                                           (oscillationCenter - Amplitude[1] - (float)Math.Cos((position.X + horizontalSpeed) / 14f) * Amplitude[1])
                                           ) - position;

                    break;
                case 2:
                    Velocity = new Vector2(position.X + horizontalSpeed,
                                           (oscillationCenter - Amplitude[2] - (float)Math.Cos((position.X + horizontalSpeed) / 20f) * Amplitude[2])
                                           ) - position;
                    break;

            }


            position += Velocity;


        }

        public virtual void setParticleColor(int colorIndex)
        {
            switch (colorIndex)
            {
                case 0:
                    for (int i = 0; i < textureColor.Length; i++)
                    {
                        textureColor[i] = Color.Red;
                    }
                    break;
                case 1:
                    for (int i = 0; i < textureColor.Length; i++)
                    {
                        textureColor[i] = Color.Yellow;
                    }
                    break;
                case 2:
                    for (int i = 0; i < textureColor.Length; i++)
                    {
                        textureColor[i] = Color.DodgerBlue;
                    }
                    break;
            }

            texture.SetData(textureColor);
        }



        public virtual bool isTouchingLeft(Rectangle collidingRect)
        {
            return (this.ObjectRect.Right + this.Velocity.X > collidingRect.Left) &&
                   (this.ObjectRect.Left < collidingRect.Left) &&
                   (this.ObjectRect.Bottom > collidingRect.Top) &&
                   (this.ObjectRect.Top < collidingRect.Bottom);

        }

        public virtual bool isTouchingRight(Rectangle collidingRect)
        {
            return (this.ObjectRect.Left + this.Velocity.X < collidingRect.Right) &&
                   (this.ObjectRect.Right > collidingRect.Right) &&
                   (this.ObjectRect.Bottom > collidingRect.Top) &&
                   (this.ObjectRect.Top < collidingRect.Bottom);
        }

        public virtual bool isTouchingTop(Rectangle collidingRect)
        {
            return (this.ObjectRect.Bottom + this.Velocity.Y > collidingRect.Top) &&
                   (this.ObjectRect.Top < collidingRect.Top) &&
                   (this.ObjectRect.Right > collidingRect.Left) &&
                   (this.ObjectRect.Left < collidingRect.Right);
        }

        public virtual bool isTouchingBottom(Rectangle collidingRect)
        {
            return (this.ObjectRect.Top + this.Velocity.Y < collidingRect.Bottom) &&
                   (this.ObjectRect.Bottom > collidingRect.Bottom) &&
                   (this.ObjectRect.Right > collidingRect.Left) &&
                   (this.ObjectRect.Left < collidingRect.Right);
        }
    }
}
