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
        // handle mouse scroll
        int currScrollValue, prevScrollValue = 0;
        int colorAmount = 3;


        public Spirit spirit;
        public List<Vector2> spawnSpots;


        public World()
        {
            Globals.colorIndex = 0;
            spawnSpots = new List<Vector2>();

            // spawn spots
            spawnSpots.Add(new Vector2(200, 730));
            spawnSpots.Add(new Vector2(920, 620));
            spawnSpots.Add(new Vector2(1480, 500));
            

            
            // Main Character (start pos: [200, 700])
            // spawn spots:
            // E1 [920, 620]
            // E2 [1480, 500]
            
            spirit = new Spirit("spirit", new Vector2(200, 700), new Vector2(40, 64), 1.5f);
            Globals.spirit = spirit;
            spirit.EnableGravity = true;
            spirit.GravityScale = 0.01f;

            

            // Add Platforms
            Globals.platforms = new List<Platform>();
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(400, 800), new Vector2(600, 60)));
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(420, 720), new Vector2(100, 120)));
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(900, 720), new Vector2(300, 120)));

            // E1: Narrow tunnel
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(1180, 1060), new Vector2(100, 800)));
            Globals.platforms.Add(new Platform("09 day plat02_flipV", new Vector2(1180, 150), new Vector2(100, 800)));

            // E2: after tunnel
            // 1. switch to go up
            // 2. free fall, wait for the platform below
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(1480, 620), new Vector2(200, 80)));
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(1500, 920), new Vector2(160, 60)));

            // E3: wave through gaps
            // 1. Big obstacle ahead, had to free fall a little
            // 2. three equal gapped cube shaped obstacle
            Globals.platforms.Add(new Platform("09 day plat02_flipV", new Vector2(1900, 280), new Vector2(300, 600)));
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(1850, 700), new Vector2(40, 40)));
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(1850, 820), new Vector2(40, 40)));
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(1850, 940), new Vector2(40, 40)));



        }

        public virtual void Update(GameTime gameTime)
        {

            if (spirit.position.Y > 1500)
            {
                respawnSpirit(spirit);
            }     

            spirit.Update(gameTime);

            foreach (var platform in Globals.platforms)
            {
                platform.Update(gameTime);

            }

            foreach (var particle in spirit.particles)
            {
                particle.Update(gameTime);
            }


            HandleMouseScroll();
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

        public void HandleMouseScroll()
        {
            MouseState mState = Mouse.GetState();
            currScrollValue = mState.ScrollWheelValue;

            if (currScrollValue > prevScrollValue)
            {
                if (Globals.colorIndex < colorAmount - 1)
                {
                    Globals.colorIndex++;
                }
                else
                {
                    Globals.colorIndex--;
                }

                // update particle color
                foreach (Particle p in spirit.particles)
                    p.setParticleColor(Globals.colorIndex);

                // if surfing particle is active, scrolling interrupts its movement
                if (spirit.surfP.isActive)
                {
                    // set as invisble and inactive
                    spirit.surfP.isActive = false;
                    spirit.surfP.isVisible = false;

                    // store its final position
                    spirit.surfP.finalPos = spirit.surfP.position;

                    // set spirit to that position
                    spirit.position = spirit.surfP.finalPos;
                    spirit.isVisible = true;
                }

            } else if (currScrollValue < prevScrollValue)
            {

                if (Globals.colorIndex > 0)
                {
                    Globals.colorIndex--;
                } 
                else
                {
                    Globals.colorIndex = colorAmount - 1;
                }

                // update particle color
                foreach (Particle p in spirit.particles)
                    p.setParticleColor(Globals.colorIndex);

                // if surfing particle is active, scrolling interrupts its movement
                if (spirit.surfP.isActive)
                {
                    // set as invisble and inactive
                    spirit.surfP.isActive = false;
                    spirit.surfP.isVisible = false;

                    // store its final position
                    spirit.surfP.finalPos = spirit.surfP.position;

                    // set spirit to that position
                    spirit.position = spirit.surfP.finalPos;
                    spirit.isVisible = true;
                }


            }
            
            prevScrollValue = currScrollValue;
        }


        public void respawnSpirit (Spirit spirit)
        {
            if (spirit.position.X > spawnSpots[0].X && spirit.position.X < spawnSpots[1].X)
                spirit.position = spawnSpots[0];
            else if (spirit.position.X > spawnSpots[1].X && spirit.position.X < spawnSpots[2].X)
                spirit.position = spawnSpots[1];
            else if (spirit.position.X > spawnSpots[2].X)
                spirit.position = spawnSpots[2];
        }


    }

   
}
