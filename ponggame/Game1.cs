using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ponggame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont score;
        Texture2D paddleTex, ballTex, gameOverTex;
        Vector2 ballPos, paddlePos, gameOverPos , scorePos;
        private bool isGameOver;
        private float ballXSpeed, ballYSpeed, paddleSpeed;
        private int actualscore = 0;
        private bool isPaused;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1900;
            graphics.IsFullScreen = true;


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            paddleTex = Content.Load<Texture2D>("paddle2");
            ballTex = Content.Load<Texture2D>("ball2");
            if (actualscore > 5)
            {
                ballTex = Content.Load<Texture2D>("ball3");
            }
            gameOverTex = Content.Load<Texture2D>("gameover");
            score = Content.Load<SpriteFont>("file");
            ballXSpeed = 10;
            ballYSpeed = 10;        
            paddleSpeed = 25;

            ballPos = new Vector2(GraphicsDevice.Viewport.Width / 2 - ballTex.Width / 2, 0);
            paddlePos = new Vector2(GraphicsDevice.Viewport.Width / 2 - paddleTex.Width / 2, GraphicsDevice.Viewport.Height - paddleTex.Height * 1.5f);
            gameOverPos = new Vector2(GraphicsDevice.Viewport.Width / 2 - gameOverTex.Width / 2, GraphicsDevice.Viewport.Height / 2 - gameOverTex.Height / 2);
            scorePos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2 + 0.13f * ballTex.Height);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            if (!isPaused)
            {
                if (!isGameOver)
                {
                    ballPos.X += ballXSpeed;
                    ballPos.Y += ballYSpeed;
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        paddlePos.X -= paddleSpeed;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        paddlePos.X += paddleSpeed;
                    }
                    if (ballPos.X <= 0)
                    {
                        ballXSpeed *= -1;
                    }
                    if (ballPos.Y <= 0)
                    {
                        ballYSpeed *= -1;
                    }
                    if (ballPos.X >= GraphicsDevice.Viewport.Width - ballTex.Width)
                    {
                        ballXSpeed *= -1;
                    }
                    if (ballPos.Y >= paddlePos.Y - ballTex.Width)
                    {
                        if (ballPos.X + ballTex.Width >= paddlePos.X & ballPos.X <= paddlePos.X + paddleTex.Width)
                        {
                            ballYSpeed *= -1.1f;
                            ballXSpeed *= 1.15f;
                            paddleSpeed *= 1.025f;
                            actualscore++;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.F))
                    {
                        graphics.IsFullScreen = !graphics.IsFullScreen;
                    }


                    if (ballPos.Y >= GraphicsDevice.Viewport.Height - ballTex.Width)
                    {
                        isGameOver = true;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                  
                    isPaused = true;
                }

            }
            if (isPaused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    isPaused = false;
                }
            }

            if (isGameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    ballPos = new Vector2(GraphicsDevice.Viewport.Width / 2 - ballTex.Width / 2, 0);
                    paddlePos = new Vector2(GraphicsDevice.Viewport.Width / 2 - paddleTex.Width / 2, GraphicsDevice.Viewport.Height - paddleTex.Height * 1.5f);
                    ballXSpeed = 10;
                    ballYSpeed = 10;
                    isGameOver = false;
                    actualscore = 0;
                }
            }



            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            string output = "Score: " + actualscore.ToString();
            Vector2 FontOrigin = score.MeasureString(output) / 2;
            
            if (!isGameOver)
            {
            spriteBatch.Draw(ballTex, ballPos, null);
            spriteBatch.Draw(paddleTex, paddlePos, null);
            spriteBatch.DrawString(score, output, scorePos, Color.Red,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
            if (isGameOver)
            {
                spriteBatch.Draw(gameOverTex, gameOverPos, null);
                spriteBatch.DrawString(score, output, scorePos, Color.Red,
                    0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
                
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
