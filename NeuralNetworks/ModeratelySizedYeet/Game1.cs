using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using System.Collections.Generic;
using sel = OpenQA.Selenium;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using WindowsInput;
using System.Diagnostics;

namespace _2048_Solver
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState prevState;

        InputSimulator inputSimulator;

        //RIP botMan


        Texture2D tile;
        SpriteFont font;


        Dictionary<int, Color> squareColors;

        TimeSpan prevTime = new TimeSpan(0);
        TimeSpan elapsedTime = new TimeSpan(0);
        TimeSpan moveDelay = TimeSpan.FromMilliseconds(1);

        TestResult status;

        Stopwatch updateTimer = new Stopwatch();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

       //     this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d); //figure out how to work this

            LordOfTheBots.bigYEET = true;//this sets something in botLord so that it starts existing
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            
            //this.IsFixedTimeStep = true;
            

            inputSimulator = new InputSimulator();

            squareColors = new Dictionary<int, Color>();

            squareColors[0] = new Color(255, 255, 255);
            squareColors[2] = new Color(238, 228, 218);
            squareColors[4] = new Color(237, 224, 200);
            squareColors[8] = new Color(242, 177, 121);
            squareColors[16] = new Color(245, 149, 99);
            squareColors[32] = new Color(246, 124, 95);
            squareColors[64] = new Color(246, 94, 59);
            squareColors[128] = new Color(237, 207, 114);
            squareColors[256] = new Color(237, 204, 97);
            squareColors[512] = new Color(237, 200, 80);
            squareColors[1024] = new Color(237, 197, 63);
            squareColors[2048] = new Color(0, 0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            tile = Content.Load<Texture2D>("Tile");
            font = Content.Load<SpriteFont>("Font");

            updateTimer.Start();
        }

        protected override void UnloadContent()
        {

        }

        float minimumDelayMilliseconds = 250;

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || ks.IsKeyDown(Keys.Escape))
                Exit();

            if (updateTimer.ElapsedMilliseconds < minimumDelayMilliseconds) return;
            
            if (elapsedTime - prevTime >= moveDelay)
            {
                status = LordOfTheBots.testBots(100);
            }

            elapsedTime += gameTime.ElapsedGameTime;

            prevState = ks;
            updateTimer.Restart();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Chocolate);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.DrawString(font, "Highscore      Current score    Game#", new Vector2(150, 0), Color.Black);

            spriteBatch.DrawString(font, "Stupid:", new Vector2(140 - font.MeasureString("Stupid:").X, 50), Color.Black);
            spriteBatch.DrawString(font, status.stupidBotHighScore.ToString(), new Vector2(250, 50), Color.Black);
            spriteBatch.DrawString(font, status.stupidBotPoints.ToString(), new Vector2(550, 50), Color.Black);
            spriteBatch.DrawString(font, LordOfTheBots.stupidBot.gameNumber.ToString(), new Vector2(750, 50), Color.Black);

            spriteBatch.DrawString(font, "Eh:", new Vector2(140 - font.MeasureString("Eh:").X, 100), Color.Black);
            spriteBatch.DrawString(font, status.ehBotHighScore.ToString(), new Vector2(250, 100), Color.Black);
            spriteBatch.DrawString(font, status.ehBotPoints.ToString(), new Vector2(550, 100), Color.Black);
            spriteBatch.DrawString(font, LordOfTheBots.ehBot.gameNumber.ToString(), new Vector2(750, 100), Color.Black);

            spriteBatch.DrawString(font, "Stupid bot:", new Vector2(0, 400), Color.Black);
            spriteBatch.DrawString(font, "Eh bot:", new Vector2(443, 400), Color.Black);

            spriteBatch.DrawBoard(LordOfTheBots.stupidBot.board, tile, font, squareColors, 0.45, new Vector2(0, 443));
            spriteBatch.DrawBoard(LordOfTheBots.ehBot.board, tile, font, squareColors, 0.45, new Vector2(443, 443));

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}