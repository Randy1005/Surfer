using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Surfer
{
    public class World
    {

        public static bool gameOver = false;

        // handle mouse scroll
        int currScrollValue, prevScrollValue = 0;
        int colorAmount = 3;


        public List<BackGround> bgObjList;
        public Spirit spirit;
        public Timer timer;
        
        public static List<Vector2> spawnSpots;

        public static SoundEffectInstance walkSFXInst;
        public static SoundEffectInstance hitWallSFXInst;
        public static SoundEffectInstance RedWaveSFXInst;
        public static SoundEffectInstance YellowWaveSFXInst;
        public static SoundEffectInstance BlueWaveSFXInst;
        public static SoundEffectInstance FallSFXInst;

        



        public World()
        {
            Globals.colorIndex = 0;

            // load animations
            //var animations = new Dictionary<string, Animation>()
            //{
            //    {"TransformIn", new Animation(Globals.content.Load<Texture2D>("TransformToLight"), 5)},
            //    {"LightTravel", new Animation(Globals.content.Load<Texture2D>("lightTravelling"), 21)}
            //};
            

            // load SFXs
            Globals.WalkingSFX = Globals.content.Load<SoundEffect>("Walking");
            walkSFXInst = Globals.WalkingSFX.CreateInstance();
            walkSFXInst.Volume = 1f;

            Globals.HitWallSFX = Globals.content.Load<SoundEffect>("HittingWall");
            hitWallSFXInst = Globals.HitWallSFX.CreateInstance();
            hitWallSFXInst.Volume = 0.7f;

            Globals.RedWaveSFX = Globals.content.Load<SoundEffect>("RedWave");
            RedWaveSFXInst = Globals.RedWaveSFX.CreateInstance();
            RedWaveSFXInst.Volume = 1f;

            Globals.YellowWaveSFX = Globals.content.Load<SoundEffect>("YellowWave");
            YellowWaveSFXInst = Globals.YellowWaveSFX.CreateInstance();
            YellowWaveSFXInst.Volume = 1f;

            Globals.BlueWaveSFX = Globals.content.Load<SoundEffect>("BlueWave");
            BlueWaveSFXInst = Globals.BlueWaveSFX.CreateInstance();
            BlueWaveSFXInst.Volume = 1f;

            Globals.FallSFX = Globals.content.Load<SoundEffect>("Falling");
            FallSFXInst = Globals.FallSFX.CreateInstance();
            FallSFXInst.Volume = 0.7f;


            // spawn spots
            spawnSpots = new List<Vector2>();
            spawnSpots.Add(new Vector2(200, 730));
            spawnSpots.Add(new Vector2(920, 620));
            spawnSpots.Add(new Vector2(1480, 500));
            spawnSpots.Add(new Vector2(2290, 610));



            bgObjList = new List<BackGround>();
            bgObjList.Add(new BackGround("14background", spawnSpots[0], new Vector2(Globals.sceneWidth * 2, Globals.sceneHeight * 2), true));
            bgObjList.Add(new BackGround("09sunormoon", spawnSpots[0] - new Vector2(50, 500), new Vector2(100, 100), true));
            bgObjList.Add(new BackGround("14backtree", new Vector2(100, 800), new Vector2(800, 1000), false));
            bgObjList.Add(new BackGround("14backtree", new Vector2(400, 800), new Vector2(800, 1000), false));
            bgObjList.Add(new BackGround("14fronttreesingle2", new Vector2(600, 800), new Vector2(600, 1000), false));
            bgObjList.Add(new BackGround("14fronttreesingle1", new Vector2(1180, 800), new Vector2(600, 1000), false));
            bgObjList.Add(new BackGround("14backtreesingle", new Vector2(1580, 800), new Vector2(600, 1000), false));
            bgObjList.Add(new BackGround("14backtree", new Vector2(1800, 850), new Vector2(800, 1100), false));
            bgObjList.Add(new BackGround("14fronttreesingle2", new Vector2(2200, 1000), new Vector2(900, 800), false));



            // Main Character (start pos: [200, 700])
            // spawn spots:
            // E1 [920, 620]
            // E2 [1480, 500]
            // E4 [2290, 610]
            // End [2800, 250]
            spirit = new Spirit("spirit", spawnSpots[0], new Vector2(40, 64), 1.5f);
            Globals.spirit = spirit;
            spirit.EnableGravity = true;
            spirit.GravityScale = 0.008f;



            // Add Platforms
            Globals.platforms = new List<Platform>();
            Globals.platforms.Add(new Platform("14plat2", new Vector2(0, 800), new Vector2(200, 100), 0));
            Globals.platforms.Add(new Platform("14plat2", new Vector2(200, 800), new Vector2(200, 100), 0));
            Globals.platforms.Add(new Platform("14plat2", new Vector2(400, 800), new Vector2(200, 100), 0));
            Globals.platforms.Add(new Platform("14plat2", new Vector2(600, 800), new Vector2(200, 100), 0));
            Globals.platforms.Add(new Platform("14plat2", new Vector2(420, 720), new Vector2(100, 120), 0));
            Globals.platforms.Add(new Platform("14plat2", new Vector2(900, 720), new Vector2(300, 120), 0));

            // E1: Narrow tunnel
            Globals.platforms.Add(new Platform("14plat3", new Vector2(1180, 1060), new Vector2(100, 220), 0));
            Globals.platforms.Add(new Platform("14plat3", new Vector2(1180, 860), new Vector2(100, 200), 0));
            Globals.platforms.Add(new Platform("14plat3", new Vector2(1180, 760), new Vector2(100, 100), 0));
            Globals.platforms.Add(new Platform("14plat3", new Vector2(1180, 710), new Vector2(100, 90), 0));
            Globals.platforms.Add(new Platform("09 day plat02_flipV", new Vector2(1180, 150), new Vector2(100, 800), 0));

            // E2: after tunnel
            // 1. switch to go up
            // 2. free fall, wait for the platform below
            // 3. make it so that tall wave won't work
            Globals.platforms.Add(new Platform("14plat2", new Vector2(1480, 620), new Vector2(200, 80), 0));
            Globals.platforms.Add(new Platform("14plat2", new Vector2(1500, 920), new Vector2(160, 60), 0));
            Globals.platforms.Add(new Platform("09 day plat02_flipV", new Vector2(1425, 250), new Vector2(400, 300), 0));


            // E3: wave through gaps
            // 1. Big obstacle ahead, had to free fall a little
            // 2. three equal gapped cube shaped obstacle
            Globals.platforms.Add(new Platform("09 day plat02_flipV", new Vector2(1900, 330), new Vector2(300, 600), 0));
            Globals.platforms.Add(new Platform("14plat2", new Vector2(1850, 700), new Vector2(40, 40), 0));
            Globals.platforms.Add(new Platform("14plat2", new Vector2(1850, 820), new Vector2(40, 40), 0));
            Globals.platforms.Add(new Platform("14plat2", new Vector2(1850, 940), new Vector2(40, 40), 0));

            // E4: final high ground
            Globals.platforms.Add(new Platform("14plat3", new Vector2(2290, 1000), new Vector2(150, 700), 0));
            Globals.platforms.Add(new Platform("09 day plat02_flipV", new Vector2(2250, 300), new Vector2(400, 400), 0));

            // E5: End
            Globals.platforms.Add(new Platform("14plat3", new Vector2(2890, 800), new Vector2(500, 700), 0));
            Globals.platforms.Add(new Platform("14cave", new Vector2(2890, 190), new Vector2(100, 100), 99));
            bgObjList.Add(new BackGround("14stone1", new Vector2(2800, 400), new Vector2(150, 150), false));
            bgObjList.Add(new BackGround("14stone2", new Vector2(3000, 400), new Vector2(150, 150), false));

            // Timer instance
            timer = new Timer("Timer", spirit.position - new Vector2(600, 400), Color.GhostWhite);

        }

        public virtual void Update(GameTime gameTime)
        {

            if (spirit.position.Y > 1200)
            {
                FallSFXInst.Play();
            }

            if (spirit.position.Y > 1250)
            {
                respawnSpirit(spirit);
            }     


            foreach (var bg in bgObjList)
            {
                bg.Update(gameTime);
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

            timer.Update(gameTime);


            HandleMouseScroll();

            if (gameOver && Globals.keyState.IsKeyDown(Keys.P))
            {
                // reset

                // spirit reset
                spirit.position = spawnSpots[0];

                // timer reset
                timer.seconds = 0;

                gameOver = false;
            }
            
        }

        public virtual void Draw()
        {


            foreach (var bg in bgObjList)
            {
                bg.Draw();
            }


            spirit.Draw();

            foreach (var platform in Globals.platforms)
            {
                platform.Draw();
            }

            foreach (var particle in spirit.particles)
            {
                particle.Draw();
            }


            timer.Draw();

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
            else if (spirit.position.X > spawnSpots[2].X && spirit.position.X < spawnSpots[3].X)
                spirit.position = spawnSpots[2];
            else
                spirit.position = spawnSpots[3];
        }


    }

   
}
