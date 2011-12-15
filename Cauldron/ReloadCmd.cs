using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TShockAPI;

namespace Cauldron
{
    class ReloadCmd:Cmd
    {
        public override void exec()
        {
            if (ply.Group.HasPermission("cauldron-reload"))
            {
                Console.WriteLine("Potions are being reloaded from file.");
                CauldronReader reader = new CauldronReader();
                Cauldron.potions = reader.readFile(Path.Combine(TShockAPI.TShock.SavePath, "kits.cfg"));
            }
            else
            {
                ply.SendMessage( "You do not have access to this command.", Color.Red);
            }
        }
    }
}
