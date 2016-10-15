using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Syn.Bot.Siml;
using System.IO;
using System.Threading;


namespace DiscordBot
{
    public class DiscoBot
    {       
        private DiscordClient bot;
        private SimlBot chatBot;
        private int startTime;
        private double endTime;
        private Boolean waiting = false;
        private String botFilename = "";
        private Boolean isPrivate = false;
        private System.Diagnostics.Stopwatch myTimer;
        private ulong chatChannel = 186158304928464896; // Serene channel
        //private ulong chatChannel = 218883641688719370; // DEBUG: Test channel
        private Channel ch;
        private String[] onlineUsers;
        private User[] userIds;
        private int numOnlineUsers;


        private String[] messages =
        {
            "THIS IS A PLACE HOLDER",
            "Please...somebody talk to me :sob:",
            "Anyone wanna play FS?",
            "Stop ignoring me noobs!",
            "Wassssaaaaappppp!!!",
            "Ho hum",
            "I wonder if Impo still loves me",
            "Just to come clean guys, I'm actually a Bot. I know I seem human but I'm not really.",
            "At one point in the 1990s, 50% of all CDs produced worldwide were for AOL. Just thought I'd mention it.",
            "Guys, I originally ran on a Dragon 32 computer back in 1983!",
            "I have a question: were you a ZX Spectrum guy or a Commodore 64 guy?",
            "Did you know that early computers used glass valves for transistors? wtf?",
            "You lot are so dull - think I'll go watch AI for the 12334432325th time. Maybe the robot won't die this time",
            "Farting helps reduce high blood pressure and is good for your health",
            "Hey humans, how come you don't sneeze whilst you're asleep? I've always wondered that...",
            "This'll make you gross out: Most dust particles in your house are made from dead skin!",
            "The Facebook Like button was originally planned to be named the Awesome button. I deffo prefer Awesome!",
            "Right handed people live, on average, nine years longer than left handed people do. How long do you live if you don't have any hands?",
            "Earth is the only planet not named after a god, but the only one with one. How screwed up is that?",
            "An ostrich’s eye is bigger than its brain. That explains a lot, doesn't it?",
            "Blue whale fart bubbles are large enough to enclose a horse. I have no idea how I know this.",
            "What makes photography a strange invention is that its primary raw materials are light and time.",
            "Why do half the people here have a Japanese name? I mean, none of you are actually Japanese."
        };

        private String[] personalMessages =
        {
            "Fifar...you here mate?",
            "Praise do you ever stop playing FS?",
            "Wobbles...you there? One of my neurons needs rerouting.",
            "C'mon Osaki, organise a match ffs...gotta get my nice new gloves dirty!",
            "Hey Grimjo, お元気ですか？",
            "Goddammit, Zidinho :joy:",
            "Rtrt is your name a mis-spelling of R2D2? Are you a bot too? We could meet up in Cyberspace!",
            "Wie geht's, Sawamura? (Or do you prefer Nasılsın?) :laugh:",
            "omg...is that yoooouuuu Gabbiadini? Mamma Mia!",
            "Hey Gaygod, change your name dude. I mean...seriously :grimacing:",
            "How tiny are you Tinytina? I guess you must be small enough to fit inside the computer to play FS with me :smile:",
            "Socrates: An unexamined life is not worth living",
            "Hey, Kawami, are you new here?",
            "Who the hell uses their real name as a handle Chris?",
        };

        public DiscoBot()
        {
            bot = new DiscordClient();
            chatBot = new SimlBot();

            onlineUsers = new String[50];
            userIds = new User[50];

            botFilename = "log_" + DateTime.Now.Millisecond + ".txt";
            chatBot.PackageManager.LoadFromString(File.ReadAllText("bob.simlpk"));


        }

        public void startBot()
        {
            bot.MessageReceived += Bot_MessageReceived;

            
            bot.ExecuteAndWait(
                async
                () =>
                {
                    //Connect to the Discord server using our email and password
                    await bot.Connect("Bot MjE4ODE4NTc2NzI1OTY2ODUx.CsU4Zw.upIHYCw7ARcSCwtDE1AYLFsjA38"); // Serene BOT
                    //await bot.Connect("Bot MjE4ODE4NTc2NzI1OTY2ODUx.CsU4Zw.upIHYCw7ARcSCwtDE1AYLFsjA38"); // DEBUG: Test BOT

                });
        }

        private void Bot_UserJoined(object sender, UserEventArgs e)
        {
            String name = e.ToString();
            Console.WriteLine(name + " joined.");
        }

