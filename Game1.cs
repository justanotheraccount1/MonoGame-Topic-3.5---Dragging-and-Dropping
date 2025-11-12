using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Topic_3._5___Dragging_and_Dropping
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        Texture2D carTexture, rocketTexture, asteriodTexture;
        Rectangle carRect, rocketRect, asteriodRect, window;
        bool isDraggingCar = false;
        bool isDraggingRocket = false;
        bool isDraggingAsteriod = false;
        MouseState mouseState, prevMouseState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            asteriodRect = new Rectangle(10, 10, 50, 50);
            carRect = new Rectangle(200, 200, 75, 25);
            rocketRect = new Rectangle(400, 100, 40, 75);
            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            asteriodTexture = Content.Load<Texture2D>("Images/asteroid");
            carTexture = Content.Load<Texture2D>("Images/fast_car");
            rocketTexture = Content.Load<Texture2D>("Images/rocket");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (NewClick() && asteriodRect.Contains(mouseState.Position))
                isDraggingAsteriod = true;
            else if (isDraggingAsteriod && mouseState.LeftButton == ButtonState.Released)
                isDraggingAsteriod = false;
            else if (isDraggingAsteriod)
                asteriodRect.Offset(mouseState.X - prevMouseState.X, mouseState.Y - prevMouseState.Y);

            if (NewClick() && carRect.Contains(mouseState.Position))
                isDraggingCar = true;
            else if (isDraggingAsteriod && mouseState.LeftButton == ButtonState.Released)
                isDraggingCar = false;
            else if (isDraggingAsteriod)
                asteriodRect.Offset(mouseState.X - prevMouseState.X, 0);

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(carTexture, carRect, Color.White);
            _spriteBatch.Draw(rocketTexture, rocketRect, Color.White);
            _spriteBatch.Draw(asteriodTexture, asteriodRect, Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        protected bool NewClick()
        {
            return mouseState.LeftButton == ButtonState.Pressed && 
                prevMouseState.LeftButton == ButtonState.Released;

        }

    }
}
