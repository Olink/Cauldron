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
    [APIVersion(1, 8)]
    public class Cauldron : TerrariaPlugin
    {
        public static Dictionary<String, Potion> potions;

        public override Version Version
        {
            get { return new Version("1.1"); }
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
            potions = new Dictionary<string, Potion>();
            String file = Path.Combine(TShock.SavePath, "potions.txt");
            if (File.Exists(file))
            {
                using (var sr = new StreamReader(file))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        String[] info = line.Split();
                        if (info.Length >= 4)
                        {
                            String pName = info[0];
                            int l = 0;
                            int.TryParse(info[1], out l);
                            String per = info[2];
                            Potion p = new Potion(pName, l, per);
                            for (int i = 3; i < info.Length; i++)
                            {
                                int id = 0;
                                int.TryParse(info[i], out id);
                                p.addPotion(id);
                            }
                            Console.WriteLine( String.Format( "Potion {0} added.", pName));
                            potions.Add(pName.ToLower(), p);
                        }
                    }
                }
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
                    ply.SendMessage("/cauldron potion <potion name>", Color.Red);
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
                    ply.SendMessage("Invalid command.  Valid commands are:", Color.Red);
                    ply.SendMessage("/cauldron reload", Color.Red);
                    ply.SendMessage("/cauldron potion <potion name>", Color.Red);
                }
            }

        }
    }
}
