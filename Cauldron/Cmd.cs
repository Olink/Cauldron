using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;

namespace Cauldron
{
    abstract class Cmd
    {
        protected static Dictionary<String, Cmd> commands;
        protected TSPlayer ply;
        protected String[] tokens;

        static Cmd()
        {
            commands = new Dictionary<string, Cmd>();
            commands.Add("reload", new ReloadCmd());
            commands.Add("potion", new ApplyCmd());
        }

        protected Cmd()
        {
        }

        public void setPlayer( TSPlayer p)
        {
            ply = p;
        }

        public void token( String[] t )
        {
            tokens = t;
        }

        public static Cmd findCmd( String key )
        {
            if (commands.ContainsKey(key))
            {
                return commands[key];
            }
            return null;
        }

        public abstract void exec();
    }
}
