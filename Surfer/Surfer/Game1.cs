using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Surfer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public World _world;
        public Camera camera;
        public Song bgm;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            


        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            




        }

        protected override void LoadContent()
        {
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);


            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            Globals.sceneWidth = _graphics.PreferredBackBufferWidth;
            Globals.sceneHeight = _graphics.PreferredBackBufferHeight;

            // TODO: use this.Content to load your game content here

            bgm = Globals.content.Load<Song>("BGM_trimmed");

            _world = new World();
            camera = new Camera();

            MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Globals.keyState = Keyboard.GetState();




            _world.Update(gameTime);

            if (!Globals.spirit.surfP.isActive)
            {
                camera.Follow(Globals.spirit);
            }
            else 
            {
                camera.Follow2(Globals.spirit.surfP.position, Globals.spirit.surfPPosStatic);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            // TODO: Add your drawing code here
            Globals.spriteBatch.Begin(transformMatrix: camera.Transform);




            _world.Draw();



            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
