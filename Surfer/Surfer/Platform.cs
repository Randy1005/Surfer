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
    public class Platform : Basic2D
    {
        public bool isMoving;
        public Vector2 Velocity;
        public Platform(string path, Vector2 pos, Vector2 dims, Vector2 velocity, bool moving) : base(path, pos, dims)
        {
            isMoving = moving;
            Velocity = velocity;
        }

        public override void Update(GameTime gameTime)
        {

            if (isMoving)
                autoMovement();


            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw(); 
        }


        public void autoMovement()
        {
            
        }
    }
}
