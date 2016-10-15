using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        private static DiscordClient bot;
        private static SimlBot chatBot;

        static void Main(string[] args)
        {
            bot.MessageReceived += Bot_MessageReceived;

            bot.ExecuteAndWait(
                async
                () =>
                {
                    //Connect to the Discord server using our email and password
                    await bot.Connect("MjE4ODE4NTc2NzI1OTY2ODUx.CqM59Q.f_Hp2-_NVF0KVq2kBu2_dEezpvo");

                });
        }
    }
}
