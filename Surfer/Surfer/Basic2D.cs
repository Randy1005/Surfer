using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;

namespace Surfer
{
    public class Basic2D
    {

        public Vector2 position, dimensions;
        public Texture2D texture;
        public Rectangle ObjectRect;
        public float totalSeconds;

        public float GravityScale;
        public bool EnableGravity;

        public Basic2D(string path, Vector2 pos, Vector2 dims)
        {
            position = pos;
            dimensions = dims;           

            texture = Globals.content.Load<Texture2D>(path);
            ObjectRect = new Rectangle((int)(position.X - dimensions.X/2), (int)(position.Y - dimensions.Y/2), (int)dimensions.X, (int)dimensions.Y);
        }


        public virtual void Update(GameTime gameTime)
        {


            // update the rectangle bounds manually (should do the same for moving platforms)
            ObjectRect = new Rectangle((int)(position.X - dimensions.X / 2), (int)(position.Y - dimensions.Y / 2), (int)dimensions.X, (int)dimensions.Y);
        }

        public virtual void Draw()
        {

            if (texture != null)
            {
                Globals.spriteBatch.Draw(
                    texture,
                    new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y),
                    null,
                    Color.White,
                    0f,
                    new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2),
                    new SpriteEffects(),
                    0);
            }

        }



        
    }
}
