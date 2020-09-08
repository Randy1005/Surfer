using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;
using System.Reflection;

namespace Surfer
{


    public class Timer
    {

        public SpriteFont font;
        public Vector2 _position;
        public Color _color;
        public int seconds;
        public int finalResult;

        public float delay = 1f;
        public float remainingDelay;
        public Timer(string fontpath, Vector2 pos, Color color)
        {
            font = Globals.content.Load<SpriteFont>(fontpath);
            _position = pos;
            _color = color;

            seconds = 0;
            remainingDelay = delay;
        }


        public void Update(GameTime gameTime)
        {
            if (Globals.spirit.isOnGround && !World.gameOver)
            {
                var elapsedtime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                remainingDelay -= elapsedtime;
                if (remainingDelay <= 0)
                {
                    // a second passed
                    seconds++;
                    remainingDelay = delay;

                }

                
            }

            if (!World.gameOver)
                _position = Globals.spirit.position - new Vector2(750, 400);
            else
                _position = Globals.spirit.position - new Vector2(500, 0);
        }

        public void Draw()
        {
            if (!World.gameOver)
                Globals.spriteBatch.DrawString(font, "Time Spent On Ground: " + seconds, _position, _color);
            else
                Globals.spriteBatch.DrawString(font, "Game Over! That's " + seconds + " Seconds on the floor!\n" + "Press P to start over.", _position, _color);
        }

    }
}
