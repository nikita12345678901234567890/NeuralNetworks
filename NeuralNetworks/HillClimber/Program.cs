using System;

namespace HillClimber
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            string target = "you are a fish?";

            char[] current = new char[target.Length];
            for (int i = 0; i < target.Length; i++)
            {
                current[i] = (char)random.Next(32, 127);
            }

            var error = MAE(current, target);
            while (error > 0)
            {
                int index = random.Next(current.Length);
                char original = current[index];
                current[index] = (char)(current[index] + random.Next(-1, 2));

                float newError = MAE(current, target);
                if (newError < error)
                {
                    error = newError;
                }
                else
                {
                    current[index] = original;
                }

                Print(current, target, error);
            }
            Print(current, target, error);
        }

        /// <summary>
        /// Calculates the MEAN ABSOLOUTE ERROR
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        static float MAE(char[] current, string target)
        {
            int sum = 0;  //previously known as fish

            for (int i = 0; i < target.Length; i++)
            {
                sum += Math.Abs(current[i] - target[i]);
            }

            return sum / (float)target.Length;
        }

        static void Print(char[] current, string target, float error)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;

            var original = Console.ForegroundColor;
            for (int i = 0; i < current.Length; i++)
            {
                Console.Write(current[i]);
            }
            Console.Write($" => ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{target}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" ({error.ToString("0.00")})     ");

            Console.ForegroundColor = original;

        //    System.Threading.Thread.Sleep(1);
        }
    }
}