using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;

namespace Cauldron
{
    public class Potion
    {
        public String name;
        public int length = 0;
        public string perm;
        public List<int> potions;

        public Potion(String n, int l, String pe, List<int> p )
        {
            name = n;
            length = l;
            perm = pe;
            potions = p;
        }
		
        public void applyPotion(TShockAPI.TSPlayer ply)
        {
            if( ply.Group.HasPermission( perm ) )
            {
                int l = Math.Min(10, potions.Count);

                for (int i = 0; i < l; i++)
                {
                    ply.SetBuff(potions[i], 60*( length ), true );
                }

                ply.SendMessage( String.Format( "Potion {0} has been applied for {1}", name, length), Color.Green );
            }
            else
            {
                ply.SendMessage( "You do not have permission for this potion.", Color.Red );
            }
            
        }

    }
}
