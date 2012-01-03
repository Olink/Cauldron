using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Cauldron
{
    class CauldronReader
    {
        public PotionList writeFile(String file)
        {
            TextWriter tw = new StreamWriter(file);

            PotionList potions = new PotionList();
            List<int> potionItems = new List<int>();
            potionItems.Add(2);
            potions.potions.Add(new Potion( "regen", 30, null, potionItems) );

            List<int> potionItems2 = new List<int>();
            potionItems2.Add(3);
            potions.potions.Add(new Potion("haste", 30, null, potionItems2));

            tw.Write(JsonConvert.SerializeObject(potions, Formatting.Indented));
            tw.Close();

            return potions;
        }

        public PotionList readFile(String file)
        {
            PotionList potions = null;
            using (var tr = new StreamReader(file))
            {
                String raw = tr.ReadToEnd();
                potions = JsonConvert.DeserializeObject<PotionList>(raw);
            }
            return potions;
        }
    }
}
