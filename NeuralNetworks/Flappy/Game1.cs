using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace Flappy
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int number = 300;

        Random random = new Random();
        (NeuralNetwork.NeuralNetwork network, double fitness)[] population = new (NeuralNetwork.NeuralNetwork, double)[number];

        struct game
        {
            public bool playing;
            public int score;
            public PictureBox birb;
        }

        game[] games = new game[number];

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < number; i++)
            {
                population[i].network = new NeuralNetwork.NeuralNetwork(Perceptron.ActivationFunctions.Identity, Perceptron.ErrorFunctions.MSE, 2, 4, 1);
                games[i].birb = new PictureBox();
                games[i].birb.Image = Resources.flappyBird;
                games[i].birb.Visible = true;
                games[i].score = 0;
                games[i].birb.SizeMode = PictureBoxSizeMode.StretchImage;
                Controls.Add(games[i].birb);
            }
        }

        public void Phish()
        {
            bool done = true;
            for (int i = 0; i < number; i++)
            {
                if (games[i].playing)
                {
                    done = false;

                    int distance = pipeTop.Left < pipeBottom.Left ? pipeTop.Left : pipeBottom.Left;
                    distance -= games[i].birb.Right;
                    int heightDistance = this.Height / 2 - (games[i].birb.Location.Y - games[i].birb.Height / 2);

                    double result = population[i].network.Compute(new double[] { distance, heightDistance })[0];

                    if (result < 25) glide();
                    else flap();
                }
            }

            if (done)
            {
                StopGame();
                for (int i = 0; i < number; i++)
                {
                    population[i].fitness = games[i].score;

                    games[i].birb.Top = 300;
                    games[i].score = 0;
                    games[i].playing = true;
                }

                Train(population, random, 0.1, -1, 1);

                pipeTop.Left = 525;
                pipeBottom.Left = 525;

                StartGame();
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
