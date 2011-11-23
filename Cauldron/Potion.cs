using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;

namespace Cauldron
{
    public class Potion
    {
        private String name;
        private List<int> potions;
        private int length = 0;
        private string perm;

        public Potion( String n, int l, String p )
        {
            name = n;
            length = l;
            perm = p;
            potions = new List<int>();
        }

        public Potion(String n, int l, String pe, List<int> p )
        {
            name = n;
            length = l;
            perm = pe;
            potions = new List<int>();
            foreach( int i in p )
            {
                potions.Add( i );
            }
        }

        public void addPotion( int i )
        {
            potions.Add( i );
        }

        public String getName()
        {
            return name;
        }

        public void applyPotion(TShockAPI.TSPlayer ply)
        {
            if( ply.Group.HasPermission( perm ) )
            {
                int l = Math.Min(10, potions.Count);

                for (int i = 0; i < l; i++)
                {
                    ply.SetBuff(potions[i], 60*( length ) );
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
