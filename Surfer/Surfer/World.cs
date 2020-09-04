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
            Globals.acceleration = 9.8f;

            // Main Character
            spirit = new Spirit("spirit", new Vector2(600, 730), new Vector2(40, 64), 3f);

            // Add Platforms
            Globals.platforms = new List<Platform>();
            Globals.platforms.Add(new Platform("GroundSprite", new Vector2(800, 800), new Vector2(600, 60), new Vector2(0, 0), false));
            Globals.platforms.Add(new Platform("GroundSprite", new Vector2(860, 720), new Vector2(100, 120), new Vector2(0, 0), false));




        }

        public virtual void Update(GameTime gameTime)
        {

            spirit.Update(gameTime);

            foreach (var platform in Globals.platforms)
            {
                platform.Update(gameTime);

            }

            foreach (var particle in spirit.particles)
            {
                particle.Update(gameTime);
            }


        }

        public virtual void Draw()
        {
            spirit.Draw();

            foreach (var platform in Globals.platforms)
            {
                platform.Draw();
            }

            foreach (var particle in spirit.particles)
            {
                particle.Draw();
            }
        }


    }

   
}
