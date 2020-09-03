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

        public Spirit(string path, Vector2 pos, Vector2 dims, Vector2 velocity) : base(path, pos, dims)
        {
            Velocity = velocity;

        }




        public override void Update()
        {
            HandleInput(Globals.keyState);


            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void HandleInput(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.A))
            {
                position = new Vector2(position.X - Velocity.X, position.Y);
            } 
            else if (state.IsKeyDown(Keys.D))
            {
                position = new Vector2(position.X + Velocity.X, position.Y);
            }
            else if (state.IsKeyDown(Keys.S))
            {
                position = new Vector2(position.X , position.Y + Velocity.Y);
            }
            else if (state.IsKeyDown(Keys.W))
            {
                position = new Vector2(position.X, position.Y - Velocity.Y);
            }


        }
    }
}
