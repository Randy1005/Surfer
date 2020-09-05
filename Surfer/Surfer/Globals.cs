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
    public class Globals
    {
        public static int sceneWidth;
        public static int sceneHeight;
        public static float acceleration;
        public static ContentManager content;
        public static SpriteBatch spriteBatch;
        public static KeyboardState keyState;
        public static List<Platform> platforms;
        public static int colorIndex;
        public static bool isScrolling;
        public static Spirit spirit;
    }
}
