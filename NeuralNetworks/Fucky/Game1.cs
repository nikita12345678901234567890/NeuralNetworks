using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using NeuralNetwork;

using Perceptron;

using System;

namespace Flappy
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public Texture2D pixel;

        int pipeSpeed = 15;
        const int gravity = 5;
        const int jumpConst = 150;

        int defaultPipeHeight = 250;
        const int maxShift =150;

        const int number = 3000;

        public int Generation = 0;
        public int Best = 0;
        public int Current = 0;
        public int numberAlive = 0;

        Random random = new Random();
        (NeuralNetwork.NeuralNetwork network, double fitness)[] population = new (NeuralNetwork.NeuralNetwork, double)[number];

        public struct game
        {
            public bool playing;
            public int score;
            public Rectangle birb;
        }

        public Rectangle pipeTop;
        public Rectangle pipeBottom;

        game[] games = new game[number];

        const bool testing = false;
        bool testPlaying = true;
        game testGame;
        KeyboardState keyState = new KeyboardState();
        KeyboardState prevState = new KeyboardState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //this.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 300);
        }

        protected override void Initialize()
        {
            pipeTop.X = 525;
            pipeTop.Y = 0;
            pipeTop.Width = 50;
            pipeTop.Height = defaultPipeHeight;

            pipeBottom.X = 525;
            pipeBottom.Y = 800 - defaultPipeHeight;
            pipeBottom.Width = 50;
            pipeBottom.Height = defaultPipeHeight;

            for (int i = 0; i < number; i++)
            {
                population[i].network = new NeuralNetwork.NeuralNetwork(Perceptron.ErrorFunctions.MSE, (2, ActivationFunctions.Identity), (4, ActivationFunctions.Identity), (1, ActivationFunctions.BinaryStep));
                population[i].network.Randomize(random, -1, 1);
                games[i].birb = new Rectangle();
                games[i].birb.X = 250;
                games[i].birb.Y = 400;
                games[i].birb.Width = 50;
                games[i].birb.Height = 50;
                games[i].score = 0;
                games[i].playing = true;
            }

            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            testGame.birb = new Rectangle();
            testGame.birb.X = 250;
            testGame.birb.Y = 400;
            testGame.birb.Width = 50;
            testGame.birb.Height = 50;
            testGame.score = 0;
            testGame.playing = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = new Texture2D(GraphicsDevice, 1, 1);

            pixel.SetData(new Color[] { Color.White });
        }

        public void flap(ref game game)
        {
            game.birb.Y -= jumpConst;
        }

        public void Phish()
        {
            bool done = true;
            numberAlive = 0;

            for (int i = 0; i < number; i++)
            {
                if (games[i].playing)
                {
                    done = false;
                    numberAlive++;

                    int distance = pipeTop.Left < pipeBottom.Left ? pipeTop.Left : pipeBottom.Left;
                    distance -= games[i].birb.Right;
                    int heightDistance = graphics.GraphicsDevice.Viewport.Height / 2 - (games[i].birb.Location.Y - games[i].birb.Height / 2);
                    double[] inputs = new[] { (double)distance / graphics.GraphicsDevice.Viewport.Width, (double)heightDistance / graphics.GraphicsDevice.Viewport.Height };
                    double result = population[i].network.Compute(inputs)[0];

                    if (result > 0.6) flap(ref games[i]);

                    population[i].fitness++;
                    Current = (int)population[i].fitness;
                    if (population[i].fitness > Best) Best = (int)population[i].fitness;
                }
            }

            if (done)
            {
                Generation++;
                for (int i = 0; i < number; i++)
                {
                    //population[i].fitness = games[i].score;
                    population[i].fitness = 0;
                    games[i].birb.Y = 300;
                    games[i].score = 0;
                    games[i].playing = true;
                }

                Train(population, random, 0.25, -10, 10);

                pipeTop.X = 525;
                pipeBottom.X = 525;

                pipeTop.Height = defaultPipeHeight;
                pipeBottom.Y = 800 - defaultPipeHeight;
                pipeBottom.Height = defaultPipeHeight;
            }

            Window.Title = $"Generation: {Generation}, Best: {Best}, Current: {Current}, Alive: {numberAlive}";
        }

        public void Mutate(NeuralNetwork.NeuralNetwork net, Random random, double mutationRate)
        {
            foreach (Layer layer in net.Layers)
            {
                foreach (Neuron neuron in layer.Neurons)
                {
                    //Mutate the Weights
                    for (int i = 0; i < neuron.dendrites.Length; i++)
                    {
                        if (random.NextDouble() < mutationRate)
                        {
                            if (random.Next(2) == 0)
                            {
                                neuron.dendrites[i].Weight *= random.NextDouble(0.5, 1.5); //scale weight
                            }
                            else
                            {
                                neuron.dendrites[i].Weight *= -1; //flip sign
                            }
                        }
                    }

                    //Mutate the Bias
                    if (random.NextDouble() < mutationRate)
                    {
                        if (random.Next(2) == 0)
                        {
                            neuron.bias *= random.NextDouble(0.5, 1.5); //scale weight
                        }
                        else
                        {
                            neuron.bias *= -1; //flip sign
                        }
                    }
                }
            }
        }

        public void Crossover(NeuralNetwork.NeuralNetwork winner, NeuralNetwork.NeuralNetwork loser, Random random)
        {
            for (int i = 0; i < winner.Layers.Length; i++)
            {
                //References to the Layers
                Layer winLayer = winner.Layers[i];
                Layer childLayer = loser.Layers[i];

                int cutPoint = random.Next(winLayer.Neurons.Length); //calculate a cut point for the layer
                bool flip = random.Next(2) == 0; //randomly decide which side of the cut point will come from winner

                //Either copy from 0->cutPoint or cutPoint->Neurons.Length from the winner based on the flip variable
                for (int j = (flip ? 0 : cutPoint); j < (flip ? cutPoint : winLayer.Neurons.Length); j++)
                {
                    //References to the Neurons
                    Neuron winNeuron = winLayer.Neurons[j];
                    Neuron childNeuron = childLayer.Neurons[j];

                    //Copy the winners Weights and Bias into the loser/child neuron
                    winNeuron.setWeights(childNeuron.dendrites);
                    childNeuron.bias = winNeuron.bias;
                }
            }
        }

        public void Train((NeuralNetwork.NeuralNetwork net, double fitness)[] population, Random random, double mutationRate, double min, double max)
        {
            Array.Sort(population, (a, b) => b.fitness.CompareTo(a.fitness));

            int start = (int)(population.Length * 0.1);
            int end = (int)(population.Length * 0.9);

            //Notice that this process is only called on networks in the middle 80% of the array
            for (int i = start; i < end; i++)
            {
                Crossover(population[random.Next(start)].net, population[i].net, random);
                Mutate(population[i].net, random, mutationRate);
            }

            //Removes the worst performing networks
            for (int i = end; i < population.Length; i++)
            {
                population[i].net.Randomize(random, min, max);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (testing)
            {
                prevState = keyState;
                keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space)) flap(ref testGame);

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    testGame.birb.X = 250;
                    testGame.birb.Y = 400;
                    testGame.birb.Width = 50;
                    testGame.birb.Height = 50;
                    testGame.score = 0;
                    testPlaying = true;
                }

                if (testPlaying)
                {
                    testGame.birb.Y += gravity;

                    int verticalShift = 0;
                    if (pipeBottom.X < -150)
                    {
                        verticalShift = random.Next(-maxShift, maxShift);
                        pipeBottom.Y = (800 - defaultPipeHeight) - verticalShift;
                        pipeBottom.Height = defaultPipeHeight + verticalShift;

                        pipeBottom.X = 800;

                        testGame.score++;
                    }
                    if (pipeTop.X < -150)
                    {
                        pipeTop.Height = defaultPipeHeight + verticalShift;

                        pipeTop.X = 800;
                        testGame.score++;
                    }


                    if (testGame.birb.Intersects(pipeBottom) ||
                        testGame.birb.Intersects(pipeTop) ||
                        testGame.birb.Bottom > GraphicsDevice.Viewport.Height || testGame.birb.Top < -25
                        )
                    {
                        testPlaying = false;
                    }
                }
                prevState = keyState;
            }

            else
            { 
                Phish();
                for (int i = 0; i < number; i++)
                {
                    if (games[i].playing)
                    {
                        games[i].birb.Y += gravity;

                        int verticalShift = 0;
                        if (pipeBottom.X < -150)
                        {
                            verticalShift = random.Next(-maxShift, maxShift);
                            pipeBottom.Y = (800 - defaultPipeHeight) - verticalShift;
                            pipeBottom.Height = defaultPipeHeight + verticalShift;

                            pipeBottom.X = 800;
                            games[i].score++;
                        }
                        if (pipeTop.X < -150)
                        {
                            pipeTop.Height = defaultPipeHeight - verticalShift;

                            pipeTop.X = 800;
                            games[i].score++;
                        }


                        if (games[i].birb.Intersects(pipeBottom) ||
                            games[i].birb.Intersects(pipeTop) ||
                            games[i].birb.Bottom > GraphicsDevice.Viewport.Height || games[i].birb.Top < -25
                            )
                        {
                            games[i].playing = false;
                        }
                    }
                }
            }

            pipeBottom.X -= pipeSpeed;
            pipeTop.X -= pipeSpeed;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(pixel, pipeTop, Color.GreenYellow);
            spriteBatch.Draw(pixel, pipeBottom, Color.GreenYellow);

            if (testing)
            {
                spriteBatch.Draw(pixel, testGame.birb, Color.Green);
            }
            else
            {
                for (int i = 0; i < number; i++)
                {
                    if (games[i].playing)
                    {
                        spriteBatch.Draw(pixel, games[i].birb, Color.Chocolate);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}