using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraffSim
{
    static class Program
    {
        public static Queue Expo(double mean, double period)
        {
            // This method generates a sequence of cars dates arrival
            // in interval [0, period] as double real numbers in minutes
            // Dates are stored in a Queue
            // The model is a Poisson process of 'mean' cars/minute
            // And theory says that inter-arrival dates 
            // form an exponential sequence of parameter 1/mean
            // To retreive dates from the returned Queue, let's name it times:
            // while (times.Count != 0)
            //    Console.WriteLine((double)times.Dequeue());
            Queue q = new Queue();
            Random g = new Random();
            double current_time = 0.0, u, inter;
            while (current_time < period)
            {
                u = g.NextDouble(); // u in [0,1]
                inter = -1.0 / mean * Math.Log(1 - u);
                current_time += inter;
                if (current_time < period) q.Enqueue(current_time);
            }
            return q;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form0());
        }
    }
}
