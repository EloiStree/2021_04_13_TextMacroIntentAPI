using System;
using System.Threading;

namespace TextMacroIntent
{
    public class Class1
    {

        public static void Main(string[] p) {

            ThreadPool.QueueUserWorkItem(Ping, 1);

            while (true) { 
            string text = Console.ReadLine();
                Console.WriteLine("+>"+text);

            }
        }

        private static void Ping(object state)
        {
            float t = (float) state;

            while (true) {

                Console.WriteLine("Ping");
                Thread.Sleep( (int) (t*1000f) );
            }
        }
    }
}
