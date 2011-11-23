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
            if( tokens.Length < 3 )
            {
                ply.SendMessage( "Potion command requires a name.", Color.Red);
                return;
            }

            if( Cauldron.potions.ContainsKey( tokens[2] ) )
            {
                Potion pot = Cauldron.potions[tokens[2]];
                pot.applyPotion( ply );
            }
            else
            {
                ply.SendMessage( "That potion does not exist.", Color.Red);
                return;
            }
        }
    }
}
