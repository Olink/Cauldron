using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cauldron
{
    class ApplyCmd:Cmd
    {
        public override void exec()
        {
            if( tokens.Length < 2 )
            {
                ply.SendMessage( "Potion command requires a name.", Color.Red);
                return;
            }

            Potion p = Cauldron.potions.findPotion( tokens[1] );
            if( p != null )
            {
                p.applyPotion( ply );
            }
            else
            {
                ply.SendMessage( "That potion does not exist.", Color.Red);
                return;
            }
        }
    }
}
