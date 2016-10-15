using Discord;
using Syn.Bot.Siml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        static DiscoBot myBot;
        static int randTime;
        static Boolean started = false;
        static System.Timers.Timer timer;

        static void Main(string[] args)
        {
            myBot = new DiscoBot();

            // Create a 30 min timer 
            timer = new System.Timers.Timer();

            // Hook up the Elapsed event for the timer.
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 5000;
            timer.Enabled = true;


            myBot.startBot();


            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;
        }

        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            Random random = new Random();
            randTime = random.Next(10, 31);
            timer.Interval = randTime * 60000;

            //timer.Interval = 5000; // DEBUG

            //Console.WriteLine("Message Timer: " + randTime + " mins");

            myBot.SendMessage(started);
            started = true;

        }

        public static void resetTimer()
        {
            timer.Interval = randTime;
        }
    }
}
