using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Terraria;
using TerrariaApi.Server;
using System.IO;
using TShockAPI;
using TShockAPI.DB;
using MySql.Data.MySqlClient;
using System.Threading;

namespace Cauldron
{
    [ApiVersion(1, 15)]
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
        	Order = 4;
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
            	Console.WriteLine("Basic potion file being created.");
				Console.WriteLine("1 potion containing regeneration for 30 seconds created.");
				Console.WriteLine("1 potion containing swiftness for 30 seconds created.");
            }

        }

        public override void Initialize()
        {
			Commands.ChatCommands.Add( new Command( "", HandleCommand, "cauldron"));
        }

        private void HandleCommand(CommandArgs args)
        {
        	TSPlayer ply = args.Player;
            if (ply == null)
                return;

			if( args.Parameters.Count < 1 )
			{
				ply.SendMessage( "Please see help for a list of commands and the proper syntax.", Color.Red);
				return;
			}

			if( args.Parameters[0] == "help")
			{
				ply.SendMessage( "The following are valid commands:", Color.Yellow);
				ply.SendMessage("/cauldron reload - This reloads the potions from file.", Color.Yellow);
				ply.SendMessage("/cauldron <potion name> - Applies the specified potion.", Color.Yellow);
			}
			else if( args.Parameters[0] == "reload")
			{
				if (ply.Group.HasPermission("cauldron-reload"))
				{
					Console.WriteLine("Potions are being reloaded from file.");
					var reader = new CauldronReader();
					potions = reader.readFile(Path.Combine(TShock.SavePath, "potions.cfg"));
				}
				else
				{
					ply.SendMessage("You do not have access to this command.", Color.Red);
				}
			}
			else
			{
				Potion p = potions.findPotion(args.Parameters[0]);
				if (p != null)
				{
					p.applyPotion(ply);
				}
				else
				{
					ply.SendMessage("That potion does not exist.", Color.Red);
				}
			}
        }
    }
}
