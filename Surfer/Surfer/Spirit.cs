using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace Surfer
{
    public class Spirit : Basic2D
    {

        public Vector2 Velocity;
        public float Speed;
        public bool isMidAir;

        public Spirit(string path, Vector2 pos, Vector2 dims, float speed) : base(path, pos, dims)
        {
            Speed = speed;
            Velocity = new Vector2(0f, 0f);
        }




        public override void Update()
        {
            Move(Globals.keyState);

            foreach (var platform in Globals.platforms)
            {
                if (!isTouchingTop(platform.ObjectRect))
                    isMidAir = true;

                if (this.Velocity.X > 0 && isTouchingLeft(platform.ObjectRect) ||
                    this.Velocity.X < 0 && isTouchingRight(platform.ObjectRect))

                {
                    this.Velocity.X = 0;
                }

                if (this.Velocity.Y > 0 && isTouchingTop(platform.ObjectRect) ||
                   this.Velocity.Y < 0 && isTouchingBottom(platform.ObjectRect))
                {
                    this.Velocity.Y = 0;
                }
            }


            position += Velocity;
            Velocity = Vector2.Zero;

        }

        public override void Draw()
        {
            base.Draw();
        }

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
            else if (state.IsKeyDown(Keys.S))
            {
                Velocity.Y = Speed;
            }
            else if (state.IsKeyDown(Keys.W))
            {
                Velocity.Y = -Speed;
            }

           

        }


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

    }
}
