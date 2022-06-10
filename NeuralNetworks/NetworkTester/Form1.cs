using NeuralNetwork;
using static Perceptron.Extensions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkTester
{
    public partial class Form1 : Form
    {
        NeuralNetwork.NeuralNetwork[] population = new NeuralNetwork.NeuralNetwork[10];
        public Form1()
        {
            for(int i = 0; i < population.Length; i++)
            {
                population[i] = new NeuralNetwork.NeuralNetwork(Perceptron.ActivationFunctions.BinaryStep, Perceptron.ErrorFunctions.MSE, 2, 4, 1);
            }
            InitializeComponent();
        }


        public void Phish()
        {
            for (int i = 0; i < population.Length; i++)
            {
                int distance = pipeTop.Left < pipeBottom.Left ? pipeTop.Left : pipeBottom.Left;
                distance -= flappyBird.Right;
                int heightDistance = this.Height / 2 - (flappyBird.Location.Y - flappyBird.Height / 2);

                var result = population[i].Compute(new double[] {distance, heightDistance})[0];

                if()
            }
        }

        #region flappy
        // stolen from MOO ICT Flappy Bird Tutorial

        int pipeSpeed = 20; // default pipe speed defined with an integer
        const int gravityConst = 5;
        const int jumpConst = 50;
        int gravity = gravityConst;
        int score = 0;


        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                flap();
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                glide();
            }
        }

        public void flap()
        {
            gravity = -jumpConst;
        }
        public void glide()
        {
            gravity = gravityConst;
        }

        private void endGame()
        {
            // this is the game end function, this function will when the bird touches the ground or the pipes
            gameTimer.Stop(); // stop the main timer
            scoreText.Text += " Game over!!!"; // show the game over text on the score text, += is used to add the new string of text next to the score instead of overriding it
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity; // link the flappy bird picture box to the gravity, += means it will add the speed of gravity to the picture boxes top location so it will move down
            pipeBottom.Left -= pipeSpeed; // link the bottom pipes left position to the pipe speed integer, it will reduce the pipe speed value from the left position of the pipe picture box so it will move left with each tick
            pipeTop.Left -= pipeSpeed; // the same is happening with the top pipe, reduce the value of pipe speed integer from the left position of the pipe using the -= sign
            scoreText.Text = "Score: " + score; // show the current score on the score text label

            // below we are checking if any of the pipes have left the screen

            if (pipeBottom.Left < -150)
            {
                // if the bottom pipes location is -150 then we will reset it back to 800 and add 1 to the score
                pipeBottom.Left = 800;
                score++;
            }
            if (pipeTop.Left < -180)
            {
                // if the top pipe location is -180 then we will reset the pipe back to the 950 and add 1 to the score
                pipeTop.Left = 950;
                score++;
            }

            // the if statement below is checking if the pipe hit the ground, pipes or if the player has left the screen from the top
            // the two pipe symbols stand for OR inside of an if statement so we can have multiple conditions inside of this if statement because its all going to do the same thing

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < -25
                )
            {
                // if any of the conditions are met from above then we will run the end game function
                endGame();
            }

        }
        #endregion



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
    }
}