using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;

namespace Surfer
{
    public class BackGround : Basic2D
    {

        public bool moveWithCamera;
        public BackGround(string path, Vector2 pos, Vector2 dims, bool moveWithCam) : base(path, pos, dims)
        {
            moveWithCamera = moveWithCam;
        }

        public override void Update(GameTime gameTime)
        {

            if (moveWithCamera)
            {
                position = Globals.spirit.position;
            }
            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
