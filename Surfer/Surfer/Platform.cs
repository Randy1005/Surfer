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

        public Platform(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
        {

        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw(); 
        }


    }
}
