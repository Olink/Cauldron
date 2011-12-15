using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Terraria;
using Hooks;
using System.IO;
using TShockAPI;
using TShockAPI.DB;
using MySql.Data.MySqlClient;
using System.Threading;

namespace Cauldron
{
    [APIVersion(1, 9)]
    public class Cauldron : TerrariaPlugin
    {
        public static PotionList potions;

        public override Version Version
        {
            get { return new Version("1.2"); }
        }

        public override string Name
        {
            get { return "Potions"; }
        }

        public override string Author
        {
            get { return "Zach Piispanen"; }
        }

        public override string Description
        {
            get { return "Create special potions by combining old potions!"; }
        }

        public Cauldron(Main game)
            : base(game)
        {
            String savepath = Path.Combine(TShock.SavePath, "potions.cfg");
            CauldronReader reader = new CauldronReader();
            if (File.Exists(savepath))
            {
                try
                {
                    potions = reader.readFile(savepath);
                    Console.WriteLine(potions.potions.Count + " potions have been loaded.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                potions = reader.writeFile(savepath);
                Console.WriteLine("Basic potion file being created.  1 potion containing regeneration for 30 seconds created");
            }
        }

        public override void Initialize()
        {
            ServerHooks.Chat += handleCommand;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerHooks.Chat -= handleCommand;
            }
        }

        private void handleCommand(messageBuffer buff, int i, String command, HandledEventArgs args)
        {
            TSPlayer ply = TShock.Players[buff.whoAmI];
            if (ply == null)
                return;

            String commandline = command;
            String[] tokens = command.Split(' ');

            if (commandline.Length > 0 && tokens[0] == "/cauldron")
            {
                args.Handled = true;
                tokens = commandline.Trim().Split();

                for (int k = 0; k < tokens.Length; k++)
                {
                    tokens[k] = tokens[k].ToLower().Trim();
                }

                if( tokens.Length == 1 )
                {
                    ply.SendMessage("Valid commands are:", Color.Red);
                    ply.SendMessage("/cauldron reload", Color.Red);
                    ply.SendMessage("/cauldron <potion name>", Color.Red);
                    return;
                }
                
                Cmd cmd = Cmd.findCmd(tokens[1]);

                if (cmd != null)
                {
                    cmd.token(tokens);
                    cmd.setPlayer(ply);
                    cmd.exec();
                }
                else
                {
                    cmd = new ApplyCmd();
                    cmd.token(tokens);
                    cmd.setPlayer(ply);
                    cmd.exec();
                }
            }

        }
    }
}
