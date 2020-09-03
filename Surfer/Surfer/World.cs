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
    public class World
    {

        public Spirit spirit;

        public World()
        {
            Globals.acceleration = -2f;

            // Main Character
            spirit = new Spirit("spirit", new Vector2(500f, 120f), new Vector2(40f, 64f), 2f);

            // Add Platforms
            Globals.platforms = new List<Platform>();
            Globals.platforms.Add(new Platform("GroundSprite", new Vector2(500f, 400f), new Vector2(500f, 60f), new Vector2(0f, 0f), false));
        
        }

        public virtual void Update()
        {
            spirit.Update();
            foreach (var platform in Globals.platforms)
            {
                platform.Update();

            }

            
        }

        public virtual void Draw()
        {
            spirit.Draw();
            foreach (var platform in Globals.platforms)
                platform.Draw();
        }


    }

   
}