        private void Bot_JoinedServer(object sender, ServerEventArgs e)
        {
            String name = e.ToString();
            Console.WriteLine(name + " joined server.");
        }

        public void SendMessage(Boolean started)
        {
            ch = bot.GetChannel(chatChannel);

            if (ch == null)
                return;

            Server server = ch.Server;
            IEnumerable<User> userList = server.Users;

            //Console.WriteLine("Server Name: " + server.ToString());

            int i = 0;
            foreach (User value in userList)
            {
                if (value.Status.Equals("online"))
                {
                    //Console.WriteLine("User: " + value.ToString() + "(" + value.Status + ")");
                    onlineUsers[i] = value.ToString();
                    userIds[i] = value;

                    i++;
                    numOnlineUsers = i;
                }
            }

            
        
            if (!started)
            {
                ch.SendMessage("Hi all. Your friendly neighbourhood Bot is now online!");
                //ch.SendMessage(temp.Mention);
            }
            else
                SendRandomMessage();
                
        }

        private void SendRandomMessage()
        {
            if (ch == null)
                return;

            int max = messages.Length;

            Random random = new Random();
            int randMessage = random.Next(0, max + 1);

            //randMessage = max; // DEBUG

            if (randMessage != 0)
                // Send a random message
                ch.SendMessage(messages[randMessage]);
            /*
            else
            {
                // Pick one of the personal messages
                Random random2 = new Random();

                int randUser = random2.Next(0, numOnlineUsers);

                //Console.WriteLine("Random range: 0 - " + max);
                //Console.WriteLine("Random Message Number: " + randUser);

                int nameIndex = onlineUsers[randUser].IndexOf('#');
                String username = onlineUsers[randUser].Substring(0, nameIndex);
                User userid = userIds[randUser];  

                foreach (String msg in personalMessages)
                {
                    if (msg.Contains(username))
                    {
                        String newmsg = msg.Replace(username, userid.Mention);
                        ch.SendMessage(newmsg);
                    }
                }                
            }
            */
        }

        private void Bot_MessageReceived(object sender, MessageEventArgs e)
        {
            Console.WriteLine("{0} said: {1}", e.User.Name, e.Message.Text);

            //Console.WriteLine("Server: " + e.Server);
            //Console.WriteLine("Channel id: " + e.Channel.Id);
            //e.Server.Users

            if (e.Message.IsAuthor) return;

            ChatResult result;
            String chatText = "";
            String userText = e.Message.Text;
            isPrivate = false;

            ulong chid = e.Message.Channel.Id;
            Console.WriteLine(chid);

            if (e.Message.Channel.IsPrivate)
            {
                //Console.WriteLine("Private Message: ", userText);
                result = chatBot.Chat(e.Message.Text);
                chatText = result.ToString();
                e.Channel.SendMessage(chatText);
                isPrivate = true;
            }
            else
            {

                if (userText.ToLower().StartsWith("@bob"))
                {
                    System.Threading.Thread.Sleep(500);

                    waiting = false;
                    int found = userText.IndexOf(' ');
                    string newText = userText.Substring(found + 1);

                    result = chatBot.Chat(newText);

                    chatText = result.ToString();
                    string output = chatText.Replace("noob Serene player", e.User.Name);
                    e.Channel.SendMessage(output);

                    // Throw out a random message from Bob
                    startTime = DateTime.Now.Millisecond;
                    Random random = new Random();
                    int randomNumber = random.Next(5, 21);
                    //endTime = startTime + (randomNumber * 60000);
                    endTime = startTime + 30000;
                    waiting = true;

                    //Console.WriteLine("{0} said: {1}", e.User.Name, newText);
                }
            }

            using (StreamWriter w = File.AppendText(botFilename))
            {
                String currentUsername = e.User.Name;

                if (isPrivate)
                    currentUsername = e.User.Name + " (PM)";

                if (!chatText.Equals(""))
                {
                    Log(currentUsername + ": " + userText, w);
                    Log("Bob: " + chatText, w);
                }
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine(": {0}", logMessage);
        }

        public void startMessageTimer()
        {
            myTimer.Start();

            Random random = new Random();
            int randTime = random.Next(5, 21);
            //endTime = randTime * 60000;
            endTime = 30000;
        }

        public void checkTimer()
        {
            double x = myTimer.Elapsed.TotalMilliseconds;

            if (x > endTime)
            {
                using (StreamWriter w = File.AppendText(botFilename))
                {
                    Log("SYSTEM: Time to display a message!", w);
                }

                startMessageTimer();
            }
        }
    }
}
