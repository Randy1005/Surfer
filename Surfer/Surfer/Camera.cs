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
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Spirit target)
        {
            var position = Matrix.CreateTranslation(
                -target.position.X - (target.ObjectRect.Width * 1.1f / 2),
                -target.position.Y - (target.ObjectRect.Height * 1.1f / 2),
                0);

            var offset = Matrix.CreateTranslation(
                Globals.sceneWidth / 2,
                Globals.sceneHeight / 2,
                0);

            Transform = position * offset * Matrix.CreateScale(1.1f);
        }

    }
}
