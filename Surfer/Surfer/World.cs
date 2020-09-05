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


        public World()
        {
            Globals.acceleration = 9.8f;
            Globals.colorIndex = 0;



            // Main Character (start pos: [200, 730])
            // tunnel [1040, 640]
            spirit = new Spirit("spirit", new Vector2(1040, 620), new Vector2(40, 64), 0.5f);
            Globals.spirit = spirit;
            spirit.EnableGravity = true;
            spirit.GravityScale = 0.1f;

            


            // Add Platforms
            Globals.platforms = new List<Platform>();
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(400, 800), new Vector2(600, 60)));
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(420, 720), new Vector2(100, 120)));
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(900, 720), new Vector2(300, 120)));

            // E1: Narrow tunnel
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(1200, 1060), new Vector2(100, 800)));
            Globals.platforms.Add(new Platform("09 day plat02_flipV", new Vector2(1200, 150), new Vector2(100, 800)));

            // E2: after tunnel
            // 1. switch to go up
            // 2. free fall, wait for the platform below
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(1480, 620), new Vector2(200, 60)));
            Globals.platforms.Add(new Platform("09 day plat02", new Vector2(1520, 920), new Vector2(200, 60)));




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


    }

   
}
