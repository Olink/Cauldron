using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cauldron
{
    public class PotionList
    {
        public List<Potion> potions;

        public PotionList()
        {
            potions = new List<Potion>();
        }

        public Potion findPotion( String name )
        {
            foreach (Potion p in potions)
            {
                if( p.name == name )
                {
                    return p;
                }
            }

            return null;
        }
    }
}
