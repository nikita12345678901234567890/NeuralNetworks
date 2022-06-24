using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        int pipeSpeed = 20;
        const int gravityConst = 5;
        const int jumpConst = 50;
        int gravity = gravityConst;

        const int number = 300;

        Random random = new Random();
        (NeuralNetwork.NeuralNetwork network, double fitness)[] population = new (NeuralNetwork.NeuralNetwork, double)[number];

        struct game
        {
            public bool playing;
            public int score;
            public Rectangle birb;
        }

        public Rectangle pipeTop;
        public Rectangle pipeBottom;

        game[] games = new game[number];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            pipeTop.X = 525;
            pipeTop.Y = 0;
            pipeTop.Width = 50;
            pipeTop.Height = 300;

            pipeBottom.X = 525;
            pipeBottom.Y = 500;
            pipeBottom.Width = 50;
            pipeBottom.Height = 300;

            for (int i = 0; i < number; i++)
            {
                population[i].network = new NeuralNetwork.NeuralNetwork(Perceptron.ActivationFunctions.Identity, Perceptron.ErrorFunctions.MSE, 2, 4, 1);
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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = new Texture2D(GraphicsDevice, 1, 1);

            pixel.SetData(new Color[] { Color.White });
        }

        public void flap()
        {
            gravity = -jumpConst;
        }
        public void glide()
        {
            gravity = gravityConst;
        }

        public void update()
        {
            Phish();

            for (int i = 0; i < number; i++)
            {
                if (games[i].playing)
                {
                    games[i].birb.Y += gravity; // link the flappy bird picture box to the gravity, += means it will add the speed of gravity to the picture boxes top location so it will move down

                    // below we are checking if any of the pipes have left the screen

                    if (pipeBottom.X < -150)
                    {
                        // if the bottom pipes location is -150 then we will reset it back to 800 and add 1 to the score
                        pipeBottom.X = 800;
                        games[i].score++;
                    }
                    if (pipeTop.X < -180)
                    {
                        // if the top pipe location is -180 then we will reset the pipe back to the 950 and add 1 to the score
                        pipeTop.X = 950;
                        games[i].score++;
                    }

                    // the if statement below is checking if the pipe hit the ground, pipes or if the player has left the screen from the top
                    // the two pipe symbols stand for OR inside of an if statement so we can have multiple conditions inside of this if statement because its all going to do the same thing

                    if (games[i].birb.Intersects(pipeBottom) ||
                        games[i].birb.Intersects(pipeTop) ||
                        games[i].birb.Bottom > GraphicsDevice.Viewport.Height || games[i].birb.Top < -25
                        )
                    {
                        games[i].playing = false;
                    }
                }
            }

            pipeBottom.X -= pipeSpeed;
            pipeTop.X -= pipeSpeed;
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
                    int heightDistance = graphics.GraphicsDevice.Viewport.Height / 2 - (games[i].birb.Location.Y - games[i].birb.Height / 2);

                    double result = population[i].network.Compute(new double[] { distance, heightDistance })[0];

                    if (result < 25) glide();
                    else flap();
                }
            }

            if (done)
            {
                for (int i = 0; i < number; i++)
                {
                    population[i].fitness = games[i].score;

                    games[i].birb.Y = 300;
                    games[i].score = 0;
                    games[i].playing = true;
                }

                Train(population, random, 0.1, -1, 1);

                pipeTop.X = 525;
                pipeBottom.X = 525;
            }
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

            update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(pixel, pipeTop, Color.GreenYellow);
            spriteBatch.Draw(pixel, pipeBottom, Color.GreenYellow);

            for (int i = 0; i < number; i++)
            {
                spriteBatch.Draw(pixel, games[i].birb, Color.Chocolate);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
